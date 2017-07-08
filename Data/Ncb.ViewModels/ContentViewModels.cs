using Ncb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncb.ViewModels
{
    public class ContentQueryViewModel : ContentModel
    {
        public string QueryText { get; set; }
        [Display(Name ="发布日期")]
        public DateTime StartDate { get; set; }
        [Display(Name = "到")]
        public DateTime EndDate { get; set; }
    }
}
