using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Data;
using Framework.Common;
using Framework.Common.Extensions;

namespace Ncb.Data
{
    [Table("Contents")]
    public class ContentModel : BaseModel<string>
    {
        [Display(Name = "标题")]
        [Required(ErrorMessage = RequiredErrMsg)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = RangeLengthErrMsg)]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = LengthErrMsg)]
        [Display(Name = "子标题")]
        public string SubTitle { get; set; }

        [Display(Name = "内容")]
        [Required(ErrorMessage = RequiredErrMsg)]
        public string Content { get; set; }

        [Display(Name = "内容类型")]
        public AccessTypes AccessType { get; set; }

        [Display(Name = "免费日期")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = LengthErrMsg)]
        public string FreeDate { get; set; }

        [NotMapped]
        [Display(Name = "封面图片")]
        public string Banner { get; set; }

        [NotMapped]
        public string AccessTypeDisplay
        {
            get
            {
                return AccessType.GetDisplayName();
            }
        }

    }
}
