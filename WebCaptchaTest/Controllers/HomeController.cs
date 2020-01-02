using Pigi.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCaptchaTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string c1,string c2)
        {
            var isCaptcha1Valid = CaptchaManager.ValidateCurrentCaptcha("c1", c1);
            var isCaptcha2Valid = CaptchaManager.ValidateCurrentCaptcha("c2", c2);

            ViewBag.c1 = isCaptcha1Valid;
            ViewBag.c2 = isCaptcha2Valid;
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}