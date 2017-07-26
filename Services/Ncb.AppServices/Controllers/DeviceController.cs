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
        public async Task<JsonResult> Create(DeviceModel model)
        {
            try
            {
                bool result = false;

                var item = await _DeviceManager.GetByIdAsync(model.ID);
                var device = new DeviceModel
                {
                    Operator = User.Identity.Name,
                    LastUpdateDate = DateTime.Now,
                    AppVersion = model.AppVersion,
                    DeviceType = model.DeviceType,
                    Imei = model.Imei,
                    Model = model.Model,
                    RecordState = model.RecordState,
                    Net = model.Net,
                    OsVersion = model.OsVersion,
                    PlusVersion = model.PlusVersion,
                    UserAgent = model.UserAgent
                };
                if (item == null)
                {
                    result = await _DeviceManager.SaveAsync(device, model.ID);
                }
                else
                {
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