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
    public class FeebackModelManager : BaseManager<NcbDbContext, FeeBackModel, string>
    {
        public async Task<GeneralResponseModel<List<FeebackQueryResultViewModel>>> QueryAsync(FeebackQueryViewModel model, int pageIndex, int pageSize, RecordStates state = RecordStates.AuditPass)
        {
            //if (model == null)
            //    return await base.QueryAsync(pageIndex, pageSize);
            using (Db = new NcbDbContext())
            {
                var query = Db.FeeBacks.AsQueryable();

                if (!string.IsNullOrEmpty(model.QueryText))
                    query = query.Where(m => m.ID.StartsWith(model.QueryText));


                var list = await query
                              .OrderByDescending(f => f.CreateDate)
                              .Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToListAsync();

                var result = new GeneralResponseModel<List<FeebackQueryResultViewModel>>
                {
                    Data = null,
                    TotalCount = query.Count()
                };

                return result;
            }
        }
    }
}
