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
    public class UserInfoQueryViewModel : UserInfoModel
    {
        public string QueryText { get; set; }
        [Display(Name ="注册日期")]
        public DateTime StartDate { get; set; }
        [Display(Name = "到")]
        public DateTime EndDate { get; set; }
    }
}
