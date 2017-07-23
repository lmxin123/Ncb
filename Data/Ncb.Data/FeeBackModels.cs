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
    [Table("FeeBacks")]
    public partial class FeeBackModel : BaseModel<string>
    {
        [Display(Name = "Mac地址")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Mac { get; set; }

        [Display(Name = "AppId")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string AppId { get; set; }

        [Display(Name = "设备标识")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Imei { get; set; }

        [Display(Name = "图片文件")]
        public string Images { get; set; }

        [DisplayName("联系方式")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, ErrorMessage = "{0}必需在{1}个字符以内")]
        public string Contact { get; set; }

        [DisplayName("平台类型")]
        public DeviceTypes DeviceType { get; set; }

        [DisplayName("设备型号")]
        [StringLength(50, ErrorMessage = "{0}必需在{1}个字符以内")]
        public string Model { get; set; }

        [Display(Name = "UserAgent")]
        [StringLength(500, ErrorMessage = LengthErrMsg)]
        public string UserAgent { get; set; }

        [DisplayName("app版本")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string AppVersion { get; set; }

        [DisplayName("基座版本号")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string PlusVersion { get; set; }

        [DisplayName("操作系统版本")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string OsVersion { get; set; }

        [DisplayName("网络类型")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Net { get; set; }

        [DisplayName("反馈内容")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Question { get; set; }

        [DisplayName("评分")]
        public int Star { get; set; }
    }
}
