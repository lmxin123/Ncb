using Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncb.AppViewModels
{
    public class UpdateViewModel
    {
        public string Title { get; set; }
        public string Note { get; set; }
        public string DowloadUrl { get; set; }
        public bool Updated { get; set; }
        public string Size { get; set; }
        public string Version { get; set; }
    }

    public class UpdateCheckModel
    {
        public string Mac { get; set; }
        public string AppId { get; set; }
        public string Version { get; set; }
        public string Imei { get; set; }
    }
}
