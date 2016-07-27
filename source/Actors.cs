﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popsc
{
    internal class Actors
    {
        internal static bool convertActors(string inputPath, string outputPath)
        {
            bool result = true;
            // Local variables
            string bmpPath = "", bmpName = "", pngName, pngPath;
            Dictionary<string, string> dict;
            Object[] types = new Object[]
            {
                new Object[2] {@"pv\princess", "princess"},
                new Object[2] {@"pv\jaffar", "vizier"},
                new Object[2] {@"kid\mouse", "mouse"}
            };
            try
            {
                for (int g = 0; g < types.Count(); g++)
                {
                    Object[] type = (Object[])types[g];
                    bmpPath = Path.Combine(inputPath, type[0].ToString());
                    dict = getResourceDict(type[1].ToString());
                    pngPath = Path.Combine(outputPath, @"actors\" + type[1].ToString());
                    if (!Directory.Exists(pngPath))
                    {
                        Directory.CreateDirectory(pngPath);
                    }
                    foreach (var file in dict)
                    {
                        bmpName = file.Value;
                        pngName = file.Key;
                        result = Util.convertBitmap(Path.Combine(bmpPath, bmpName), Path.Combine(pngPath, pngName));
                        if (!result) break;
                        Console.WriteLine("Actor frame converted: {0}", Path.Combine(pngPath, pngName));
                    }
                    if (!result) break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting actor: {0} {1}", Path.Combine(bmpPath, bmpName), ex.Message);
                result = false;
            }

            return result;
        }

        internal static Dictionary<string, string> getResourceDict(string type)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (type == "princess")
            {
                dict.Add("princess-1.png", @"in story\frame16.bmp");
                dict.Add("princess-2.png", @"in story\frame02.bmp");
                dict.Add("princess-3.png", @"in story\frame03.bmp");
                dict.Add("princess-4.png", @"in story\frame04.bmp");
                dict.Add("princess-5.png", @"in story\frame05.bmp");
                dict.Add("princess-6.png", @"in story\frame06.bmp");
                dict.Add("princess-7.png", @"in story\frame07.bmp");
                dict.Add("princess-8.png", @"in story\frame08.bmp");
                dict.Add("princess-9.png", @"in story\frame09.bmp");
                dict.Add("princess-11.png", @"in story\frame01.bmp");
                dict.Add("princess-12.png", @"in story\frame10.bmp");
                dict.Add("princess-13.png", @"in story\frame11.bmp");
                dict.Add("princess-14.png", @"in story\frame12.bmp");
                dict.Add("princess-15.png", @"in story\frame13.bmp");
                dict.Add("princess-16.png", @"in story\frame14.bmp");
                dict.Add("princess-17.png", @"in story\frame15.bmp");
                dict.Add("princess-18.png", @"in story\frame17.bmp");
                dict.Add("princess-19.png", @"resting.bmp");
                dict.Add("princess-20.png", @"winning\frame01.bmp");
                dict.Add("princess-21.png", @"winning\frame02.bmp");
                dict.Add("princess-22.png", @"winning\frame03.bmp");
                dict.Add("princess-23.png", @"winning\frame04.bmp");
                dict.Add("princess-24.png", @"winning\frame05.bmp");
                dict.Add("princess-25.png", @"winning\frame06.bmp");
                dict.Add("princess-26.png", @"winning\frame07.bmp");
                dict.Add("princess-27.png", @"winning\frame08.bmp");
                dict.Add("princess-28.png", @"winning\frame09.bmp");
                dict.Add("princess-29.png", @"winning\frame10.bmp");
                dict.Add("princess-30.png", @"winning\frame11.bmp");
                dict.Add("princess-31.png", @"winning\frame12.bmp");
                dict.Add("princess-32.png", @"winning\frame13.bmp");
                dict.Add("princess-33.png", @"winning\frame14.bmp");
                dict.Add("princess-34.png", @"with mouse\frame01.bmp");
                dict.Add("princess-35.png", @"with mouse\frame02.bmp");
                dict.Add("princess-36.png", @"with mouse\frame03.bmp");
                dict.Add("princess-37.png", @"with mouse\frame04.bmp");
                dict.Add("princess-38.png", @"with mouse\frame05.bmp");
                dict.Add("princess-39.png", @"with mouse\frame06.bmp");
                dict.Add("princess-40.png", @"with mouse\frame07.bmp");
                dict.Add("princess-41.png", @"with mouse\frame08.bmp");
                dict.Add("princess-42.png", @"with mouse\frame09.bmp");
                dict.Add("princess-43.png", @"with mouse\frame10.bmp");
                dict.Add("princess-44.png", @"with mouse\frame11.bmp");
                dict.Add("princess-45.png", @"with mouse\frame12.bmp");
                dict.Add("princess-46.png", @"with mouse\frame13.bmp");
                dict.Add("princess-47.png", @"with mouse\frame14.bmp");
            }
            else if (type == "vizier")
            {
                dict.Add("vizier-1.png", @"walking\frame01.bmp");
                dict.Add("vizier-2.png", @"walking\frame02.bmp");
                dict.Add("vizier-3.png", @"walking\frame03.bmp");
                dict.Add("vizier-4.png", @"walking\frame04.bmp");
                dict.Add("vizier-5.png", @"walking\frame05.bmp");
                dict.Add("vizier-6.png", @"walking\frame06.bmp");
                dict.Add("vizier-7.png", @"walking\frame07.bmp");
                dict.Add("vizier-8.png", @"walking\frame08.bmp");
                dict.Add("vizier-9.png", @"walking\frame09.bmp");
                dict.Add("vizier-10.png", @"walking\frame10.bmp");
                dict.Add("vizier-11.png", @"walking\frame11.bmp");
                dict.Add("vizier-12.png", @"walking\frame12.bmp");
                dict.Add("vizier-13.png", @"walking\frame13.bmp");
                dict.Add("vizier-14.png", @"walking\frame14.bmp");
                dict.Add("vizier-15.png", @"walking\frame15.bmp");
                dict.Add("vizier-16.png", @"walking\frame16.bmp");
                dict.Add("vizier-17.png", @"walking\frame17.bmp");
                dict.Add("vizier-18.png", @"walking\frame18.bmp");
                dict.Add("vizier-19.png", @"walking\frame19.bmp");
                dict.Add("vizier-20.png", @"conjuring\frame20.bmp");
                dict.Add("vizier-21.png", @"conjuring\frame21.bmp");
                dict.Add("vizier-22.png", @"conjuring\frame22.bmp");
                dict.Add("vizier-23.png", @"conjuring\frame23.bmp");
                dict.Add("vizier-24.png", @"conjuring\frame24.bmp");
                dict.Add("vizier-25.png", @"conjuring\frame25.bmp");
                dict.Add("vizier-26.png", @"conjuring\frame26.bmp");
                dict.Add("vizier-27.png", @"conjuring\frame27.bmp");
                dict.Add("vizier-28.png", @"conjuring\frame28.bmp");
                dict.Add("vizier-29.png", @"conjuring\frame29.bmp");
                dict.Add("vizier-30.png", @"conjuring\frame30.bmp");
                dict.Add("vizier-31.png", @"conjuring\frame31.bmp");
                dict.Add("vizier-32.png", @"conjuring\frame32.bmp");
                dict.Add("vizier-33.png", @"conjuring\frame33.bmp");
                dict.Add("vizier-34.png", @"conjuring\frame34.bmp");
                dict.Add("vizier-35.png", @"conjuring\frame35.bmp");
                dict.Add("vizier-36.png", @"conjuring\frame36.bmp");
                dict.Add("vizier-37.png", @"conjuring\frame37.bmp");
                dict.Add("vizier-38.png", @"conjuring\frame38.bmp");
            }
            else if (type == "mouse")
            {
                dict.Add("mouse-1.png", "frame01.bmp");
                dict.Add("mouse-2.png", "frame02.bmp");
                dict.Add("mouse-3.png", "frame03.bmp");
            }
            return dict;
        }
    }
}
