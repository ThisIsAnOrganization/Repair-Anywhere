using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepairAnywhere.Controllers
{
    public class CommonController : Controller
    {

        public CommonController()
        {

        }

        public ActionResult index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        public ActionResult forgetpass()
        {
            return View();
        }

        public ActionResult register()
        {
            return View();
        }
    }
}
