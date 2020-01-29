using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Pigi.Captcha
{
    public static class Initializer
    {
        public static void AddPigiCaptchaHandlers(this RouteCollection collection)
        {
            collection.Add(new Route("sayit.ashx", new sayit()));
            collection.Add(new Route("captcha.ashx", new captcha()));
        }
    }
}
