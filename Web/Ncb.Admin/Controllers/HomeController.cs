﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ncb.Admin.Controllers
{
    public class HomeController : MediaBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
