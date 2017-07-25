using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Data;
using Framework.Common.Json;
using Framework.Common;

using Ncb.Data;
using Ncb.AppServices.Models;
using System.Web;

namespace Ncb.AppServices.Manager
{
    public class DeviceModelManager : BaseManager<NcbDbContext, DeviceModel, string>
    {
    }
}
