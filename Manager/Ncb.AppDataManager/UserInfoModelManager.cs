using Framework.Data;
using Ncb.AppViewModels;
using Ncb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncb.AppDataManager
{
    public class UserInfoModelManager : BaseManager<NcbDbContext, UserInfoModel, string>
    {
        public async Task<UserInfoViewModel> GetUserInfoAsync(string id)
        {
            var item = await GetByIdAsync(id);
            if (item == null)
            {
                item = new UserInfoModel
                {
                    CategoryID = 1,
                    LastLoginDate = DateTime.Now
                };
                await SaveAsync(item, id);
            }
            else
            {
                item.LastLoginDate = DateTime.Now;
                await SaveAsync(item);
            }

            var result = new UserInfoViewModel
            {
                ID = item.ID,
                Name = item.Name,
                PhoneNumber = item.PhoneNumber
            };

            if (item.CategoryID == 1)
            {
                result.IsPaid = false;
            }
            else
            {
                if (item.ExpiryDate.HasValue)
                {
                    int expDays = (item.ExpiryDate.Value - DateTime.Today).Days;
                    result.IsExpiring = expDays <= 7;
                    result.IsExpired = expDays <= 0;
                }
            }
            return result;
        }
    }
}
