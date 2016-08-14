#region MIT License

/*
 * Copyright (c) 2016 Marcelo Lv Cabral (http://github.com/lvcabral)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
 * 
 */

#endregion

using System.IO;
using sspack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace popsc
{
    internal class Util
    {
        internal static List<string> images;
        
        internal static bool packSprites(string sheetFile, string mapFile = "")
        {
            bool result = true;
            // generate our output
            ImagePacker imagePacker = new ImagePacker();
            Bitmap outputSheet;
            Dictionary<string, Rectangle> outputMap;

            try
            {
                // pack the image, generating a map only if desired
                if (imagePacker.PackImage(images, false, true, 1024, 1024, 3, mapFile != "" , out outputSheet, out outputMap) == 0)
                {
                    outputSheet.Save(sheetFile);
                    if (mapFile != "") saveMap(mapFile, outputMap);
                    Console.WriteLine("Generated Sprite Sheet: {0}.", sheetFile);
                }
                else
                {
                    Console.WriteLine("There was an error making the image sheet {0}.", sheetFile);
                    result = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving sheet: {0} {1}", sheetFile, e.Message);
                result = false;
            }
            return result;
        }

        internal static void saveMap(string filename, Dictionary<string, Rectangle> map)
        {
            // copy the files list and sort alphabetically
            string[] keys = new string[map.Count];
            map.Keys.CopyTo(keys, 0);
            List<string> outputFiles = new List<string>(keys);
            outputFiles.Sort();
            bool first = true;
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("{\"frames\": {");
                foreach (var image in outputFiles)
                {
                    if (!first)
                    {
                        writer.WriteLine("},");
                    }
                    // get the destination rectangle
                    Rectangle destination = map[image];

                    // write out the destination rectangle for this bitmap
                    writer.WriteLine(string.Format("\"{0}\":", Path.GetFileNameWithoutExtension(image)));
                    writer.WriteLine("{");
                    writer.WriteLine("\t\"frame\": {" + string.Format("\"x\":{0} ,\"y\":{1},\"w\":{2},\"h\":{3}",
                                                                    destination.X,
                                                                    destination.Y,
                                                                    destination.Width,
                                                                    destination.Height) + "}");
                    first = false;
                }
                writer.WriteLine("}}");
                writer.WriteLine("}");
            }
        }

        internal static Bitmap getTransparentBitmap(string filePath)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(filePath);
            Color transparent;
            if (bitmap.Palette.Entries.Count() > 0)
            {
                ColorPalette palette = bitmap.Palette;
                if (palette.Entries[0] == Color.Black)
                {
                    for (int i = 1; i < palette.Entries.Count(); i++)
                    {
                        if (palette.Entries[i] == Color.Black)
                        {
                            palette.Entries[i] = Color.FromArgb(1, 1, 1);
                        }
                    }
                }
                bitmap.Palette = palette;
                transparent = bitmap.Palette.Entries[0];
            }
            else
            {
                transparent = Color.Black;
            }
            bitmap.MakeTransparent(transparent);
            return bitmap;
        }

        internal static bool convertBitmap(string input, string output, bool transparent = true)
        {
            bool result = true;
            try
            {
                Bitmap bitmap;
                if (transparent)
                {
                    bitmap = getTransparentBitmap(input);
                }
                else
                {
                    bitmap = (Bitmap)Image.FromFile(input);
                }
                bitmap.Save(output);
                images.Add(output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error converting image: {0} {1}", input, e.Message);
            }
            return result;
        }

        internal static bool convertShadow(string input, string output)
        {
            bool result = true;
            try
            {
                Bitmap shadow = getTransparentBitmap(input);
                Bitmap bitmap = new Bitmap(shadow.Width, shadow.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawImage(shadow, new Rectangle(0, 0, shadow.Width, shadow.Height),
                                                    new Rectangle(1,0,shadow.Width, shadow.Height), 
                                                    GraphicsUnit.Pixel);
                }
                Bitmap xorbmp = BitmapXOR(shadow, bitmap);
                xorbmp.Save(output);
                images.Add(output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error converting shadow frame: {0} {1}", input, e.Message);
            }
            return result;
        }

        internal static bool clipBitmap(string input, string output, Rectangle[] rects)
        {
            bool result = true;
            try
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(input);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    foreach (Rectangle rect in rects)
                    {
                        g.SetClip(rect, CombineMode.Replace);
                        g.Clear(Color.Transparent);
                    }
                }
                bitmap.Save(output);
                images.Add(output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error converting image palette: {0} {1}", input, e.Message);
            }
            return result;
        }

        internal static bool convertBitmapPalette(string input, string output, Color[] colors, bool transparent = true)
        {
            bool result = true;
            try
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(input);
                ColorPalette palette = bitmap.Palette;
                for (int i = 0; i < Math.Min(palette.Entries.Count(), colors.Count()); i++) palette.Entries[i] = colors[i];
                bitmap.Palette = palette;
                if (transparent) bitmap.MakeTransparent(colors[0]);
                bitmap.Save(output);
                images.Add(output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error converting image palette: {0} {1}", input, e.Message);
            }
            return result;
        }

        internal static bool convertBitmapColor(string input, string output, Color color, int index = -1)
        {
            bool result = true;
            try
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(input);
                ColorPalette palette = bitmap.Palette;
                if (index < 0) index = palette.Entries.Count() - 1;
                palette.Entries[index] = color;
                bitmap.Palette = palette;
                bitmap.MakeTransparent(palette.Entries[0]);
                bitmap.Save(output);
                images.Add(output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error converting image palette: {0} {1}", input, e.Message);
            }
            return result;
        }

        internal static Color[] readPalette(string filePath, int index = 0)
        {
            List<Color> entries = new List<Color>();
            int counter = 0;
            int start = index*16 + 4;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                counter++;
                if (counter == 1)
                {
                    if (line.Substring(0,8) != "JASC-PAL") break;
                }
                else if (counter >= start && counter <= start + 15)
                {
                    string[] colors = line.Split(' ');
                    entries.Add(Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2])));
                }
            }
            file.Close();
            return entries.ToArray();
        }

        internal static Bitmap BitmapXOR(Bitmap bmpimg, Bitmap bmp)
        {
            BitmapData bmpData = bmpimg.LockBits(new Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData bmpData2 = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int width = Math.Max(bmpData.Width, bmpData2.Width);
            int height = Math.Max(bmpData.Height,  bmpData2.Height);

            bmpimg.UnlockBits(bmpData);
            bmp.UnlockBits(bmpData2);

            Bitmap bit1 = new Bitmap(bmpimg, width, height);
            Bitmap bit2 = new Bitmap(bmp, width, height);

            Bitmap bmpresult = new Bitmap(width, height);

            BitmapData data1 = bit1.LockBits(new Rectangle(0, 0, bit1.Width, bit1.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData data2 = bit2.LockBits(new Rectangle(0, 0, bit2.Width, bit2.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            BitmapData data3 = bmpresult.LockBits(new Rectangle(0, 0, bmpresult.Width, bmpresult.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int k = 1;
            unsafe
            {
                int remain1 = data1.Stride - data1.Width * 4;
                int remain2 = data2.Stride - data2.Width * 4;
                int remain3 = data3.Stride - data3.Width * 4;


                byte* ptr1 = (byte*)data1.Scan0;
                byte* ptr2 = (byte*)data2.Scan0;
                byte* ptr3 = (byte*)data3.Scan0;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width * 4; j++)
                    {
                        if (k != 4)
                        {
                            ptr3[0] = (byte)(ByteXOR(ptr1[0], ptr2[0]));
                        }
                        else
                        {
                            ptr3[0] = ptr1[0]; 
                        }
                        ptr1++;
                        ptr2++;
                        ptr3++;
                        k++;
                        if (k > 4) k = 1;
                    }
                    ptr1 += remain1;
                    ptr2 += remain2;
                    ptr3 += remain3;
                }
            }
            bit1.UnlockBits(data1);
            bit2.UnlockBits(data2);
            bmpresult.UnlockBits(data3);
            return bmpresult;
        }

        internal static byte ByteXOR(byte a, byte b)
        {
            byte A = (byte)(255 - a);
            byte B = (byte)(255 - b);
            return (byte)((a & B) | (A & b));
        }
    }
}
