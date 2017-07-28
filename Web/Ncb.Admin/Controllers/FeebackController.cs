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
    public class FeebackController : AdminBaseController
    {
        private readonly FeebackModelManager _FeebackModelManager;

        public FeebackController()
        {
            _FeebackModelManager = _FeebackModelManager ?? new FeebackModelManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetList(FeebackQueryViewModel model, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _FeebackModelManager.QueryAsync(model, pageIndex, pageSize);

                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadImage(string mac)
        {
            try
            {
                if(Request.Files.Count>0)
                {

                }
                var model = await _FeebackModelManager.GetByIdAsync(mac);
                var result = await _FeebackModelManager.SaveAsync(model);

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
                if (_FeebackModelManager != null)
                {
                    _FeebackModelManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}