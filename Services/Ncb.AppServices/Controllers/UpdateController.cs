using Framework.Common.IO;
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
        public JsonResult Check(UpdateCheckModel model)
        {
            try
            {
                var result = _UpdateModelManager.Check(model);

                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.DataError, e.Message);
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