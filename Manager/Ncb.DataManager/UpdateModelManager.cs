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
    public class UpdateModelManager : BaseManager<NcbDbContext, UpdateModel, string>
    {
        public UpdateModel GetLast()
        {
            using (Db = new NcbDbContext())
            {
                var item = Db.Updates
                    .OrderByDescending(a => a.CreateDate)
                    .Take(1)
                    .FirstOrDefault();

                return item;
            }
        }
    }
}
