using System;
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
    public class DeviceModelManager : BaseManager<NcbDbContext, DeviceModel, string>
    {
        public async Task<GeneralResponseModel<List<DeviceModel>>> QueryAsync(FeebackQueryViewModel model, int pageIndex, int pageSize, RecordStates state = RecordStates.AuditPass)
        {
            if (model == null)
                return await base.QueryAsync(pageIndex, pageSize);
            using (Db = new NcbDbContext())
            {
                var query = Db.Devices.AsQueryable();

                if (!string.IsNullOrEmpty(model.ID))
                    query = query.Where(m => m.ID.StartsWith(model.ID));
                if (!string.IsNullOrEmpty(model.Imei))
                    query = query.Where(m => m.Imei.StartsWith(model.Imei));
                if (!string.IsNullOrEmpty(model.Model))
                    query = query.Where(m => m.Model.StartsWith(model.Model));
                if (!string.IsNullOrEmpty(model.OsVersion))
                    query = query.Where(m => m.OsVersion.StartsWith(model.OsVersion));
                if (!string.IsNullOrEmpty(model.PlusVersion))
                    query = query.Where(m => m.OsVersion.StartsWith(model.PlusVersion));
                if (!string.IsNullOrEmpty(model.QueryText))
                    query = query.Where(m => m.OsVersion.StartsWith(model.PlusVersion));

                var list = await query
                              .OrderByDescending(f => f.CreateDate)
                              .Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToListAsync();

                var result = new GeneralResponseModel<List<DeviceModel>>
                {
                    Data = list,
                    TotalCount = query.Count()
                };

                return result;
            }
        }

        public async Task<GeneralResponseModel<List<DeviceModel>>> QueryAsync(string queryText, int pageIndex, int pageSize, RecordStates state = RecordStates.AuditPass)
        {
            if (string.IsNullOrEmpty(queryText))
                return await base.QueryAsync(pageIndex, pageSize);

            using (Db = new NcbDbContext())
            {
                var query = Db.Devices.AsQueryable();

                if (!string.IsNullOrEmpty(queryText))
                {
                    query = query.Where(m => m.ID.Contains(queryText) ||
                    m.AppVersion.Contains(queryText) ||
                    m.Model.Contains(queryText) ||
                    m.OsVersion.Contains(queryText) ||
                    m.Imei.Contains(queryText) ||
                    m.PlusVersion.Contains(queryText) ||
                    m.UserAgent.Contains(queryText));
                }

                var list = await query
                              .OrderByDescending(f => f.CreateDate)
                              .Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToListAsync();

                var result = new GeneralResponseModel<List<DeviceModel>>
                {
                    Data = list,
                    TotalCount = query.Count()
                };

                return result;
            }
        }
    }
}
