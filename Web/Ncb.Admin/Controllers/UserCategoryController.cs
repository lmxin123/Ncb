using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Framework.Common.Json;
using Ncb.Data;
using Ncb.DataManager;

namespace Ncb.Admin.Controllers
{
    public class UserCategoryController : AdminBaseController
    {
        private readonly UserInfoModelManager _UserModelManager;
        private readonly UserCategoryModelManager _UserCategoryModelManager;

        public UserCategoryController()
        {
            _UserModelManager = _UserModelManager ?? new UserInfoModelManager();
            _UserCategoryModelManager = _UserCategoryModelManager ?? new UserCategoryModelManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<JsonResult> ValidateNameExist(Int16 id, string name)
        {
            if (string.IsNullOrEmpty(name))
                return Json(false, JsonRequestBehavior.AllowGet);

            bool isExists = await _UserCategoryModelManager.GetByExpreAsync(item =>
             item.Name == name.Trim().Replace("\t", "") && item.ID != id) == null;

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> GetList(int pageIndex, int pageSize)
        {
            try
            {
                var result = await _UserCategoryModelManager.QueryAsync(pageIndex, pageSize);

                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Create(UserCategoryModel model)
        {
            return await CreateOrUpdate(model);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UserCategoryModel model)
        {
            return await CreateOrUpdate(model);
        }

        private async Task<JsonResult> CreateOrUpdate(UserCategoryModel model)
        {
            try
            {
                var validateResult = await ValidateNameExist(model.ID, model.Name);
                if (validateResult.Data.ToString() == bool.FalseString)
                    throw new Exception("分类名称己存在！");

                var result = false;
                model.Operator = User.Identity.Name;
                if (model.ID == 0)
                {
                    var categories = await _UserCategoryModelManager.GetAllAsync();
                    var maxId = categories.Count == 0 ? 1 : categories.Max(a => a.ID) + 1;
                    result = await _UserCategoryModelManager.SaveAsync(model, (Int16)maxId);
                }
                else
                {
                    result = await _UserCategoryModelManager.SaveAsync(model);
                }
                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(Int16 id)
        {
            try
            {
                var device = await _UserModelManager.GetByExpreAsync(item => item.CategoryID == id);
                if (device != null)
                    throw new Exception("此分类下包含用户数据，不允许删除！");

                var model = await _UserCategoryModelManager.GetByIdAsync(id);
                model.Operator = UserId;

                var result = await _UserCategoryModelManager.DeleteAsync(model);

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
                if (_UserCategoryModelManager != null)
                {
                    _UserCategoryModelManager.Dispose();
                }
                if (_UserModelManager != null)
                {
                    _UserModelManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}