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
using Ncb.ViewModels;

namespace Ncb.DataServices
{
    public class RechargeLogModelManager : BaseManager<NcbDbContext, RechargeLogModel, string>
    {
        public async Task<GeneralResponseModel<List<RechargeLogQueryResultViewModel>>> QueryAsync(RechargeLogQueryViewModel model, int pageIndex, int pageSize, RecordStates state = RecordStates.AuditPass)
        {
            var query = Db.RechargeLogs.Join(Db.Devices, a => a.DeviceID, b => b.ID, (a, b) => new
            {
                DeviceID = a.DeviceID,
                Name = b.Name,
                PhoneNumber = b.PhoneNumber,
                Amount = a.Amount,
                CreateDate = a.CreateDate,
                ExpiryDate = a.ExpiryDate,
                Operator = a.Operator
            });

            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.ID))
                    query = query.Where(m => m.DeviceID.Contains(model.ID));
                if (!string.IsNullOrEmpty(model.Name))
                    query = query.Where(m => m.Name.Contains(model.Name));
                if (!string.IsNullOrEmpty(model.PhoneNumber))
                    query = query.Where(m => m.PhoneNumber.Contains(model.PhoneNumber));
            }
            var list = await query
                          .OrderByDescending(f => f.CreateDate)
                          .Skip(pageSize * (pageIndex - 1))
                          .Take(pageSize)
                          .Select(m => new RechargeLogQueryResultViewModel
                          {
                              Amount = m.Amount,
                              DeviceID = m.DeviceID,
                              ExpiryDate = m.ExpiryDate,
                              PhoneNumber = m.PhoneNumber,
                              Operator = m.Operator,
                              RechargeDate = m.CreateDate,
                              UserName = m.Name
                          })
                          .ToListAsync();

            var result = new GeneralResponseModel<List<RechargeLogQueryResultViewModel>>
            {
                Data = list,
                TotalCount = query.Count()
            };

            return result;
        }
    }
}
