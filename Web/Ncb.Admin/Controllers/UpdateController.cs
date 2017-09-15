using Framework.Common.IO;
using Framework.Common.Json;
using Framework.Common.Mvc;
using Ncb.DataManager;
using Ncb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Framework.Common;

namespace Ncb.Admin.Controllers
{
    public class UpdateController : BaseController
    {
        private readonly UpdateModelManager _UpdateModelManager;
        private readonly UpdateLogModelManager _UpdateLogModelManager;

        public UpdateController()
        {
            _UpdateModelManager = _UpdateModelManager ?? new UpdateModelManager();
            _UpdateLogModelManager = _UpdateLogModelManager ?? new UpdateLogModelManager();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Dowload(string mac)
        {
            try
            {
                var item = _UpdateModelManager.GetLast();

                item.DowloadCount += 1;
                await _UpdateModelManager.SaveAsync(item);

                if (string.IsNullOrEmpty(mac))
                {
                    mac = Utility.GetMACAddress();
                    var log = new UpdateLogModel
                    {
                        Mac = mac ?? "get mac faild",
                        Version = item.Version
                    };
                    await _UpdateLogModelManager.SaveAsync(log, Guid.NewGuid().ToString());
                }
                var path = Server.MapPath("/content/package/") + item.AppName;
                return File(path, "application/vnd.android.package-archive", item.AppName);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.RequestParamError, e.Message);
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_UpdateModelManager != null)
                {
                    _UpdateModelManager.Dispose();
                }

                if (_UpdateLogModelManager != null)
                {
                    _UpdateLogModelManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}