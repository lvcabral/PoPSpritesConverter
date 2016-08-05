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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace popsc
{
    internal class General
    {
        internal static bool convertGeneral(string inputPath, string outputPath)
        {
            bool result = true;
            // Local variables
            string bmpPath = "", bmpName = "", pngName, sheetPath;
            string swordPath = Path.Combine(outputPath, "sword");
            if (!Directory.Exists(swordPath))
            {
                Directory.CreateDirectory(swordPath);
            }
            string genPath = Path.Combine(outputPath, "general");
            if (!Directory.Exists(genPath))
            {
                Directory.CreateDirectory(genPath);
            }
            Color[] colors;
            Dictionary<string, string> dict;
            Object[] types = new Object[]
            {
                new Object[2] {@"prince\sword","sword"},
                new Object[2] {@"prince\fire","fire"},
                new Object[2] {@"prince\potions\bubble animation","bubble"},
                new Object[2] {@"vdungeon\chomper\blood","blood"}
            };
            try
            {
                Util.images = new List<string>();
                for (int g = 0; g < types.Count(); g++)
                {
                    Object[] type = (Object[])types[g];
                    bmpPath = Path.Combine(inputPath, type[0].ToString());
                    dict = getResourceDict(type[1].ToString());
                    foreach (var file in dict)
                    {
                        bmpName = file.Value;
                        pngName = file.Key;
                        if (type[1].ToString() == "sword")
                        {
                            result = Util.convertBitmap(Path.Combine(bmpPath, bmpName), Path.Combine(swordPath, pngName));
                        }
                        else if (type[1].ToString() == "blood")
                        {
                            colors = new Color[2] { Color.Black, Color.FromArgb(228, 0, 0) };
                            result = Util.convertBitmapPalette(Path.Combine(bmpPath, bmpName), Path.Combine(genPath, pngName), colors);
                        }
                        else if (type[1].ToString() == "bubble")
                        {
                            colors = new Color[2] { Color.Black, Color.FromArgb(224, 0, 48) };
                            result = Util.convertBitmapPalette(Path.Combine(bmpPath, bmpName), Path.Combine(genPath, pngName + "_red.png"), colors);
                            colors = new Color[2] { Color.Black, Color.FromArgb(85, 255, 85) };
                            result = Util.convertBitmapPalette(Path.Combine(bmpPath, bmpName), Path.Combine(genPath, pngName + "_green.png"), colors);
                            colors = new Color[2] { Color.Black, Color.FromArgb(85, 85, 255) };
                            result = Util.convertBitmapPalette(Path.Combine(bmpPath, bmpName), Path.Combine(genPath, pngName + "_blue.png"), colors);
                        }
                        else
                        {
                            result = Util.convertBitmap(Path.Combine(bmpPath, bmpName), Path.Combine(genPath, pngName));
                        }
                        if (!result) break;
                        Console.WriteLine("General frame converted: {0}", Path.Combine(genPath, pngName));
                    }
                    if (!result) break;
                    if (type[1].ToString() == "sword")
                    {
                        sheetPath = Path.Combine(outputPath, "sword");
                        result = Util.packSprites(sheetPath + ".png", sheetPath + ".json");
                        Util.images = new List<string>();
                    }                    
                    if (!result) break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting general: {0} {1}", Path.Combine(bmpPath, bmpName), ex.Message);
                result = false;
            }
            sheetPath = Path.Combine(outputPath, "general");
            try
            {
                // Add files generated in other objects
                Util.images.Add(Path.Combine(genPath,"kid-splash.png"));
                Util.images.Add(Path.Combine(genPath,"kid-live.png"));
                Util.images.Add(Path.Combine(genPath,"kid-emptylive.png"));
                string[] guards = new string[3] { "fatguard", "shadow", "vizier" };
                for (int g = 0; g < 7; g++)
                {
                    Util.images.Add(Path.Combine(genPath, "guard" + (g + 1).ToString() +"-splash.png"));
                    Util.images.Add(Path.Combine(genPath, "guard" + (g + 1).ToString() +"-live.png"));
                    if (g < 3)
                    {
                        Util.images.Add(Path.Combine(genPath, guards[g] + "-splash.png"));
                        Util.images.Add(Path.Combine(genPath, guards[g] + "-live.png"));
                    }
                }
                // Create sprite sheet
                result = Util.packSprites(sheetPath + ".png", sheetPath + ".json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error packaging general sheet: {0} {1}", sheetPath + ".png", ex.Message);
                result = false;
            }

            return result;
        }

        internal static Dictionary<string, string> getResourceDict(string type)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (type == "sword")
            {
                dict.Add("sword1.png", @"fighting\sword01.bmp");
                dict.Add("sword2.png", @"fighting\sword02.bmp");
                dict.Add("sword3.png", @"fighting\sword03.bmp");
                dict.Add("sword4.png", @"fighting\sword04.bmp");
                dict.Add("sword5.png", @"fighting\sword05.bmp");
                dict.Add("sword6.png", @"fighting\sword06.bmp");
                dict.Add("sword7.png", @"fighting\sword07.bmp");
                dict.Add("sword8.png", @"fighting\sword08.bmp");
                dict.Add("sword9.png", @"fighting\sword09.bmp");
                dict.Add("sword10.png", @"fighting\sword10.bmp");
                dict.Add("sword11.png", @"fighting\sword11.bmp");
                dict.Add("sword12.png", @"fighting\sword12.bmp");
                dict.Add("sword13.png", @"fighting\sword13.bmp");
                dict.Add("sword14.png", @"fighting\sword14.bmp");
                dict.Add("sword15.png", @"fighting\sword15.bmp");
                dict.Add("sword16.png", @"fighting\sword16.bmp");
                dict.Add("sword17.png", @"fighting\sword17.bmp");
                dict.Add("sword18.png", @"fighting\sword18.bmp");
                dict.Add("sword19.png", @"fighting\sword19.bmp");
                dict.Add("sword20.png", @"fighting\sword20.bmp");
                dict.Add("sword21.png", @"fighting\sword21.bmp");
                dict.Add("sword22.png", @"fighting\sword22.bmp");
                dict.Add("sword23.png", @"fighting\sword23.bmp");
                dict.Add("sword24.png", @"fighting\sword24.bmp");
                dict.Add("sword25.png", @"fighting\sword25.bmp");
                dict.Add("sword26.png", @"fighting\sword26.bmp");
                dict.Add("sword27.png", @"fighting\sword27.bmp");
                dict.Add("sword28.png", @"fighting\sword28.bmp");
                dict.Add("sword29.png", @"fighting\sword29.bmp");
                dict.Add("sword30.png", @"fighting\sword30.bmp");
                dict.Add("sword31.png", @"fighting\sword31.bmp");
                dict.Add("sword32.png", @"fighting\sword32.bmp");
                dict.Add("sword33.png", @"fighting\sword33.bmp");
                dict.Add("sword34.png", @"fighting\sword34.bmp");
            }
            else if (type == "fire")
            {
                dict.Add("fire_1.png", "frame1.bmp");
                dict.Add("fire_2.png", "frame2.bmp");
                dict.Add("fire_3.png", "frame3.bmp");
                dict.Add("fire_4.png", "frame4.bmp");
                dict.Add("fire_5.png", "frame5.bmp");
                dict.Add("fire_6.png", "frame6.bmp");
                dict.Add("fire_7.png", "frame7.bmp");
                dict.Add("fire_8.png", "frame8.bmp");
                dict.Add("fire_9.png", "frame9.bmp");
            }
            else if (type == "bubble")
            {
                dict.Add("bubble_1", "frame01.bmp");
                dict.Add("bubble_2", "frame02.bmp");
                dict.Add("bubble_3", "frame03.bmp");
                dict.Add("bubble_4", "frame04.bmp");
                dict.Add("bubble_5", "frame05.bmp");
                dict.Add("bubble_6", "frame06.bmp");
                dict.Add("bubble_7", "frame07.bmp");
            }
            else if (type == "blood")
            {
                dict.Add("slicer_blood_1.png", "frame02.bmp");
                dict.Add("slicer_blood_2.png", "frame03.bmp");
                dict.Add("slicer_blood_3.png", "frame04.bmp");
                dict.Add("slicer_blood_4.png", "frame05.bmp");
                dict.Add("slicer_blood_5.png", "frame01.bmp");
            }
            return dict;
        }
    }
}
