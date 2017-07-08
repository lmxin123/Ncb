using Framework.Common.IO;
using Framework.Common.Json;
using Ncb.Data;
using Ncb.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Framework.Common;

namespace Ncb.Admin.Controllers
{
    public class ContentController : MediaBaseController
    {
        private readonly ContentModelManager _ContentModelManager;
        private static object lockObj = new object();

        public ContentController()
        {
            _ContentModelManager = _ContentModelManager ?? new ContentModelManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetList(ContentModel model, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _ContentModelManager.QueryAsync(model, pageIndex, pageSize);

                result.Data.ForEach(m =>
                {
                    m.Content = null;
                });
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
        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Update(string id)
        {
            var model = await _ContentModelManager.GetByIdAsync(id);
            return View("Create", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(ContentModel model)
        {
            return await CreateOrUpdate(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Update(ContentModel model)
        {
            return await CreateOrUpdate(model);
        }

        private async Task<JsonResult> CreateOrUpdate(ContentModel model)
        {
            try
            {
                model.Operator = User.Identity.Name;

                var id = string.IsNullOrEmpty(model.ID) ? Guid.NewGuid().ToString() : model.ID;
                if (Request.Files.Count > 0)
                {
                    var banner = Request.Files[0];
                    var filePath = (AppSetting.BannerPath + id) + Utility.GetFileSuffix(banner.FileName);
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                    banner.SaveAs(filePath);
                }

                if (string.IsNullOrEmpty(model.ID))
                {
                    await _ContentModelManager.SaveAsync(model, id);
                }
                else
                {
                    await _ContentModelManager.SaveAsync(model);
                }

                return Success(true);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        /// <summary>
        /// 上传封面
        /// </summary>
        /// <param name="id">内容Id</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Upload(string id)
        {
            try
            {
                lock (lockObj)
                {
                    if (string.IsNullOrEmpty(id))
                        throw new ArgumentNullException("文件上传失败，缺少id参数！");

                    FileHelper filesHelper = new FileHelper(AppSetting.BannerPath, Request.Url.Authority);

                    var resultList = filesHelper.Upload(HttpContext);

                    if (resultList.Count > 0)
                    {
                        var banner = resultList.FirstOrDefault();
                        var content = _ContentModelManager.GetById(id);
                        content.Banner = banner.path;

                        var result = _ContentModelManager.SaveAsync(content).Result;

                        var uploadResult = new List<UploadFilesResult>
                        {
                            new UploadFilesResult
                        {
                            deleteUrl = string.Concat("/content/DeleteBanner?id=", id),
                            name = banner.name,
                            size = banner.size,
                            thumbnailUrl = filesHelper.GetThumbUrl(banner.type, banner.name),
                            type = banner.type
                            //  url = item.url
                        }
                        };
                        return Json(uploadResult);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> DeleteFile(string id)
        {
            try
            {
                var content = _ContentModelManager.GetById(id);
                var fileHelper = new FileHelper(AppSetting.BannerPath, Request.Url.Authority);
                string file = fileHelper.GetStorageFullName(content.Banner);
                FileHelper.DeleteFile(file);
                content.Banner = string.Empty;

                await _ContentModelManager.SaveAsync(content);

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                var model = await _ContentModelManager.GetByIdAsync(id);

                model.Operator = UserId;
                await _ContentModelManager.SaveAsync(model);

                await _ContentModelManager.DeleteAsync(id);
                return Success(true);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        public ActionResult Log()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetLogs()
        {
            return Success();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_ContentModelManager != null)
                {
                    _ContentModelManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}