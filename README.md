# AudioCaptcha

This is a simple to use Audio Captcha made for ASP.NET MVC
<H2>Requirements</H2>
<ul>
<li>
jquery
</li>
<li>
bootstrap
</li>
<li>
SessionState must be enabled (default is)
</li>
      <li>
Copying 'libmp3lame.32.dll' and 'libmp3lame.32.dll' in bin project
            (included in nuget package)
</li>
</ul>

<h2>
Instructions:
</h2>

<ul>
<li>
Add a reference to 'Pigi.Captcha.dll' to your project.
      or install using nuget package manager :
      <pre>Install-Package Pigi.Captcha.Mvc</pre>
</li>
<li>
      add <pre>using Pigi.Captcha</pre> namespace to top of global.asax.cs
Add Captcha httphandlers to route table by calling this method.
<pre>
RouteTable.Routes.AddPigiCaptchaHandlers();
    </pre>
      <b>***Note***</b> call to this method should be before calling to <pre>RouteConfig.RegisterRoutes(RouteTable.Routes);</pre>
</li>

<li>
Make sure that you added packages 'NAudio' and 'NAudio.Lame' to your project and see if these two dlls are included in bin folder :
'libmp3lame.64.dll'
'libmp3lame.32.dll'
They are used for audio compression purpose.
</li>
<li>
Add <pre>@using Pigi.Captcha</pre> to the top of your view.cshtml
</li>
<li>
Add this helper method to generate Audio Captcha.
<pre>@Html.Captcha(new CaptchaSettings { Id = "c1" })</pre>
you can also customize more by setting 'CaptchaSettings' object properties, like image size, captcha style, enabling captcha user input...
</li>
</ul>
<img src='https://github.com/pigivc/Pigi.Captcha/blob/master/Images/pigi.captcha.png' />
