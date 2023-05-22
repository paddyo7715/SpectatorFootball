using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System;

namespace SpectatorFootball
{
    public class Uniform_Image
    {
        private Bitmap stock_image = null;
        public Bitmap Home_Uniform_image = null;
        public Bitmap Away_Uniform_Image = null;

        private int debug_file_id = 0;

        public Uniform_Image(string stock_image_file_path)
        {
            stock_image = new Bitmap(stock_image_file_path);

            Home_Uniform_image = (Bitmap) stock_image.Clone();
            Away_Uniform_Image = (Bitmap) stock_image.Clone();
        }
        public BitmapImage getHomeUniform_Image()
        {
            MemoryStream stream = new MemoryStream();

            Home_Uniform_image.Save(stream, ImageFormat.Png); // Was .Bmp, but this did Not show a transparent background.

            stream.Position = 0;
            BitmapImage result = new BitmapImage();
            result.BeginInit();
            // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
            // Force the bitmap to load right now so we can dispose the stream.
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
        }
        public BitmapImage GetAwayUniform_Image()
        {
            MemoryStream stream = new MemoryStream();

            Away_Uniform_Image.Save(stream, ImageFormat.Png); // Was .Bmp, but this did Not show a transparent background.

            stream.Position = 0;
            BitmapImage result = new BitmapImage();
            result.BeginInit();
            // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
            // Force the bitmap to load right now so we can dispose the stream.
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
        }
        public void Flip_All_Colors(bool bHome, Color helmet_color, Color fasemask_color, Color hel_logo_color, Color jersey_color, Color number_color, Color num_outline_color, Color sleeve_color, Color shoulder_stripe_color, Color sleeve_stripe_1_color, Color sleeve_stripe_2_color, Color sleeve_stripe_3_color, Color sleeve_stripe_4_color, Color sleeve_stripe_5_color, Color sleeve_stripe_6_color, Color pants_color, Color pants_stripe_1_color, Color pants_stripe_2_color, Color pants_stripe_3_color, Color socks_color, Color cleats_color)
        {
            int x;
            int y;
            byte red;
            byte green;
            byte blue;

            Bitmap img = null;

            if (bHome)
                img = Home_Uniform_image;
            else
                img = Away_Uniform_Image;
            int loopTo = stock_image.Width - 1;
            for (x = 0; x <= loopTo; x++)
            {
                int loopTo1 = stock_image.Height - 1;
                for (y = 0; y <= loopTo1; y++)
                {
                    red = stock_image.GetPixel(x, y).R;
                    green = stock_image.GetPixel(x, y).G;
                    blue = stock_image.GetPixel(x, y).B;

                    System.Drawing.Color the_color = Color.FromArgb(red, green, blue);

                    if (the_color.ToArgb() == app_Constants.STOCK_HELMET_COLOR.ToArgb())
                        img.SetPixel(x, y, helmet_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_FACEMASK.ToArgb())
                        img.SetPixel(x, y, fasemask_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_HEL_LOGO_COLOR.ToArgb())
                        img.SetPixel(x, y, hel_logo_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_JERSEY_COLOR.ToArgb())
                        img.SetPixel(x, y, jersey_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_NUMBER_COLOR.ToArgb())
                        img.SetPixel(x, y, number_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_NUM_OUTLINE_COLOR.ToArgb())
                        img.SetPixel(x, y, num_outline_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SLEEVE_COLOR.ToArgb())
                        img.SetPixel(x, y, sleeve_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SHOULDER_STRIPE_COLOR.ToArgb())
                        img.SetPixel(x, y, shoulder_stripe_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SLEEVE_STRIPE_1_COLOR.ToArgb())
                        img.SetPixel(x, y, sleeve_stripe_1_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SLEEVE_STRIPE_2_COLOR.ToArgb())
                        img.SetPixel(x, y, sleeve_stripe_2_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SLEEVE_STRIPE_3_COLOR.ToArgb())
                        img.SetPixel(x, y, sleeve_stripe_3_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SLEEVE_STRIPE_4_COLOR.ToArgb())
                        img.SetPixel(x, y, sleeve_stripe_4_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SLEEVE_STRIPE_5_COLOR.ToArgb())
                        img.SetPixel(x, y, sleeve_stripe_5_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SLEEVE_STRIPE_6_COLOR.ToArgb())
                        img.SetPixel(x, y, sleeve_stripe_6_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_PANTS_COLOR.ToArgb())
                        img.SetPixel(x, y, pants_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_PANTS_STRIPE_1_COLOR.ToArgb())
                        img.SetPixel(x, y, pants_stripe_1_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_PANTS_STRIPE_2_COLOR.ToArgb())
                        img.SetPixel(x, y, pants_stripe_2_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_PANTS_STRIPE_3_COLOR.ToArgb())
                        img.SetPixel(x, y, pants_stripe_3_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_SOCKS_COLOR.ToArgb())
                        img.SetPixel(x, y, socks_color);

                    if (the_color.ToArgb() == app_Constants.STOCK_CLEATES_COLOR.ToArgb())
                        img.SetPixel(x, y, cleats_color);
                }
            }
        }
        public void Flip_One_Color(bool bHome, Color old_color, Color new_color)
        {
            int x;
            int y;
            byte red;
            byte green;
            byte blue;

            Bitmap img = null;
            if (bHome)
                img = Home_Uniform_image;
            else
                img = Away_Uniform_Image;
            int loopTo = stock_image.Width - 1;
            for (x = 0; x <= loopTo; x++)
            {
                int loopTo1 = stock_image.Height - 1;
                for (y = 0; y <= loopTo1; y++)
                {
                    red = stock_image.GetPixel(x, y).R;
                    green = stock_image.GetPixel(x, y).G;
                    blue = stock_image.GetPixel(x, y).B;

                    System.Drawing.Color the_color = Color.FromArgb(red, green, blue);

                    if (the_color.ToArgb() == old_color.ToArgb())
                        img.SetPixel(x, y, new_color);
                }
            }
        }
        public BitmapImage getFirstBitmap()
        {
//                        Home_Uniform_image.Save("C:\\Database\\spritesheet.png", ImageFormat.Png);

            Rectangle subRect = new Rectangle(50, 0, 50, 50);

            Bitmap subBitmap;
            subBitmap = new Bitmap(50, 50, Home_Uniform_image.PixelFormat);

            Graphics g = Graphics.FromImage(subBitmap);
            g.DrawImage(Home_Uniform_image, 0, 0, subRect, GraphicsUnit.Pixel);
            g.Dispose();
//            Home_Uniform_image.Save("C:\\Database\\spritesheet2.png", ImageFormat.Png);

            return CommonUtils.ConvertBMtoBitmapImage(subBitmap);
        }
    }
}
