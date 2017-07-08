using Framework.Auth;
using Framework.Common.Json;
using Ncb.Data;
using Ncb.DataServices;
using Ncb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ncb.Admin.Controllers
{
    public class DeviceController : MediaBaseController
    {
        private readonly DeviceModelManager _DeviceManager;
        private readonly DeviceCategoryModelManager _DeviceCategoryManager;
        private readonly RechargeLogModelManager _RechargeLogModelManager;

        public DeviceController()
        {
            _DeviceManager = _DeviceManager ?? new DeviceModelManager();
            _DeviceCategoryManager = _DeviceCategoryManager ?? new DeviceCategoryModelManager();
            _RechargeLogModelManager = _RechargeLogModelManager ?? new RechargeLogModelManager();
        }

        public async Task<ActionResult> Index()
        {
            var items = await _DeviceCategoryManager.QueryAsync(1, 50).ConfigureAwait(false);
            ViewBag.CategoryID = items.Data.Select(item => new SelectListItem { Value = item.ID.ToString(), Text = item.Name }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetList(DeviceQueryViewModel model, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _DeviceManager.QueryAsync(model, pageIndex, pageSize);

                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(DeviceCreateOrUpdateViewModel model)
        {
            return await CreateOrUpdate(model);
        }

        [HttpPost]
        public async Task<JsonResult> Update(DeviceCreateOrUpdateViewModel model)
        {
            return await CreateOrUpdate(model);
        }

        private async Task<JsonResult> CreateOrUpdate(DeviceCreateOrUpdateViewModel model)
        {
            try
            {
                bool result = false;
                if (string.IsNullOrEmpty(model.ID))
                {
                    var device = new DeviceModel
                    {
                        ID = model.ID,
                        Operator = User.Identity.Name,
                        LastUpdateDate = DateTime.Now,
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber,
                        CategoryID = model.CategoryID,
                        Gender = model.Gender,
                        RecordState = model.RecordState,
                        Region = model.Region
                    };
                    result = await _DeviceManager.SaveAsync(device, Guid.NewGuid().ToString());
                }
                else
                {
                    var item = await _DeviceManager.GetByIdAsync(model.ID);
                    if (item == null) throw new Exception("更新实体不存在");

                    item.Name = model.Name;
                    item.PhoneNumber = model.PhoneNumber;
                    item.CategoryID = model.CategoryID;
                    item.Gender = model.Gender;
                    item.RecordState = model.RecordState;
                    item.Address = model.Address;
                    item.Remark = model.Remark;
                    item.Operator = User.Identity.Name;
                    item.LastUpdateDate = DateTime.Now;

                    result = await _DeviceManager.SaveAsync(item);
                }

                return Success(true);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpPost]
        [ActionAuthorize(ActionType = ActionTypes.Update)]
        public async Task<JsonResult> Recharge(RechargeViewModel model)
        {
            try
            {
                var device = await _DeviceManager.GetByIdAsync(model.DeviceId);

                if (device == null) throw new Exception("用户不存在！");

                device.Amount = model.Amount;
                device.Operator = User.Identity.Name;
                device.LastRechargeDate = DateTime.Now;
                device.ExpiryDate = model.ExpiryDate;

                var result = await _DeviceManager.SaveAsync(device);

                var log = new RechargeLogModel
                {
                    Amount = model.Amount,
                    DeviceID = model.DeviceId,
                    ExpiryDate = model.ExpiryDate,
                    Operator = User.Identity.Name
                };

                result = await _RechargeLogModelManager.SaveAsync(log, Guid.NewGuid().ToString());

                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        public ActionResult RechargeLog()
        {
            return View();
        }

        public async Task<JsonResult> GetLogs(RechargeLogQueryViewModel model, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _RechargeLogModelManager.QueryAsync(model, pageIndex, pageSize);
                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.DataBaseError, e.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                var model = await _DeviceManager.GetByIdAsync(id);
                model.Operator = UserId;
                model.LastUpdateDate = DateTime.Now;
                var result = await _DeviceManager.SaveAsync(model);

                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_DeviceCategoryManager != null)
                {
                    _DeviceCategoryManager.Dispose();
                }

                if (_DeviceManager != null)
                {
                    _DeviceManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}