using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Framework.Common.Json;
using Framework.Common.Mvc;

using Ncb.Data;
using Ncb.AppViewModels;
using Ncb.AppDataManager;
using System.Threading.Tasks;

namespace Ncb.AppServices.Controllers
{
    public class ContentController : BaseController
    {
        private readonly ContentModelManager _ContentModelManager;

        public ContentController()
        {
            _ContentModelManager = _ContentModelManager ?? new ContentModelManager();
        }

        [HttpGet]
        public JsonResult GetList(DateTime? lastTime)
        {
            try
            {
                var result = _ContentModelManager.GetList(lastTime, 1, 30);
               
                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpGet]
        public async Task<ContentResult> GetContent(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("参数无效");

                var item = await _ContentModelManager.GetByIdAsync(id);
                if (item == null)
                    throw new Exception("内容不存在！");

                return Content(item.Content);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}
