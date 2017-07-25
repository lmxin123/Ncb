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

        [Display(Name = "图片文件")]
        public string Images { get; set; }

        [DisplayName("联系方式")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, ErrorMessage = "{0}必需在{1}个字符以内")]
        public string Contact { get; set; }

        [DisplayName("反馈内容")]
        [StringLength(50, ErrorMessage = LengthErrMsg)]
        public string Question { get; set; }

        [DisplayName("评分")]
        public int Star { get; set; }
    }
}
