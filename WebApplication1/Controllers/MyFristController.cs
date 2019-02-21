using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class MyFristController : Controller
    {
        // GET: MyFrist
        public ActionResult Index()
        {
            return View();
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            var aa = filterContext.HttpContext.ToString();
        }
    }
}