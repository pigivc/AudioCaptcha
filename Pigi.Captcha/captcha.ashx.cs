using System.Web;
using System.Web.SessionState;

namespace Pigi.Captcha
{
    /// <summary>
    /// Summary description for captcha
    /// </summary>
    public class captcha : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var key = Extensions._captchaPrefix + context.Request.QueryString["id"];
            var bmCaptcha = CaptchaManager.GenerateCaptchaImage(key);
            bmCaptcha.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
            context.Response.ContentType = "image/png";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}