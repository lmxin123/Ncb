using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Linq;

namespace Ncb.Data
{
    public class NcbDbContext : DbContext
    {
        public NcbDbContext() : base("NcbConnection")
        {
        }

        public static NcbDbContext Create()
        {
            return new NcbDbContext();
        }

        public DbSet<ContentModel> Contents { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<UserInfoModel> UserInfos { get; set; }
        public DbSet<UserCategoryModel> UserCategories { get; set; }
        public DbSet<RechargeLogModel> RechargeLogs { get; set; }
        public DbSet<FeeBackModel> FeeBacks { get; set; }
        public DbSet<UpdateModel> Updates { get; set; }
        public DbSet<UpdateLogModel> UpdateLogs { get; set; }

    }
}
