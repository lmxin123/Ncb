using Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncb.AppViewModels
{
    public class CreateDeviceViewModel
    {
        public string Id { get; set; }
        public string AppId { get; set; }
        public string Imei { get; set; }
        public DeviceTypes Platform { get; set; }
        public string Model { get; set; }
        public string AppVersion { get; set; }
        public string PlusVersion { get; set; }
        public string OsVersion { get; set; }
        public NetTypes NetType { get; set; }
        public string Vendor { get; set; }
    }
}
