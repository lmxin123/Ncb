using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Ncb.AppServices
{
    public class AppSetting
    {
        static NameValueCollection settings = ConfigurationManager.AppSettings;
        /// <summary>
        /// 
        /// </summary>
        public static string FeebackPath
        {
            get
            {
                return settings["FeebackPath"];
            }
        }
    }
}