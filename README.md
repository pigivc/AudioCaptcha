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
</ul>

<h2>
Instructions:
</h2>

<ul>
<li>
Add a reference to 'Pigi.Captcha.dll' to your project.
</li>
<li>
Register two httphandlers by adding these two lines to your webconfig at 'system.webserver' section:
<pre>
<handlers>
&lt;handlers&gt;
      &lt;add name="captcha" path="captcha.ashx" type="Pigi.Captcha.captcha,Pigi.Captcha" verb="*" preCondition="integratedMode"/&gt;
      &lt;add name="sayit" path="sayit.ashx" type="Pigi.Captcha.sayit,Pigi.Captcha" verb="*" preCondition="integratedMode"/&gt;
    &lt;/handlers&gt;
    </pre>
</li>
<li>
Also register routing ignore by adding this line to your 'routeConfig' file
<pre>routes.Ignore("{*legecy}", new { legecy = @".*\.(aspx|ashx|asmx|axd|svc)([/\?].*)?" });</pre>
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
