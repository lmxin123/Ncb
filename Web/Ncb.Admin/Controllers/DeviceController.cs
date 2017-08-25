﻿using Framework.Auth;
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
    public class DeviceController : AdminBaseController
    {
        private readonly DeviceModelManager _DeviceManager;
        private readonly UserInfoModelManager _UserInfoModelManager;
        private readonly UserCategoryModelManager _UserCategoryManager;
        private readonly RechargeLogModelManager _RechargeLogModelManager;

        public DeviceController()
        {
            _DeviceManager = _DeviceManager ?? new DeviceModelManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetList(string name,string phoneNumber, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _DeviceManager.QueryAsync(model, pageIndex, pageSize);

                return Success(result);
            }
            catch (Exception e)
            {
                return Fail(ErrorCode.ProcessError, e.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                var model = await _DeviceManager.GetByIdAsync(id);
                model.Operator = UserId;
                model.LastUpdateDate = DateTime.Now;
                var result = await _DeviceManager.SaveAsync(model);

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
                if (_DeviceManager != null)
                {
                    _DeviceManager.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}