using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Framework.Data;
using Ncb.AppViewModels;
using Ncb.Data;

namespace Ncb.AppDataManager
{
    public class UpdateModelManager : BaseManager<NcbDbContext, UpdateModel, string>
    {
        public UpdateViewModel Check(UpdateCheckModel model)
        {
            var result = new UpdateViewModel();
            using (Db = new NcbDbContext())
            {
                var item = Db.Updates
                    .Where(a => a.AppId == model.AppId)
                    .OrderByDescending(a => a.CreateDate)
                    .Take(1)
                    .FirstOrDefault();

                if (item != null)
                {
                    result.DowloadUrl = item.DowloadUrl;
                    result.Note = item.Note;
                    result.Title = item.Title;
                    result.Size = item.Size;
                    result.Updated = true;
                }
                return result;
            }
        }
    }

    public class UpdateLogModelManager : BaseManager<NcbDbContext, UpdateLogModel, string>
    {

    }
}
