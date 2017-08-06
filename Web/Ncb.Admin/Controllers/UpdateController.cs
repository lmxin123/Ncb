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
        public async Task<ActionResult> Dowload(string mac)
        {
            try
            {
                var item = _UpdateModelManager.GetLast();

                item.DowloadCount += 1;
                await _UpdateModelManager.SaveAsync(item);

                if (string.IsNullOrEmpty(mac))
                {
                    var log = new UpdateLogModel
                    {
                        Mac = mac,
                        Version = item.Version
                    };
                    await _UpdateLogModelManager.SaveAsync(log, Guid.NewGuid().ToString());
                }
                var path = Server.MapPath("/content/package/") + item.AppName;
                return File(path, "application/vnd.android.package-archive");
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