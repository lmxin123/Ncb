using Framework.Common.Json;
using Framework.Common.Mvc;
using Ncb.AppServices.Manager;
using Ncb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ncb.AppServices.Controllers
{
    public class DeviceController : BaseController
    {
        private readonly DeviceModelManager _DeviceManager;

        public DeviceController()
        {
            _DeviceManager = _DeviceManager ?? new DeviceModelManager();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_DeviceManager != null)
                {
                    _DeviceManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}