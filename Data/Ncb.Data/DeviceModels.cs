using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Data;
using System.ComponentModel;
using Framework.Common;

namespace Ncb.Data
{
    [Table("Devices")]
    public partial class DeviceModel : BaseModel<string>
    {
        [Display(Name = "类型")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        public Int16 CategoryID { get; set; }

        [Display(Name = "用户名")]
        [StringLength(10, ErrorMessage = LengthErrMsg)]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public GenderTypes Gender { get; set; } = 0;

        [DisplayName("联系电话")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, ErrorMessage = "{0}必需在{1}个字符以内")]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessage = "{0}格式不正确")]
        public string PhoneNumber { get; set; }

        [Display(Name = "UserAgent")]
        [StringLength(500, ErrorMessage = LengthErrMsg)]
        public string UserAgent { get; set; }

        [DisplayName("省份")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Province { get; set; }

        [DisplayName("城市")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string City { get; set; }

        [DisplayName("县/区")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Region { get; set; }

        [DisplayName("镇")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Town { get; set; }

        [DisplayName("村")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Village { get; set; }

        [DisplayName("详细地址")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = RangeLengthErrMsg)]
        public string Address { get; set; }

        [DisplayName("充值金额")]
        public decimal Amount { get; set; }

        [DisplayName("最后充值时间")]
        public DateTime LastRechargeDate { get; set; }

        [DisplayName("到期日期")]
        public string ExpiryDate { get; set; }

        [DisplayName("最后登录时间")]
        public DateTime LastLoginDate { get; set; }

        [DisplayName("最后更新时间")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("CategoryID")]
        public virtual IEnumerable<DeviceCategoryModel> Categories { get; set; }
    }

    public partial class DeviceModel
    {
        [NotMapped]
        public string LastLoginDateDesplay
        {
            get
            {
                return LastLoginDate.ToString(DateTimeFormetStr);
            }
        }

        [NotMapped]
        public string CategoryName { get; set; }
    }

    [Table("DeviceCategories")]
    public class DeviceCategoryModel : BaseModel<Int16>
    {
        [Display(Name = "分类名")]
        [Required(ErrorMessage =RequiredErrMsg)]
        [StringLength(10, ErrorMessage = LengthErrMsg)]
        public string Name { get; set; }
    }
}
