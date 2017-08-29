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
        public GeneralResponseModel<List<ContentListViewModel>> GetList(DateTime? lastTime, int pageIndex, int pageSize)
        {
            using (Db = new NcbDbContext())
            {
                var query = Db.Contents.Where(a=>a.RecordState==RecordStates.AuditPass);
                if (lastTime.HasValue)
                {
                    query = query.Where(a => DateTime.Compare(a.CreateDate, lastTime.Value) > 0);
                }

                var query1 = query
                              .OrderByDescending(f => f.CreateDate)
                              .Skip(pageSize * (pageIndex - 1))
                              .Take(pageSize)
                              .ToList();

                var items = query1.Select(c => new ContentListViewModel
                {
                    Id = c.ID,
                    Author = c.Operator,
                    CreateTime = c.CreateDateDisplay,
                    Title = c.Title,
                    ImageUrl = c.Suffix,
                    IsFree = CheckIsFree(c.AccessType, c.FreeDate),
                    AccessType = c.AccessType,
                    FreeDate = c.FreeDateDisplay
                }).ToList();

                var result = new GeneralResponseModel<List<ContentListViewModel>>
                {
                    Data = items,
                    TotalCount = Db.Contents.Count()
                };

                return result;
            }
        }

        bool CheckIsFree(AccessTypes t, DateTime? freeDate)
        {
            switch (t)
            {
                case AccessTypes.Free:
                    return true;
                case AccessTypes.Pay:
                    return false;
                default:
                    bool isFree = DateTime.Today > freeDate.Value;
                    return isFree;
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
