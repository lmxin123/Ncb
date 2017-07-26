using Ncb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncb.AdminViewModels
{
    public class RechargeLogQueryViewModel : UserInfoModel
    {
        public string QueryText { get; set; }
        [Display(Name ="充值日期")]
        public DateTime StartDate { get; set; }
        [Display(Name = "到")]
        public DateTime EndDate { get; set; }
    }

    public class RechargeLogQueryResultViewModel
    {
        [DisplayName("Mac")]
        public string DeviceID { get; set; }
        [DisplayName("名称")]
        public string UserName { get; set; }
        [DisplayName("手机号码")]
        public string PhoneNumber { get; set; }
        [DisplayName("充值金额")]
        public decimal Amount { get; set; }
        [DisplayName("充值日期")]
        public DateTime RechargeDate { get; set; }
        [DisplayName("到期时间")]
        public string ExpiryDate { get; set; }
        [DisplayName("操作人")]
        public string Operator { get; set; }
    }
}
