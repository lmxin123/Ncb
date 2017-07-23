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
    [Table("Updates")]
    public class UpdateModel : BaseModel<string>
    {
        [Display(Name = "版本")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Version { get; set; }

        [Display(Name = "安装包名称")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string AppName { get; set; }

        [Display(Name = "AppId")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string AppId { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Title { get; set; }

        [DisplayName("更新描述")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(500, ErrorMessage = "{0}必需在{1}个字符以内")]
        public string Note { get; set; }

        [DisplayName("下载地址")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(500, ErrorMessage = "{0}必需在{1}个字符以内")]
        public string DowloadUrl { get; set; }

        [DisplayName("下载次数")]
        public int DowloadCount { get; set; }

        [DisplayName("更新包大小")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        public string Size { get; set; }
    }

    [Table("UpdateLogs")]
    public class UpdateLogModel : BaseModel<string>
    {
        [Display(Name = "版本")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Version { get; set; }

        [Display(Name = "Mac地址")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Mac { get; set; }

        [Display(Name = "AppId")]
        [Required(ErrorMessage = RequiredSelectErrMsg)]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string AppId { get; set; }

        [Display(Name = "UserAgent")]
        [StringLength(500, ErrorMessage = LengthErrMsg)]
        public string UserAgent { get; set; }

        [Display(Name = "设备标识")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Imei { get; set; }

        [DisplayName("平台类型")]
        public DeviceTypes DeviceType { get; set; }

        [DisplayName("设备型号")]
        [StringLength(50, ErrorMessage = "{0}必需在{1}个字符以内")]
        public string Model { get; set; }

        [DisplayName("基座版本号")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string PlusVersion { get; set; }

        [DisplayName("操作系统版本")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string OsVersion { get; set; }

        [DisplayName("网络类型")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Net { get; set; }
    }
}
