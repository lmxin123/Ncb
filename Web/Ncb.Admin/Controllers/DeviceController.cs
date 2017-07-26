using Framework.Auth;
using Framework.Common.Json;
using Ncb.Data;
using Ncb.DataManager;
using Ncb.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ncb.Admin.Controllers
{
    public class DeviceController : AdminBaseController
    {
        private readonly DeviceModelManager _DeviceManager;

        public DeviceController()
        {
            _DeviceManager = _DeviceManager ?? new DeviceModelManager();
        }

        public async Task<ActionResult> Index()
        {
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
                if (_DeviceManager != null)
                {
                    _DeviceManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}