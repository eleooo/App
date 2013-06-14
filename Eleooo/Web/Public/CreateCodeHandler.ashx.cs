using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    /// <summary>
    /// CreateCode 的摘要说明
    /// </summary>
    public class CreateCodeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            this.CreateCheckCodeImage(Utilities.GenerateCheckCode(4), context);
        }
        private void CreateCheckCodeImage(string checkCode, HttpContext context)
        {
            if ((checkCode != null) && (checkCode.Trim( ) != string.Empty))
            {
                context.Response.Cookies.Add(new HttpCookie("CheckCode", checkCode));
                Bitmap image = new Bitmap((int)Math.Ceiling((double)(checkCode.Length * 12.5)), 0x16);
                Graphics graphics = Graphics.FromImage(image);
                try
                {
                    Random random = new Random( );
                    graphics.Clear(Color.White);
                    for (int i = 0; i < 0x19; i++)
                    {
                        int num2 = random.Next(image.Width);
                        int num3 = random.Next(image.Width);
                        int num4 = random.Next(image.Height);
                        int num5 = random.Next(image.Height);
                        graphics.DrawLine(new Pen(Color.Silver), num2, num4, num3, num5);
                    }
                    Font font = new Font("Arial", 12f, FontStyle.Italic | FontStyle.Bold);
                    LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                    graphics.DrawString(checkCode, font, brush, (float)2f, (float)2f);
                    for (int j = 0; j < 100; j++)
                    {
                        int x = random.Next(image.Width);
                        int y = random.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(random.Next( )));
                    }
                    graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                    MemoryStream stream = new MemoryStream( );
                    image.Save(stream, ImageFormat.Gif);
                    context.Response.ClearContent( );
                    context.Response.ContentType = "image/jpeg";
                    context.Response.BinaryWrite(stream.ToArray( ));
                }
                finally
                {
                    graphics.Dispose( );
                    image.Dispose( );
                }
            }
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