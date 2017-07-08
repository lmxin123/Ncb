using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Data;
using Framework.Common;

namespace Ncb.Data
{
    [Table("Accounts")]
    public class AccountModel : BaseModel<string>
    {
        [Display(Name = "总额")]
        [Required(ErrorMessage = RequiredErrMsg)]
        public decimal Total { get; set; }

        [Display(Name = "余额")]
        public decimal Balance { get; set; }

        [Display(Name = "开始日期")]
        [Required(ErrorMessage = RequiredErrMsg)]
        [StringLength(10,MinimumLength =10,ErrorMessage =LengthErrMsg)]
        public string StartDate { get; set; }

        [Display(Name = "到期日期")]
        [Required(ErrorMessage = RequiredErrMsg)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = LengthErrMsg)]
        public string EndDate { get; set; }

        [Display(Name = "支付方式")]
        [Required(ErrorMessage =RequiredSelectErrMsg)]
        public PayTypes PayType { get; set; }

        [Display(Name = "最后充值时间")]
        public DateTime LastRechargeDate { get; set; } = DateTime.Now;
    }

    public class AccountLogModels : BaseModel<string>
    {
        [Display(Name = "Mac地址")]
        [Required(ErrorMessage = RequiredErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Mac { get; set; }

        [Display(Name = "充值金额")]
        [Required(ErrorMessage = RequiredErrMsg)]
        public decimal Amount { get; set; }
    }
}
