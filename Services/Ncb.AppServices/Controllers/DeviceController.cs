using Framework.Common.Json;
using Framework.Common.Mvc;
using Ncb.AppDataManager;
using Ncb.AppViewModels;
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
        public async Task<JsonResult> Create(CreateDeviceViewModel model)
        {
            try
            {
                bool result = false;

                var item = await _DeviceManager.GetByIdAsync(model.Id);

                var device = new DeviceModel
                {
                    LastUpdateDate = DateTime.Now,
                    AppVersion = model.AppVersion,
                    Imei = model.Imei,
                    Model = model.Model,
                    Net = model.Net,
                    OsVersion = model.OsVersion,
                    PlusVersion = model.PlusVersion,
                    UserAgent = model.UserAgent
                };
                if (item == null)
                {
                    result = await _DeviceManager.SaveAsync(device, model.Id);
                }
                else
                {
                    device.ID = model.Id;
                    result = await _DeviceManager.SaveAsync(device);
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