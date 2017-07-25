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
        [Display(Name = "手机型号")]
        [StringLength(10, ErrorMessage = LengthErrMsg)]
        public string Model { get; set; }

        [Display(Name = "UserAgent")]
        [StringLength(500, ErrorMessage = LengthErrMsg)]
        public string UserAgent { get; set; }

        [Display(Name = "设备标识")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Imei { get; set; }

        [DisplayName("平台类型")]
        public DeviceTypes DeviceType { get; set; }

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

        [DisplayName("最后更新时间")]
        public DateTime LastUpdateDate { get; set; }
    }
}
