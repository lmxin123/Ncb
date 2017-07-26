using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data;
using Framework.Common.Json;
using Framework.Common;

using Ncb.Data;
using Ncb.AppViewModels;
using System.Web;

namespace Ncb.AppDataManager
{
    public class ContentModelManager : BaseManager<NcbDbContext, ContentModel, string>
    {
        public GeneralResponseModel<List<ContentModel>> GetList(DateTime? lastTime, int pageIndex, int pageSize)
        {
            using (Db = new NcbDbContext())
            {
                var query = Db.Contents.AsQueryable();
                if (lastTime.HasValue)
                {
                    query = query.Where(a => DateTime.Compare(a.CreateDate, lastTime.Value) > 0);
                }

                var query1 = query
                              .OrderByDescending(f => f.CreateDate)
                              .Select(c => new
                              {
                                  ID = c.ID,
                                  Operator = c.Operator,
                                  Title = c.Title,
                                  Suffix = c.Suffix,
                                  CreateDate = c.CreateDate
                              })
                              .Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToList();

                var items = query1.Select(a => new ContentModel
                {
                    ID = a.ID,
                    Operator = a.Operator,
                    CreateDate = a.CreateDate,
                    Title = a.Title,
                    Suffix = a.Suffix
                }).ToList();

                var result = new GeneralResponseModel<List<ContentModel>>
                {
                    Data = items,
                    TotalCount = Db.Contents.Count()
                };

                return result;
            }
        }

        public string GetDetail(string id)
        {
            using (Db = new NcbDbContext())
            {
                var content = Db.Contents
                              .Where(c => c.ID == id)
                              .Select(c => c.Content)
                              .FirstOrDefault();

                return content;
            }
        }
    }
}
