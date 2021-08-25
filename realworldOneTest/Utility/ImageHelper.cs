using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace realworldOneTest.Utility
{
    public static class ImageHelper
    {
        /// <summary>
        /// To rotate the image we need to 
        /// a) first convert the array of bytes to bitmap
        /// b) call RotateFlip method of bitmap
        /// c) convert the bitmap back to byte array
        /// </summary>
        /// <param name="imageByteArray"></param>
        /// <returns></returns>
        public static byte[] RotateImage(byte[] imageByteArray)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Imaging.ImageFormat imageFormat = System.Drawing.Imaging.ImageFormat.Png;

                // convert the array of bytes to bitmap
                Bitmap bitmap = ConvertFromBytes(imageByteArray);

                // To flip the image upside down...
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

                // convert the bitmap back to byte array
                return ToByteArray(bitmap, imageFormat);
            }
        }

        public static Bitmap ConvertFromBytes(Byte[] imagebytes)
        {
            return new Bitmap(new MemoryStream(imagebytes));
        }

        /// <summary>
        /// Extension method
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}
