using System.Drawing;
using System.Text.RegularExpressions;
using System.Text;

namespace Projekt.NET.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ImageMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string url = httpContext.Request.Path;
            string watermark = "Projekt.NET";

            Regex rx = new Regex(@"^/.*\.(jpg|jpeg|png|gif)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (rx.Matches(url).Count != 0)
            {
                String f = "./img" + url;


                if (File.Exists(f))
                {
                    Image img = Image.FromFile(f);

                    //Znak wodny
                    Graphics grp = Graphics.FromImage(img);
                    Brush brush = new SolidBrush(Color.Black);
                    Font font = new System.Drawing.Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Pixel);
                    SizeF textSize = new SizeF();
                    textSize = grp.MeasureString(watermark, font);

                    Point position = new Point((img.Width - ((int)textSize.Width + 10)), (img.Height - ((int)textSize.Height + 10)));
                    grp.DrawString(watermark, font, brush, position);


                    //Wyświetla obrazek
                    MemoryStream stream = new MemoryStream();
                    img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    httpContext.Response.ContentType = "image/jpeg";
                    return httpContext.Response.Body.WriteAsync(stream.ToArray(), 0, (int)stream.Length);
                }
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseImageMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageMiddleware>();
        }
    }
}
