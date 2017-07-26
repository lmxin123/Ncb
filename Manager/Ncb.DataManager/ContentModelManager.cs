using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data;
using Ncb.Data;
using Framework.Common.Json;
using Framework.Common;

namespace Ncb.DataManager
{
    public class ContentModelManager : BaseManager<NcbDbContext, ContentModel, string>
    {
        public async Task<GeneralResponseModel<List<ContentModel>>> QueryAsync(ContentModel model, int pageIndex, int pageSize, RecordStates state = RecordStates.AuditPass)
        {
            if (model == null)
                return await base.QueryAsync(pageIndex, pageSize);
            using (Db = new NcbDbContext())
            {
                var query = Db.Contents.AsQueryable();

                if (!string.IsNullOrEmpty(model.ID))
                    query = query.Where(m => m.ID.Contains(model.ID));
                if (!string.IsNullOrEmpty(model.Title))
                    query = query.Where(m => m.Content.Contains(model.Title));
                if (!string.IsNullOrEmpty(model.Content))
                    query = query.Where(m => m.Content.Contains(model.Content));

                var list = query
                              .OrderByDescending(f => f.CreateDate)
                              .Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToList();

                var result = new GeneralResponseModel<List<ContentModel>>
                {
                    Data = list,
                    TotalCount = query.Count()
                };

                return result;
            }
        }
    }
}
