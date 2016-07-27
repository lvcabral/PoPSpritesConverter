using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popsc
{
    internal class Kid
    {
        internal static bool convertKid(string inputPath, string outputPath)
        {
            bool result = true;
            // Local variables
            string bmpPath = "", bmpName = "", pngName, pngPath, genPath;
            Dictionary<string, string> dict = getResourceDict();
            Dictionary<string, Rectangle> crop = getCropDict();
            try
            {
                bmpPath = Path.Combine(inputPath, "kid");
                pngPath = Path.Combine(outputPath, "kid");
                genPath = Path.Combine(outputPath, "general");
                if (!Directory.Exists(pngPath)) Directory.CreateDirectory(pngPath);
                if (!Directory.Exists(genPath)) Directory.CreateDirectory(genPath);
                foreach (var file in dict)
                {
                    bmpName = file.Value;
                    pngName = file.Key;
                    result = Util.convertBitmap(Path.Combine(bmpPath, bmpName), Path.Combine(pngPath, pngName));
                    if (!result) break;
                    if (bmpName.Substring(0, 8) == "clipping" && Int16.Parse(pngName.Substring(4, 3)) < 145)
                    {
                        Rectangle[] rects = new Rectangle[1] { crop[pngName] };
                        result = Util.clipBitmap(Path.Combine(pngPath, pngName), Path.Combine(pngPath, pngName.Replace(".png", "r.png")), rects);
                        if (!result) break;
                    }
                    Console.WriteLine("Kid frame converted: {0}", Path.Combine(pngPath, pngName));
                }
                ;
                result = Util.clipBitmap(Path.Combine(pngPath, "kid-228.png"), Path.Combine(pngPath, "kid-0.png"), new Rectangle[1] { new Rectangle(0, 0, 11, 24) });
                string pngSplash = Path.Combine(genPath, "kid-splash.png");
                result = Util.convertBitmap(Path.Combine(bmpPath, @"objects\splash.bmp"), pngSplash);
                Console.WriteLine("Kid splash converted: {0}", Path.Combine(genPath, pngSplash));
                string pngLife = Path.Combine(genPath, "kid-live.png");
                result = Util.convertBitmap(Path.Combine(bmpPath, @"objects\full live.bmp"), pngLife);
                Console.WriteLine("Kid full life converted: {0}", Path.Combine(genPath, pngSplash));
                pngLife = Path.Combine(genPath, "kid-emptylive.png");
                result = Util.convertBitmap(Path.Combine(bmpPath, @"objects\empty live.bmp"), pngLife);
                Console.WriteLine("Kid empty life converted: {0}", Path.Combine(genPath, pngSplash));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting kid: {0} {1}", Path.Combine(bmpPath, bmpName), ex.Message);
                result = false;
            }

            return result;
        }

        internal static Dictionary<string, Rectangle> getCropDict()
        {
            Dictionary<string, Rectangle> crop = new Dictionary<string, Rectangle>();
            crop.Add("kid-135.png", new Rectangle(0, 0, 9, 12));
            crop.Add("kid-136.png", new Rectangle(0, 0, 12, 12));
            crop.Add("kid-137.png", new Rectangle(0, 9, 13, 8));
            crop.Add("kid-138.png", new Rectangle(0, 15, 18, 9));
            crop.Add("kid-139.png", new Rectangle(0, 13, 24, 9));
            crop.Add("kid-140.png", new Rectangle(0, 12, 25, 9));
            crop.Add("kid-141.png", new Rectangle(0, 12, 23, 9));
            crop.Add("kid-142.png", new Rectangle(0, 14, 31, 8));
            crop.Add("kid-143.png", new Rectangle(0, 14, 28, 5));
            crop.Add("kid-144.png", new Rectangle(0, 21, 30, 3)); 
            return crop;
        }

        internal static Dictionary<string, string> getResourceDict()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("kid-1.png", @"running\frame01.bmp");
            dict.Add("kid-2.png", @"running\frame02.bmp");
            dict.Add("kid-3.png", @"running\frame03.bmp");
            dict.Add("kid-4.png", @"running\frame04.bmp");
            dict.Add("kid-5.png", @"running\frame05.bmp");
            dict.Add("kid-6.png", @"running\frame06.bmp");
            dict.Add("kid-7.png", @"running\frame07.bmp");
            dict.Add("kid-8.png", @"running\frame08.bmp");
            dict.Add("kid-9.png", @"running\frame09.bmp");
            dict.Add("kid-10.png", @"running\frame10.bmp");
            dict.Add("kid-11.png", @"running\frame11.bmp");
            dict.Add("kid-12.png", @"running\frame12.bmp");
            dict.Add("kid-13.png", @"running\frame13.bmp");
            dict.Add("kid-14.png", @"running\frame14.bmp");
            dict.Add("kid-15.png", @"normal.bmp");
            dict.Add("kid-16.png", @"simple jump\frame01.bmp");
            dict.Add("kid-17.png", @"simple jump\frame02.bmp");
            dict.Add("kid-18.png", @"simple jump\frame03.bmp");
            dict.Add("kid-19.png", @"simple jump\frame04.bmp");
            dict.Add("kid-20.png", @"simple jump\frame05.bmp");
            dict.Add("kid-21.png", @"simple jump\frame06.bmp");
            dict.Add("kid-22.png", @"simple jump\frame07.bmp");
            dict.Add("kid-23.png", @"simple jump\frame08.bmp");
            dict.Add("kid-24.png", @"simple jump\frame09.bmp");
            dict.Add("kid-25.png", @"simple jump\frame10.bmp");
            dict.Add("kid-26.png", @"simple jump\frame11.bmp");
            dict.Add("kid-27.png", @"simple jump\frame12.bmp");
            dict.Add("kid-28.png", @"simple jump\frame13.bmp");
            dict.Add("kid-29.png", @"simple jump\frame14.bmp");
            dict.Add("kid-30.png", @"simple jump\frame15.bmp");
            dict.Add("kid-31.png", @"simple jump\frame16.bmp");
            dict.Add("kid-32.png", @"simple jump\frame17.bmp");
            dict.Add("kid-33.png", @"simple jump\frame18.bmp");
            dict.Add("kid-34.png", @"running jump\frame01.bmp");
            dict.Add("kid-35.png", @"running jump\frame02.bmp");
            dict.Add("kid-36.png", @"running jump\frame03.bmp");
            dict.Add("kid-37.png", @"running jump\frame04.bmp");
            dict.Add("kid-38.png", @"running jump\frame05.bmp");
            dict.Add("kid-39.png", @"running jump\frame06.bmp");
            dict.Add("kid-40.png", @"running jump\frame07.bmp");
            dict.Add("kid-41.png", @"running jump\frame08.bmp");
            dict.Add("kid-42.png", @"running jump\frame09.bmp");
            dict.Add("kid-43.png", @"running jump\frame10.bmp");
            dict.Add("kid-44.png", @"running jump\frame11.bmp");
            dict.Add("kid-45.png", @"turning\frame01.bmp");
            dict.Add("kid-46.png", @"turning\frame02.bmp");
            dict.Add("kid-47.png", @"turning\frame03.bmp");
            dict.Add("kid-48.png", @"turning\frame04.bmp");
            dict.Add("kid-49.png", @"turning\frame05.bmp");
            dict.Add("kid-50.png", @"turning\frame06.bmp");
            dict.Add("kid-51.png", @"turning\frame07.bmp");
            dict.Add("kid-52.png", @"turning\frame08.bmp");
            dict.Add("kid-53.png", @"turn running\frame01.bmp");
            dict.Add("kid-54.png", @"turn running\frame02.bmp");
            dict.Add("kid-55.png", @"turn running\frame03.bmp");
            dict.Add("kid-56.png", @"turn running\frame04.bmp");
            dict.Add("kid-57.png", @"turn running\frame05.bmp");
            dict.Add("kid-58.png", @"turn running\frame06.bmp");
            dict.Add("kid-59.png", @"turn running\frame07.bmp");
            dict.Add("kid-60.png", @"turn running\frame08.bmp");
            dict.Add("kid-61.png", @"turn running\frame09.bmp");
            dict.Add("kid-62.png", @"turn running\frame10.bmp");
            dict.Add("kid-63.png", @"turn running\frame11.bmp");
            dict.Add("kid-64.png", @"turn running\frame12.bmp");
            dict.Add("kid-65.png", @"turn running\frame13.bmp");
            dict.Add("kid-67.png", @"scaling\frame01.bmp");
            dict.Add("kid-68.png", @"scaling\frame02.bmp");
            dict.Add("kid-69.png", @"scaling\frame03.bmp");
            dict.Add("kid-70.png", @"scaling\frame04.bmp");
            dict.Add("kid-71.png", @"scaling\frame05.bmp");
            dict.Add("kid-72.png", @"scaling\frame06.bmp");
            dict.Add("kid-73.png", @"scaling\frame07.bmp");
            dict.Add("kid-74.png", @"scaling\frame08.bmp");
            dict.Add("kid-75.png", @"scaling\frame09.bmp");
            dict.Add("kid-76.png", @"scaling\frame10.bmp");
            dict.Add("kid-77.png", @"scaling\frame11.bmp");
            dict.Add("kid-78.png", @"scaling\frame12.bmp");
            dict.Add("kid-79.png", @"scaling\frame13.bmp");
            dict.Add("kid-80.png", @"scaling\frame14.bmp");
            dict.Add("kid-81.png", @"scaling\frame15.bmp");
            dict.Add("kid-82.png", @"scaling\frame16.bmp");
            dict.Add("kid-83.png", @"scaling\frame17.bmp");
            dict.Add("kid-84.png", @"scaling\frame18.bmp");
            dict.Add("kid-85.png", @"scaling\frame19.bmp");
            dict.Add("kid-86.png", @"simple jump\frame14.bmp");
            dict.Add("kid-87.png", @"hanging and falling\frame00.bmp");
            dict.Add("kid-88.png", @"hanging and falling\frame01.bmp");
            dict.Add("kid-89.png", @"hanging and falling\frame02.bmp");
            dict.Add("kid-90.png", @"hanging and falling\frame03.bmp");
            dict.Add("kid-91.png", @"hanging and falling\frame04.bmp");
            dict.Add("kid-92.png", @"hanging and falling\frame05.bmp");
            dict.Add("kid-93.png", @"hanging and falling\frame06.bmp");
            dict.Add("kid-94.png", @"hanging and falling\frame07.bmp");
            dict.Add("kid-95.png", @"hanging and falling\frame08.bmp");
            dict.Add("kid-96.png", @"hanging and falling\frame09.bmp");
            dict.Add("kid-97.png", @"hanging and falling\frame10.bmp");
            dict.Add("kid-98.png", @"hanging and falling\frame11.bmp");
            dict.Add("kid-99.png", @"hanging and falling\frame12.bmp");
            dict.Add("kid-102.png", @"hanging and falling\frame13.bmp");
            dict.Add("kid-103.png", @"hanging and falling\frame14.bmp");
            dict.Add("kid-104.png", @"hanging and falling\frame15.bmp");
            dict.Add("kid-105.png", @"hanging and falling\frame16.bmp");
            dict.Add("kid-106.png", @"hanging and falling\frame17.bmp");
            dict.Add("kid-107.png", @"crouching\frame01.bmp");
            dict.Add("kid-108.png", @"crouching\frame02.bmp");
            dict.Add("kid-109.png", @"crouching\frame03.bmp");
            dict.Add("kid-110.png", @"crouching\frame04.bmp");
            dict.Add("kid-111.png", @"crouching\frame05.bmp");
            dict.Add("kid-112.png", @"crouching\frame06.bmp");
            dict.Add("kid-113.png", @"crouching\frame07.bmp");
            dict.Add("kid-114.png", @"crouching\frame08.bmp");
            dict.Add("kid-115.png", @"crouching\frame09.bmp");
            dict.Add("kid-116.png", @"crouching\frame10.bmp");
            dict.Add("kid-117.png", @"crouching\frame11.bmp");
            dict.Add("kid-118.png", @"crouching\frame12.bmp");
            dict.Add("kid-119.png", @"crouching\frame13.bmp");
            dict.Add("kid-121.png", @"walking a step\frame01.bmp");
            dict.Add("kid-122.png", @"walking a step\frame02.bmp");
            dict.Add("kid-123.png", @"walking a step\frame03.bmp");
            dict.Add("kid-124.png", @"walking a step\frame04.bmp");
            dict.Add("kid-125.png", @"walking a step\frame05.bmp");
            dict.Add("kid-126.png", @"walking a step\frame06.bmp");
            dict.Add("kid-127.png", @"walking a step\frame07.bmp");
            dict.Add("kid-128.png", @"walking a step\frame08.bmp");
            dict.Add("kid-129.png", @"walking a step\frame09.bmp");
            dict.Add("kid-130.png", @"walking a step\frame10.bmp");
            dict.Add("kid-131.png", @"walking a step\frame11.bmp");
            dict.Add("kid-132.png", @"walking a step\frame12.bmp");
            dict.Add("kid-133.png", @"putting down sword\frame05.bmp");
            dict.Add("kid-134.png", @"putting down sword\frame06.bmp");
            dict.Add("kid-135.png", @"clipping\frame01.bmp");
            dict.Add("kid-136.png", @"clipping\frame02.bmp");
            dict.Add("kid-137.png", @"clipping\frame03.bmp");
            dict.Add("kid-138.png", @"clipping\frame04.bmp");
            dict.Add("kid-139.png", @"clipping\frame05.bmp");
            dict.Add("kid-140.png", @"clipping\frame06.bmp");
            dict.Add("kid-141.png", @"clipping\frame07.bmp");
            dict.Add("kid-142.png", @"clipping\frame08.bmp");
            dict.Add("kid-143.png", @"clipping\frame09.bmp");
            dict.Add("kid-144.png", @"clipping\frame10.bmp");
            dict.Add("kid-145.png", @"clipping\frame11.bmp");
            dict.Add("kid-146.png", @"clipping\frame12.bmp");
            dict.Add("kid-147.png", @"clipping\frame13.bmp");
            dict.Add("kid-148.png", @"clipping\frame14.bmp");
            dict.Add("kid-149.png", @"clipping\frame15.bmp");
            dict.Add("kid-150.png", @"sword attacking\frame11.bmp");
            dict.Add("kid-151.png", @"sword attacking\frame01.bmp");
            dict.Add("kid-152.png", @"sword attacking\frame02.bmp");
            dict.Add("kid-153.png", @"sword attacking\frame03.bmp");
            dict.Add("kid-154.png", @"sword attacking\frame04.bmp");
            dict.Add("kid-155.png", @"sword attacking\frame05.bmp");
            dict.Add("kid-156.png", @"sword attacking\frame06.bmp");
            dict.Add("kid-157.png", @"sword attacking\frame07.bmp");
            dict.Add("kid-158.png", @"sword attacking\frame08.bmp");
            dict.Add("kid-159.png", @"sword attacking\frame09.bmp");
            dict.Add("kid-160.png", @"sword attacking\frame10.bmp");
            dict.Add("kid-162.png", @"sword attacking\frame12.bmp");
            dict.Add("kid-164.png", @"sword attacking\frame14.bmp");
            dict.Add("kid-165.png", @"sword attacking\frame15.bmp");
            dict.Add("kid-167.png", @"sword attacking\frame16.bmp");
            dict.Add("kid-169.png", @"sword attacking\frame18.bmp");
            dict.Add("kid-172.png", @"hanging and falling\frame13.bmp");
            dict.Add("kid-173.png", @"hanging and falling\frame14.bmp");
            dict.Add("kid-174.png", @"hanging and falling\frame15.bmp");
            dict.Add("kid-177.png", @"deaths\spiked.bmp");
            dict.Add("kid-178.png", @"deaths\chopped.bmp");
            dict.Add("kid-179.png", @"dieing\frame01.bmp");
            dict.Add("kid-180.png", @"dieing\frame02.bmp");
            dict.Add("kid-181.png", @"dieing\frame03.bmp");
            dict.Add("kid-182.png", @"dieing\frame04.bmp");
            dict.Add("kid-183.png", @"dieing\frame05.bmp");
            dict.Add("kid-185.png", @"deaths\dead.bmp");
            dict.Add("kid-191.png", @"drinking\frame01.bmp");
            dict.Add("kid-192.png", @"drinking\frame02.bmp");
            dict.Add("kid-193.png", @"drinking\frame03.bmp");
            dict.Add("kid-194.png", @"drinking\frame04.bmp");
            dict.Add("kid-195.png", @"drinking\frame05.bmp");
            dict.Add("kid-196.png", @"drinking\frame06.bmp");
            dict.Add("kid-197.png", @"drinking\frame07.bmp");
            dict.Add("kid-198.png", @"drinking\frame08.bmp");
            dict.Add("kid-199.png", @"drinking\frame09.bmp");
            dict.Add("kid-200.png", @"drinking\frame10.bmp");
            dict.Add("kid-201.png", @"drinking\frame11.bmp");
            dict.Add("kid-202.png", @"drinking\frame12.bmp");
            dict.Add("kid-203.png", @"drinking\frame13.bmp");
            dict.Add("kid-204.png", @"drinking\frame14.bmp");
            dict.Add("kid-205.png", @"drinking\frame15.bmp");
            dict.Add("kid-207.png", @"taking sword out\frame01.bmp");
            dict.Add("kid-208.png", @"taking sword out\frame02.bmp");
            dict.Add("kid-209.png", @"taking sword out\frame03.bmp");
            dict.Add("kid-210.png", @"taking sword out\frame04.bmp");
            dict.Add("kid-217.png", @"stairs\frame01.bmp");
            dict.Add("kid-218.png", @"stairs\frame02.bmp");
            dict.Add("kid-219.png", @"stairs\frame03.bmp");
            dict.Add("kid-220.png", @"stairs\frame04.bmp");
            dict.Add("kid-221.png", @"stairs\frame05.bmp");
            dict.Add("kid-222.png", @"stairs\frame06.bmp");
            dict.Add("kid-223.png", @"stairs\frame07.bmp");
            dict.Add("kid-224.png", @"stairs\frame08.bmp");
            dict.Add("kid-225.png", @"stairs\frame09.bmp");
            dict.Add("kid-226.png", @"stairs\frame10.bmp");
            dict.Add("kid-227.png", @"stairs\frame11.bmp");
            dict.Add("kid-228.png", @"stairs\frame12.bmp");
            dict.Add("kid-229.png", @"got sword\frame01.bmp");
            dict.Add("kid-230.png", @"got sword\frame02.bmp");
            dict.Add("kid-231.png", @"got sword\frame03.bmp");
            dict.Add("kid-232.png", @"got sword\frame04.bmp");
            dict.Add("kid-233.png", @"got sword\frame05.bmp");
            dict.Add("kid-234.png", @"got sword\frame06.bmp");
            dict.Add("kid-235.png", @"got sword\frame07.bmp");
            dict.Add("kid-236.png", @"got sword\frame08.bmp");
            dict.Add("kid-237.png", @"putting down sword\frame01.bmp");
            dict.Add("kid-238.png", @"putting down sword\frame02.bmp");
            dict.Add("kid-239.png", @"putting down sword\frame03.bmp");
            dict.Add("kid-240.png", @"putting down sword\frame04.bmp");
            return dict;
        }

    }
}
