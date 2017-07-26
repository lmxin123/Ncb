using Framework.Common;
using Framework.Data;
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
    public class DeviceQueryViewModel : DeviceModel
    {
        public string QueryText { get; set; }
        [Display(Name ="注册日期")]
        public DateTime StartDate { get; set; }
        [Display(Name = "到")]
        public DateTime EndDate { get; set; }
    }

    public class DeviceCreateOrUpdateViewModel: ModelBase
    {
        [Display(Name = "Mac地址")]
        public string ID { get; set; }

        [Display(Name = "用户类型")]
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

        public RecordStates RecordState { get; set; }

        [DisplayName("备注")]
        public string Remark { get; set; }
    }
}
