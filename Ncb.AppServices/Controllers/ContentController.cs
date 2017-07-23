using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Ncb.Data;
using Ncb.AppServices.Manager;
using Framework.Common.Json;
using Ncb.AppServices.Models;
using Framework.Common.Mvc;
using System.Web.Mvc;

namespace Ncb.AppServices.Controllers
{
    public class ContentController : BaseController
    {
        private readonly ContentModelManager _ContentModelManager;

        public ContentController()
        {
            _ContentModelManager = _ContentModelManager ?? new ContentModelManager();
        }

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

        public JsonResult Get(string id)
        {
            try
            {
                var result = _ContentModelManager.GetDetail(id);
                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }
    }
}
