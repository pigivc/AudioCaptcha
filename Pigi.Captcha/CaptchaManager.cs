using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Pigi.Captcha
{
    public class CaptchaManager
    {
        public static Bitmap GenerateCaptchaImage(string key)
        {
            var cSettings = (CaptchaSettings)HttpContext.Current.Session[key];
            var text = CaptchaCode.GenerateCode(cSettings.TextLength, cSettings.TextStyle);
            cSettings.CaptchaText = text;
            return new CaptchaImage().MakeCaptchaImge(text, cSettings.PicHeight - 10, cSettings.PicHeight - 1, cSettings.PicWidth, cSettings.PicHeight,cSettings.CaptchaStyle);
        }

        public static async Task<byte[]> GenerateCurrentCaptachAudio(string key)
        {
            var text = ((CaptchaSettings)HttpContext.Current.Session[key]).CaptchaText;
            if (string.IsNullOrEmpty(text))
                return await Task.Run(() => new byte[0]);

            return await text.CreateAudio();
        }

        public static bool ValidateCurrentCaptcha(string captchaId, string userCaptchaText)
        {
            var text = ((CaptchaSettings)HttpContext.Current.Session[Extensions._captchaPrefix + captchaId]).CaptchaText;
            return text == userCaptchaText.ToUpper();
        }
    }
}