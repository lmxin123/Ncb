using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Framework.Data;
using Framework.Common.Json;
using Ncb.Data;
using Ncb.Data.Views;
using Framework.Common;
using Ncb.AdminViewModels;

namespace Ncb.DataManager
{
    public class UserDeviceInfoViewManager
    {
        public async Task<GeneralResponseModel<List<View_UserDeviceInfos>>> QueryAsync(UserInfoQueryViewModel model, int pageIndex, int pageSize)
        {
            var list = new List<View_UserDeviceInfos>();
            StringBuilder sqlBuilder = new StringBuilder("WITH T AS (SELECT row_number() OVER(ORDER BY CreateDate DESC)  AS num,* ");
            sqlBuilder.AppendFormat("FROM dbo.View_UserDeviceInfos WHERE 1=1 ");
            if (model.RecordState > 0)
                sqlBuilder.AppendFormat(" AND RecordState ={0}", (int)model.RecordState);
            if (!string.IsNullOrEmpty(model.QueryText))
                sqlBuilder.AppendFormat("AND (Name LIKE '%{0}%' OR PhoneNumber LIKE '%{0}%') ", model.Name);
            if (model.StartDate != default(DateTime))
                sqlBuilder.AppendFormat("AND CreateTime >='{0}' ", model.StartDate);
            if (model.EndDate != default(DateTime))
                sqlBuilder.AppendFormat("AND CreateTime <='{0}'", model.EndDate);
            sqlBuilder.AppendFormat(") SELECT * FROM T WHERE num BETWEEN {0} AND {1} ", pageSize * (pageIndex - 1), pageSize);

            using (var db = new NcbDbContext())
            {
                list = db.Database.SqlQuery<View_UserDeviceInfos>(sqlBuilder.ToString()).ToList();
                int totalCount = await db.Database.ExecuteSqlCommandAsync("SELECT COUNT(*) FROM dbo.View_UserDeviceInfos").ConfigureAwait(false);
                var result = new GeneralResponseModel<List<View_UserDeviceInfos>>
                {
                    Data = list,
                    TotalCount = totalCount
                };

                return result;
            }
        }
    }
}
