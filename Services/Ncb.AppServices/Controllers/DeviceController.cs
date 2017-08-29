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
        private readonly UserInfoModelManager _UserInfoModelManager;

        public DeviceController()
        {
            _DeviceManager = _DeviceManager ?? new DeviceModelManager();
            _UserInfoModelManager = _UserInfoModelManager ?? new UserInfoModelManager();
        }

        [HttpPost]
        public async Task<JsonResult> Create(CreateDeviceViewModel device)
        {
            try
            {
                bool result = false;
                var item = new DeviceModel
                {
                    LastUpdateDate = DateTime.Now,
                    Imei = device.Imei,
                    Model = device.Model,
                    NetType = device.NetType,
                    AppVersion = device.AppVersion,
                    OsVersion = device.OsVersion,
                    PlusVersion = device.PlusVersion,
                    UserAgent = Request.UserAgent,
                    Vendor = device.Vendor
                };
                var existItem = await _DeviceManager.GetByIdAsync(device.Id);
                if (existItem == null)
                {
                    result = await _DeviceManager.SaveAsync(item, device.Id);
                }
                else
                {
                    item.ID = device.Id;
                    item.LastUpdateDate = DateTime.Now;
                    result = await _DeviceManager.SaveAsync(item);
                }

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