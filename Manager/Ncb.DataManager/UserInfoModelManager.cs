﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Framework.Data;
using Framework.Common.Json;
using Ncb.Data;
using Framework.Common;
using Ncb.AdminViewModels;

namespace Ncb.DataManager
{
    public class UserInfoModelManager : BaseManager<NcbDbContext, UserInfoModel, string>
    {
        public async Task<GeneralResponseModel<List<UserInfoModel>>> QueryAsync(UserInfoQueryViewModel model, int pageIndex, int pageSize, RecordStates state = RecordStates.AuditPass)
        {
            if (model == null)
                return await base.QueryAsync(pageIndex, pageSize);
            using (Db = new NcbDbContext())
            {
                var query = Db.UserInfos.AsQueryable();

                if (!string.IsNullOrEmpty(model.ID))
                    query = query.Where(m => m.ID.StartsWith(model.ID));
                if (!string.IsNullOrEmpty(model.Name))
                    query = query.Where(m => m.Name.StartsWith(model.Name));
                if (!string.IsNullOrEmpty(model.PhoneNumber))
                    query = query.Where(m => m.PhoneNumber.StartsWith(model.PhoneNumber));

                var list = await query
                              .OrderByDescending(f => f.CreateDate)
                              .Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToListAsync();

                var categories = await Db.UserCategories.ToListAsync();
                list.ForEach(m =>
                {
                    var category = categories.FirstOrDefault(c => c.ID == m.CategoryID);
                    m.CategoryName = category != null ? category.Name : string.Empty;
                });
                var result = new GeneralResponseModel<List<UserInfoModel>>
                {
                    Data = list,
                    TotalCount = query.Count()
                };

                return result;
            }
        }
    }
}
