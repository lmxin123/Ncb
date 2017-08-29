using Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncb.AppViewModels
{
    public class UserInfoViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string ExpiryDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsExpiring { get; set; }
        public bool IsExpired { get; set; }
    }
}
