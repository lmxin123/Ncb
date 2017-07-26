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
                var items = _ContentModelManager.GetList(lastTime, 1, 30);
                var result = items.Data.Select(a => new ContentListViewModel
                {
                    Id = a.ID,
                    Author = a.Operator,
                    CreateTime = a.CreateDateDisplay,
                    Title = a.Title,
                    ImageUrl = AppSetting.BannerUrl + a.Banner
                }).ToList();
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
