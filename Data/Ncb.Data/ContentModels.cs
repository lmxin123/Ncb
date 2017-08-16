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
        public DateTime? FreeDate { get; set; }

        /// <summary>
        /// 封面图片的后缀
        /// </summary>
        [StringLength(50)]
        public string Suffix { get; set; }

        [NotMapped]
        [Display(Name = "封面图片")]
        public string Banner
        {
            get
            {
                return ID + Suffix;
            }
        }

        [NotMapped]
        public string AccessTypeDisplay
        {
            get
            {
                return AccessType.GetDisplayName();
            }
        }

        [NotMapped]
        public string FreeDateDisplay
        {
            get
            {
                return FreeDate?.ToString(DateFormetStr);
            }
        }

        [NotMapped]
        public override string CreateDateDisplay
        {
            get
            {
                return CreateDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
        }

    }
}
