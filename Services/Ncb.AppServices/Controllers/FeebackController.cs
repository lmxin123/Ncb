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

                var feeback = new FeeBackModel
                {
                    Contact = model.Contact,
                    Mac = model.Mac,
                    Question = model.Question,
                    Star = model.Star
                };
                if (Request.Files.Count > 0)
                {
                    string[] nameList = FileHelper.SaveFile(Request, AppSetting.FeebackPath);
                    feeback.Images = string.Join(",", nameList);
                }
                result = await _FeebackModelManager.SaveAsync(feeback, Guid.NewGuid().ToString());

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