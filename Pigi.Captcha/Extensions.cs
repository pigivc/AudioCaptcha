using NAudio.Lame;
using NAudio.MediaFoundation;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Pigi.Captcha
{
    public static class Extensions
    {
        internal static string _captchaPrefix = "captchaPrefix";

        internal async static Task<byte[]> CreateAudio(this string text,bool doCompress = true)
        {
            Task<byte[]> task = Task.Run(() => {

                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                using (MemoryStream streamAudio = new MemoryStream())
                {
                    //System.Media.SoundPlayer m_SoundPlayer = new System.Media.SoundPlayer();

                    synth.SetOutputToWaveStream(streamAudio);
                    synth.Rate = -2;


                    foreach (var charc in text)
                    {
                        synth.Speak(charc.ToString());
                    }

                    // Speak a phrase.  
                    streamAudio.Position = 0;
                    //m_SoundPlayer.Stream = streamAudio;
                    //m_SoundPlayer.Play();

                    // Set the synthesizer output to null to release the stream.   
                    synth.SetOutputToNull();


                    if(doCompress)
                    {
                        return streamAudio.ToArray().CompressWav().Result;
                    }
                    else
                    // Insert code to persist or process the stream contents here.  
                    return streamAudio.ToArray();
                }
            });
            // Initialize a new instance of the speech synthesizer.  
            return await task;
        }

        private static Task<byte[]> CompressWav(this byte[] wav)
        {
            var task = Task.Run(() =>
            {

                try
                {
                    using (var ms = new MemoryStream(wav))
                    {

                        using (var reader = new WaveFileReader(ms))
                        {
                            var newFormat = new WaveFormat(8000, 16, 1);

                            using (var conversionStream = new WaveFormatConversionStream(newFormat, reader))
                            {
                                using (var outms = new MemoryStream())
                                {
                                    using (var writer = new LameMP3FileWriter(outms, conversionStream.WaveFormat, 24, null))
                                    {
                                        conversionStream.CopyTo(writer);
                                        
                                        return outms.ToArray();
                                    }
                                    //WaveFileWriter.WriteWavFileToStream(outms, conversionStream);
                                    //return outms.ToArray();
                                }


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    throw;
                }

            });
            return task;
        }

        public static MvcHtmlString Captcha(this HtmlHelper htmlHelper,CaptchaSettings settings)
        {
            var cs = htmlHelper.ViewData["captchaScript"];
            if (cs == null)
                htmlHelper.ViewData.Add("captchaScript", true);
            if(cs == null)
            {
                RegisterBinPath();
            }

            htmlHelper.ViewContext.RequestContext.HttpContext.Session[_captchaPrefix + settings.Id] = settings;

            StringBuilder sb = new StringBuilder();
            if(cs == null)
            sb.Append(@"<style>.lol { text-decoration: none!important;cursor: pointer;} " +
                "#capTbl tbody tr td a {color: black;}</style>");
            if(settings.ShowInput)
            sb.Append("<input id=\""+settings.Id+"\"  name=\""+settings.Id+"\" type=\"text\" class=\"form-control\" style=\"float:left;width:"+settings.PicWidth+"px\"/>");
            sb.Append("<div  style=\"display: flex; float:left; align-items:center\">");
            sb.Append("<img id=\"img"+settings.Id+"\" src=\"/captcha.ashx?id="+settings.Id+"\" />");
            sb.Append("<table id=\"capTbl\" style=\"color: black; margin-left:5px\">");
            if (settings.EnableAudio)
                sb.Append("<tr><td><a class=\"lol glyphicon glyphicon-volume-up\" title=\"Speak!\" onclick=\"play('" + settings.Id+"')\"></a></td></tr>");
            
            sb.Append("<tr><td><a class=\"lol glyphicon glyphicon-refresh\" title=\"Refresh\" onclick=\"refresh('"+settings.Id+"')\"></a></td></tr>");
            sb.Append("</table></div>");

            if (cs == null)
            {
                sb.Append("<script> var audioDic = {};function refresh(id) {var aud = audioDic[id];if (aud) { aud.pause(); aud.currentTime = 0;}" +
            "audioDic[id] = undefined;" +
        "$(\"#img\"+id).attr('src', '/captcha.ashx?id='+id+'&'+Math.random()) }");
                //if(settings.EnableAudio)
                sb.Append("var play = function(id) {var aud = audioDic[id];if (aud == undefined)" +
                        "aud = new Audio('/sayit.ashx?id=' + id);"+
                    "aud.pause();aud.currentTime = 0; aud.play(); audioDic[id] = aud;}");
                sb.Append("</script>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        static void RegisterBinPath()
        {
            var binPath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory, "bin" });
            // get current search path from environment
            var path = Environment.GetEnvironmentVariable("PATH") ?? "";

            // add 'bin' folder to search path if not already present
            if (!path.Split(Path.PathSeparator).Contains(binPath, StringComparer.CurrentCultureIgnoreCase))
            {
                path = string.Join(Path.PathSeparator.ToString(), new string[] { path, binPath });
                Environment.SetEnvironmentVariable("PATH", path);
            }
        }
    }
}
