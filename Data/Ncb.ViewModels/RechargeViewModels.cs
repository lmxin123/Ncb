using Ncb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncb.ViewModels
{
    public class RechargeViewModel
    {
        [DisplayName("Mac地址")]
        public string DeviceId { get; set; }

        [DisplayName("充值金额")]
        [Required(ErrorMessage = "请输入{0}")]
        [Range(1,10000,ErrorMessage = "请输入{1}到{2}之间的有效数字")]
        public decimal Amount { get; set; }

        [DisplayName("到期日期")]
        [Required(ErrorMessage = "请输入{0}")]
        public string ExpiryDate { get; set; }
    }
}
