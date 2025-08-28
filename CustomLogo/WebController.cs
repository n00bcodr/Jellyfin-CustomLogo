using System;
using System.IO;
using System.Threading.Tasks;
using MediaBrowser.Common.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomLogo
{
    [Route("logo")]
    public class WebController : ControllerBase
    {
        private readonly IApplicationPaths _appPaths;

        public WebController(IApplicationPaths appPaths)
        {
            _appPaths = appPaths;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadLogo()
        {
            try
            {
                var logo = Request.Form.Files["Logo"];
                var bannerDark = Request.Form.Files["BannerDark"];
                var bannerLight = Request.Form.Files["BannerLight"];

                if (logo != null && logo.Length > 0)
                {
                    if (System.IO.File.Exists(Path.Combine(_appPaths.WebPath, "assets/img/icon-transparent.png")))
                    {
                        var path = Path.Combine(_appPaths.WebPath, "assets/img/icon-transparent.png");
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await logo.CopyToAsync(stream);
                        }
                    }

                    string[] files = Directory.GetFiles(_appPaths.WebPath, "icon-transparent*", SearchOption.AllDirectories);

                    foreach (string file in files)
                    {
                        using (var stream = new FileStream(file, FileMode.Create))
                        {
                            await logo.CopyToAsync(stream);
                        }
                    }
                }

                if (bannerDark != null && bannerDark.Length > 0)
                {
                    if (System.IO.File.Exists(Path.Combine(_appPaths.WebPath, "assets/img/banner-dark.png")))
                    {
                        var path = Path.Combine(_appPaths.WebPath, "assets/img/banner-dark.png");
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await bannerDark.CopyToAsync(stream);
                        }
                    }

                    string[] files = Directory.GetFiles(_appPaths.WebPath, "banner-dark*", SearchOption.AllDirectories);

                    foreach (string file in files)
                    {
                        using (var stream = new FileStream(file, FileMode.Create))
                        {
                            await bannerDark.CopyToAsync(stream);
                        }
                    }
                }

                if (bannerLight != null && bannerLight.Length > 0)
                {
                    if (System.IO.File.Exists(Path.Combine(_appPaths.WebPath, "assets/img/banner-light.png")))
                    {
                        var path = Path.Combine(_appPaths.WebPath, "assets/img/banner-light.png");
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await bannerLight.CopyToAsync(stream);
                        }
                    }

                    string[] files = Directory.GetFiles(_appPaths.WebPath, "banner-light*", SearchOption.AllDirectories);

                    foreach (string file in files)
                    {
                        using (var stream = new FileStream(file, FileMode.Create))
                        {
                            await bannerLight.CopyToAsync(stream);
                        }
                    }
                }

                return Content(
                    "<html><head><meta http-equiv='refresh' content='0;url=/web/#/dashboard/plugins' /></head><body>Redirection...</body></html>",
                    "text/html");
            }
            catch (Exception ex)
            {
                // Return a user-friendly error page
                var errorMessage = "<h1>Error Uploading Logo</h1>" +
                                   "<p>Could not save the new logo files. This is likely a <strong>file permissions issue</strong>.</p>" +
                                   "<p>Please ensure the Jellyfin server process has <strong>write permissions</strong> for the Jellyfin web directory (e.g., /usr/share/jellyfin/web/).</p>" +
                                   "<hr>" +
                                   $"<p><strong>Details:</strong> {ex.Message}</p>" +
                                   "<br><br><a href='/web/#/dashboard/plugins'>Click here to return to the Plugins page</a>";
                return new ContentResult
                {
                    Content = errorMessage,
                    ContentType = "text/html",
                    StatusCode = 500
                };
            }
        }
    }
}