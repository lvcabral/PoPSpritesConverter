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
using System.IO;
using System.Linq;

namespace popsc
{
    internal class Guards
    {
        internal static bool convertGuards(string inputPath, string outputPath)
        {
            bool result = true;
            // Local variables
            string bmpName = "", pngName, pngPath, sheetPath, mapPath;
            string bmpPath = Path.Combine(inputPath, "guards");
            string genPath = Path.Combine(outputPath, "general");
            if (!Directory.Exists(genPath))
            {
                Directory.CreateDirectory(genPath);
            }
            Dictionary<string, string> dict = getResourceDict("guard");
            try
            {
                for (int c = 0; c < 7; c++)
                {
                    Util.images = new List<string>();
                    pngPath = Path.Combine(outputPath, @"guards\guard" + (c + 1).ToString());
                    if (!Directory.Exists(pngPath))
                    {
                        Directory.CreateDirectory(pngPath);
                    }
                    Color[] colors = Util.readPalette(Path.Combine(inputPath, @"prince\binary\guard palettes.pal"), c);
                    foreach (var file in dict)
                    {
                        bmpName = file.Value;
                        pngName = file.Key;
                        result = Util.convertBitmapPalette(Path.Combine(bmpPath, bmpName), Path.Combine(pngPath, pngName), colors);
                        if (!result) break;
                        Console.WriteLine("Guard frame converted: {0}", Path.Combine(pngPath, pngName));
                    }
                    if (!result) break;
                    sheetPath = Path.Combine(outputPath, "guard" + (c + 1).ToString() + ".png");
                    mapPath = c == 0 ? Path.Combine(outputPath, "guard.json") : "";
                    result = Util.packSprites(sheetPath, mapPath);
                    if (!result) break;
                    string pngSplash = Path.Combine(genPath, "guard" + (c + 1).ToString() + "-splash.png");
                    result = Util.convertBitmapPalette(Path.Combine(bmpPath, "splash.bmp"),pngSplash, colors);
                    if (!result) break;
                    Console.WriteLine("Guard splash converted: {0}", Path.Combine(genPath, pngSplash));
                    string pngLife = Path.Combine(genPath, "guard" + (c + 1).ToString() + "-live.png");
                    result = Util.convertBitmapPalette(Path.Combine(bmpPath, "hit points.bmp"), pngLife, colors);
                    if (!result) break;
                    Console.WriteLine("Guard life converted: {0}", Path.Combine(genPath, pngSplash));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting guard: {0} {1}", Path.Combine(bmpPath, bmpName), ex.Message);
                result = false;
            }

            return result;
        }

        internal static bool convertSpecialGuards(string inputPath, string outputPath, bool xorShadow = true)
        {
            bool result = true;
            // Local variables
            string bmpPath = "", bmpName = "", pngName, pngPath, sheetPath;
            string genPath = Path.Combine(outputPath, "general");
            if (!Directory.Exists(genPath))
            {
                Directory.CreateDirectory(genPath);
            }
            Dictionary<string, string> dict;
            Object[] types = new Object[]
            {
                new Object[2] {"fat","fatguard"},
                new Object[2] {"shadow","shadow"},
                new Object[2] {"skel","skeleton"},
                new Object[2] {"vizier","vizier"}
            };
            try
            {
                for (int g = 0; g < types.Count(); g++)
                {
                    Util.images = new List<string>();
                    Object[] type = (Object[])types[g];
                    bmpPath = Path.Combine(inputPath, type[0].ToString());
                    pngPath = Path.Combine(outputPath, @"guards\" + type[1].ToString());
                    if (!Directory.Exists(pngPath))
                    {
                        Directory.CreateDirectory(pngPath);
                    }
                    dict = getResourceDict(type[1].ToString());
                    foreach (var file in dict)
                    {
                        bmpName = file.Value;
                        pngName = file.Key;
                        if (type[1].ToString() != "shadow")
                        {
                            result = Util.convertBitmap(Path.Combine(bmpPath, bmpName), Path.Combine(pngPath, pngName));
                        }
                        else if (xorShadow)
                        {
                            result = Util.convertShadow(Path.Combine(inputPath, bmpName), Path.Combine(pngPath, pngName));
                        }
                        else
                        {
                            result = Util.convertBitmap(Path.Combine(inputPath, bmpName), Path.Combine(pngPath, pngName));
                        }
                        if (!result) break;
                        Console.WriteLine("Guard frame converted: {0}", Path.Combine(pngPath, pngName));
                    }
                    if (!result) break;
                    sheetPath = Path.Combine(outputPath, type[1].ToString());
                    result = Util.packSprites(sheetPath + ".png", sheetPath + ".json");
                    if (!result) break;
                    if (type[1].ToString() != "skeleton")
                    {
                        string pngSplash = Path.Combine(genPath, type[1].ToString() + "-splash.png");
                        result = Util.convertBitmap(Path.Combine(bmpPath, "splash.bmp"), pngSplash);
                        if (!result) break;
                        Console.WriteLine("Guard splash converted: {0}", Path.Combine(genPath, pngSplash));
                        string pngLife = Path.Combine(genPath, type[1].ToString() + "-live.png");
                        result = Util.convertBitmap(Path.Combine(bmpPath, "hit points.bmp"), pngLife);
                        if (!result) break;
                        Console.WriteLine("Guard life converted: {0}", Path.Combine(genPath, pngSplash));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting guard: {0} {1}", Path.Combine(bmpPath, bmpName), ex.Message);
                result = false;
            }

            return result;
        }

        internal static Dictionary<string, string> getResourceDict(string type)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (type != "shadow")
            {
                dict.Add(type + "-0.png", "res763.bmp");
                dict.Add(type + "-1.png", "res753.bmp");
                dict.Add(type + "-2.png", "res754.bmp");
                dict.Add(type + "-3.png", "res755.bmp");
                dict.Add(type + "-4.png", "res756.bmp");
                dict.Add(type + "-5.png", "res757.bmp");
                dict.Add(type + "-6.png", "res758.bmp");
                dict.Add(type + "-7.png", "res759.bmp");
                dict.Add(type + "-8.png", "res760.bmp");
                dict.Add(type + "-9.png", "res761.bmp");
                dict.Add(type + "-10.png", "res762.bmp");
                dict.Add(type + "-12.png", "res764.bmp");
                dict.Add(type + "-13.png", "res765.bmp");
                dict.Add(type + "-14.png", "res766.bmp");
                dict.Add(type + "-15.png", "res767.bmp");
                dict.Add(type + "-16.png", "res768.bmp");
                dict.Add(type + "-17.png", "res769.bmp");
                dict.Add(type + "-18.png", "res770.bmp");
                dict.Add(type + "-19.png", "res771.bmp");
                dict.Add(type + "-20.png", "res772.bmp");
                dict.Add(type + "-21.png", "res772.bmp");
                dict.Add(type + "-22.png", "res773.bmp");
                dict.Add(type + "-23.png", "res774.bmp");
                dict.Add(type + "-24.png", "res775.bmp");
                dict.Add(type + "-27.png", "spiked.bmp");
                dict.Add(type + "-28.png", "chopped.bmp");
                if (type != "skeleton")
                {
                    dict.Add(type + "-29.png", "res779.bmp");
                    dict.Add(type + "-30.png", "res780.bmp");
                    dict.Add(type + "-31.png", "res781.bmp");
                    dict.Add(type + "-32.png", "res782.bmp");
                    dict.Add(type + "-33.png", "res783.bmp");
                    dict.Add(type + "-35.png", "res784.bmp");
                }
            }
            else
            {
                dict.Add("shadow-1.png", @"kid\running\frame01.bmp");
                dict.Add("shadow-2.png", @"kid\running\frame02.bmp");
                dict.Add("shadow-3.png", @"kid\running\frame03.bmp");
                dict.Add("shadow-4.png", @"kid\running\frame04.bmp");
                dict.Add("shadow-5.png", @"kid\running\frame05.bmp");
                dict.Add("shadow-6.png", @"kid\running\frame06.bmp");
                dict.Add("shadow-7.png", @"kid\running\frame07.bmp");
                dict.Add("shadow-8.png", @"kid\running\frame08.bmp");
                dict.Add("shadow-9.png", @"kid\running\frame09.bmp");
                dict.Add("shadow-10.png", @"kid\running\frame10.bmp");
                dict.Add("shadow-11.png", @"kid\running\frame11.bmp");
                dict.Add("shadow-12.png", @"kid\running\frame12.bmp");
                dict.Add("shadow-13.png", @"kid\running\frame13.bmp");
                dict.Add("shadow-14.png", @"kid\running\frame14.bmp");
                dict.Add("shadow-15.png", @"kid\normal.bmp");
                dict.Add("shadow-16.png", @"kid\simple jump\frame01.bmp");
                dict.Add("shadow-17.png", @"kid\simple jump\frame02.bmp");
                dict.Add("shadow-18.png", @"kid\simple jump\frame03.bmp");
                dict.Add("shadow-19.png", @"kid\simple jump\frame04.bmp");
                dict.Add("shadow-20.png", @"kid\simple jump\frame05.bmp");
                dict.Add("shadow-21.png", @"kid\simple jump\frame06.bmp");
                dict.Add("shadow-22.png", @"kid\simple jump\frame07.bmp");
                dict.Add("shadow-23.png", @"kid\simple jump\frame08.bmp");
                dict.Add("shadow-24.png", @"kid\simple jump\frame09.bmp");
                dict.Add("shadow-25.png", @"kid\simple jump\frame10.bmp");
                dict.Add("shadow-26.png", @"kid\simple jump\frame11.bmp");
                dict.Add("shadow-27.png", @"kid\simple jump\frame12.bmp");
                dict.Add("shadow-28.png", @"kid\simple jump\frame13.bmp");
                dict.Add("shadow-29.png", @"kid\simple jump\frame14.bmp");
                dict.Add("shadow-30.png", @"kid\simple jump\frame15.bmp");
                dict.Add("shadow-31.png", @"kid\simple jump\frame16.bmp");
                dict.Add("shadow-32.png", @"kid\simple jump\frame17.bmp");
                dict.Add("shadow-33.png", @"kid\simple jump\frame18.bmp");
                dict.Add("shadow-34.png", @"kid\running jump\frame01.bmp");
                dict.Add("shadow-35.png", @"kid\running jump\frame02.bmp");
                dict.Add("shadow-36.png", @"kid\running jump\frame03.bmp");
                dict.Add("shadow-37.png", @"kid\running jump\frame04.bmp");
                dict.Add("shadow-38.png", @"kid\running jump\frame05.bmp");
                dict.Add("shadow-39.png", @"kid\running jump\frame06.bmp");
                dict.Add("shadow-40.png", @"kid\running jump\frame07.bmp");
                dict.Add("shadow-41.png", @"kid\running jump\frame08.bmp");
                dict.Add("shadow-42.png", @"kid\running jump\frame09.bmp");
                dict.Add("shadow-43.png", @"kid\running jump\frame10.bmp");
                dict.Add("shadow-44.png", @"kid\running jump\frame11.bmp");
                dict.Add("shadow-45.png", @"kid\turning\frame01.bmp");
                dict.Add("shadow-46.png", @"kid\turning\frame02.bmp");
                dict.Add("shadow-47.png", @"kid\turning\frame03.bmp");
                dict.Add("shadow-48.png", @"kid\turning\frame04.bmp");
                dict.Add("shadow-49.png", @"kid\turning\frame05.bmp");
                dict.Add("shadow-50.png", @"kid\turning\frame06.bmp");
                dict.Add("shadow-51.png", @"kid\turning\frame07.bmp");
                dict.Add("shadow-52.png", @"kid\turning\frame08.bmp");
                dict.Add("shadow-53.png", @"kid\turn running\frame01.bmp");
                dict.Add("shadow-54.png", @"kid\turn running\frame02.bmp");
                dict.Add("shadow-55.png", @"kid\turn running\frame03.bmp");
                dict.Add("shadow-56.png", @"kid\turn running\frame04.bmp");
                dict.Add("shadow-57.png", @"kid\turn running\frame05.bmp");
                dict.Add("shadow-58.png", @"kid\turn running\frame06.bmp");
                dict.Add("shadow-59.png", @"kid\turn running\frame07.bmp");
                dict.Add("shadow-60.png", @"kid\turn running\frame08.bmp");
                dict.Add("shadow-61.png", @"kid\turn running\frame09.bmp");
                dict.Add("shadow-62.png", @"kid\turn running\frame10.bmp");
                dict.Add("shadow-63.png", @"kid\turn running\frame11.bmp");
                dict.Add("shadow-64.png", @"kid\turn running\frame12.bmp");
                dict.Add("shadow-65.png", @"kid\turn running\frame13.bmp");
                dict.Add("shadow-102.png", @"kid\hanging and falling\frame13.bmp");
                dict.Add("shadow-103.png", @"kid\hanging and falling\frame14.bmp");
                dict.Add("shadow-104.png", @"kid\hanging and falling\frame15.bmp");
                dict.Add("shadow-105.png", @"kid\hanging and falling\frame16.bmp");
                dict.Add("shadow-106.png", @"kid\hanging and falling\frame17.bmp");
                dict.Add("shadow-107.png", @"kid\crouching\frame01.bmp");
                dict.Add("shadow-108.png", @"kid\crouching\frame02.bmp");
                dict.Add("shadow-109.png", @"kid\crouching\frame03.bmp");
                dict.Add("shadow-110.png", @"kid\crouching\frame04.bmp");
                dict.Add("shadow-111.png", @"kid\crouching\frame05.bmp");
                dict.Add("shadow-112.png", @"kid\crouching\frame06.bmp");
                dict.Add("shadow-113.png", @"kid\crouching\frame07.bmp");
                dict.Add("shadow-114.png", @"kid\crouching\frame08.bmp");
                dict.Add("shadow-115.png", @"kid\crouching\frame09.bmp");
                dict.Add("shadow-116.png", @"kid\crouching\frame10.bmp");
                dict.Add("shadow-117.png", @"kid\crouching\frame11.bmp");
                dict.Add("shadow-118.png", @"kid\crouching\frame12.bmp");
                dict.Add("shadow-119.png", @"kid\crouching\frame13.bmp");
                dict.Add("shadow-121.png", @"kid\walking a step\frame01.bmp");
                dict.Add("shadow-122.png", @"kid\walking a step\frame02.bmp");
                dict.Add("shadow-123.png", @"kid\walking a step\frame03.bmp");
                dict.Add("shadow-124.png", @"kid\walking a step\frame04.bmp");
                dict.Add("shadow-125.png", @"kid\walking a step\frame05.bmp");
                dict.Add("shadow-126.png", @"kid\walking a step\frame06.bmp");
                dict.Add("shadow-127.png", @"kid\walking a step\frame07.bmp");
                dict.Add("shadow-128.png", @"kid\walking a step\frame08.bmp");
                dict.Add("shadow-129.png", @"kid\walking a step\frame09.bmp");
                dict.Add("shadow-130.png", @"kid\walking a step\frame10.bmp");
                dict.Add("shadow-131.png", @"kid\walking a step\frame11.bmp");
                dict.Add("shadow-132.png", @"kid\walking a step\frame12.bmp");
                dict.Add("shadow-133.png", @"kid\putting down sword\frame05.bmp");
                dict.Add("shadow-134.png", @"kid\putting down sword\frame06.bmp");
                dict.Add("shadow-150.png", @"shadow\res763.bmp");
                dict.Add("shadow-151.png", @"shadow\res753.bmp");
                dict.Add("shadow-152.png", @"shadow\res754.bmp");
                dict.Add("shadow-153.png", @"shadow\res755.bmp");
                dict.Add("shadow-154.png", @"shadow\res756.bmp");
                dict.Add("shadow-155.png", @"shadow\res757.bmp");
                dict.Add("shadow-156.png", @"shadow\res758.bmp");
                dict.Add("shadow-157.png", @"shadow\res759.bmp");
                dict.Add("shadow-158.png", @"shadow\res760.bmp");
                dict.Add("shadow-159.png", @"shadow\res761.bmp");
                dict.Add("shadow-160.png", @"shadow\res762.bmp");
                dict.Add("shadow-162.png", @"shadow\res764.bmp");
                dict.Add("shadow-163.png", @"shadow\res765.bmp");
                dict.Add("shadow-164.png", @"shadow\res766.bmp");
                dict.Add("shadow-165.png", @"shadow\res767.bmp");
                dict.Add("shadow-167.png", @"shadow\res769.bmp");
                dict.Add("shadow-168.png", @"shadow\res770.bmp");
                dict.Add("shadow-169.png", @"shadow\res771.bmp");
                dict.Add("shadow-170.png", @"shadow\res772.bmp");
                dict.Add("shadow-171.png", @"shadow\res772.bmp");
                dict.Add("shadow-172.png", @"shadow\res773.bmp");
                dict.Add("shadow-173.png", @"shadow\res774.bmp");
                dict.Add("shadow-174.png", @"shadow\res775.bmp");
                dict.Add("shadow-179.png", @"kid\dieing\frame01.bmp");
                dict.Add("shadow-180.png", @"kid\dieing\frame02.bmp");
                dict.Add("shadow-181.png", @"kid\dieing\frame03.bmp");
                dict.Add("shadow-182.png", @"kid\dieing\frame04.bmp");
                dict.Add("shadow-183.png", @"kid\dieing\frame05.bmp");
                dict.Add("shadow-185.png", @"kid\deaths\dead.bmp");
                dict.Add("shadow-191.png", @"kid\drinking\frame01.bmp");
                dict.Add("shadow-192.png", @"kid\drinking\frame02.bmp");
                dict.Add("shadow-193.png", @"kid\drinking\frame03.bmp");
                dict.Add("shadow-194.png", @"kid\drinking\frame04.bmp");
                dict.Add("shadow-195.png", @"kid\drinking\frame05.bmp");
                dict.Add("shadow-196.png", @"kid\drinking\frame06.bmp");
                dict.Add("shadow-197.png", @"kid\drinking\frame07.bmp");
                dict.Add("shadow-198.png", @"kid\drinking\frame08.bmp");
                dict.Add("shadow-199.png", @"kid\drinking\frame09.bmp");
                dict.Add("shadow-200.png", @"kid\drinking\frame10.bmp");
                dict.Add("shadow-201.png", @"kid\drinking\frame11.bmp");
                dict.Add("shadow-202.png", @"kid\drinking\frame12.bmp");
                dict.Add("shadow-203.png", @"kid\drinking\frame13.bmp");
                dict.Add("shadow-204.png", @"kid\drinking\frame14.bmp");
                dict.Add("shadow-205.png", @"kid\drinking\frame15.bmp");
                dict.Add("shadow-207.png", @"kid\taking sword out\frame01.bmp");
                dict.Add("shadow-208.png", @"kid\taking sword out\frame02.bmp");
                dict.Add("shadow-209.png", @"kid\taking sword out\frame03.bmp");
                dict.Add("shadow-210.png", @"kid\taking sword out\frame04.bmp");
                dict.Add("shadow-233.png", @"kid\got sword\frame05.bmp");
                dict.Add("shadow-234.png", @"kid\got sword\frame06.bmp");
                dict.Add("shadow-235.png", @"kid\got sword\frame07.bmp");
                dict.Add("shadow-236.png", @"kid\got sword\frame08.bmp");
                dict.Add("shadow-237.png", @"kid\putting down sword\frame01.bmp");
                dict.Add("shadow-238.png", @"kid\putting down sword\frame02.bmp");
                dict.Add("shadow-239.png", @"kid\putting down sword\frame03.bmp");
                dict.Add("shadow-240.png", @"kid\putting down sword\frame04.bmp");
            }
            return dict;
        }
    }
}
