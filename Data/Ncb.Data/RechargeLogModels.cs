using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Data;

namespace Ncb.Data
{
    [Table("RechargeLogs")]
    public partial class RechargeLogModel : BaseModel<string>
    {
        [Required(ErrorMessage = RequiredErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string DeviceID { get; set; }

        [Display(Name = "充值金额")]
        [Required(ErrorMessage = RequiredErrMsg)]
        public decimal Amount { get; set; }

        [Display(Name = "到期日期")]
        [Required(ErrorMessage = RequiredErrMsg)]
        public DateTime ExpiryDate { get; set; }

        [ForeignKey("DeviceID")]
        protected virtual IEnumerable<DeviceModel> Devices { get; set; }
    }
}
