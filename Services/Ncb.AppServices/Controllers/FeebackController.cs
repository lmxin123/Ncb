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
    public class FeebackController : BaseController
    {
        private readonly FeebackModelManager _FeebackModelManager;

        public FeebackController()
        {
            _FeebackModelManager = _FeebackModelManager ?? new FeebackModelManager();
        }

        [HttpPost]
        public async Task<JsonResult> Create(CreateFeebackViewModel model)
        {
            try
            {
                bool result = false;

                var item = await _FeebackModelManager.GetByIdAsync(model.Mac);

                var device = new FeeBackModel
                {
                   Contact=model.Contact,
                   Mac=model.Mac,
                   Question=model.Question,
                   Star=model.Star
                };
                if (item == null)
                {
                    result = await _FeebackModelManager.SaveAsync(device, Guid.NewGuid().ToString());
                }
                else
                {
                    result = await _FeebackModelManager.SaveAsync(device);
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
                if (_FeebackModelManager != null)
                {
                    _FeebackModelManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}