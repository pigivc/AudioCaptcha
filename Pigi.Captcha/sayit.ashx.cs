using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace Pigi.Captcha
{
    /// <summary>
    /// Summary description for sayit
    /// </summary>
    public class sayit : HttpTaskAsyncHandler, IRequiresSessionState
    {

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            //"".CreateAudio();

            var key = Extensions._captchaPrefix + context.Request.QueryString["id"];
            var cSetting = (CaptchaSettings)HttpContext.Current.Session[key];
            byte[] bytes = new byte[0];
            if (cSetting.EnableAudio)
                bytes = await CaptchaManager.GenerateCurrentCaptachAudio(key);
            //var cbytes = bytes.CompressWav();
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.ContentType = "audio/mp3";
            //context.Response.Write("Hello World");

        }


    }
}