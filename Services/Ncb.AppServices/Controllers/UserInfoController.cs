﻿using Framework.Common.Json;
using Framework.Common.Mvc;
using Ncb.AppDataManager;
using Ncb.AppViewModels;
using Ncb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace Ncb.AppServices.Controllers
{
    public class UserInfoController : BaseController
    {
        private readonly UserInfoModelManager _UserInfoModelManager;

        public UserInfoController()
        {
            _UserInfoModelManager = _UserInfoModelManager ?? new UserInfoModelManager();
        }

        [HttpPost]
        public async Task<JsonResult> SetAccount(string id, string name, string phone)
        {
            try
            {
                var item = await _UserInfoModelManager.GetByIdAsync(id);
                if (item == null)
                    throw new Exception($"{id}无效！");
                item.Name = name;
                item.PhoneNumber = phone;
                var result = await _UserInfoModelManager.SaveAsync(item);

                return Success(true);
            }
            catch(DbEntityValidationException e)
            {
                List<string> errMsg =new List<string>();
                e.EntityValidationErrors.ToList().ForEach(a =>
                {
                    errMsg.AddRange( a.ValidationErrors.Select(b => b.ErrorMessage));
                });
                return Fail(ErrorCode.ModelValidateError, string.Join(";", errMsg));
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetUserInfo(string id)
        {
            try
            {
                var item = await _UserInfoModelManager.GetByIdAsync(id);
                if (item == null)
                {
                    return Fail(ErrorCode.AuthFailError, "用户不存在！");
                }
                else
                {
                    item.LastLoginDate = DateTime.Now;
                    await _UserInfoModelManager.SaveAsync(item);

                    return Success(new
                    {
                        item.ID,
                        item.Name,
                        item.PhoneNumber,
                        item.Amount,
                        item.ExpiryDate,
                        item.CategoryID
                    });
                }
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
                if (_UserInfoModelManager != null)
                {
                    _UserInfoModelManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}