using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popsc
{
    internal class Titles
    {
        internal static bool convertTitles(string inputPath, string outputPath)
        {
            bool result = true;
            // Local variables
            string bmpName = "", pngName = "";
            string bmpPath = Path.Combine(inputPath, "title");
            string pngPath = Path.Combine(outputPath, "titles");
            if (!Directory.Exists(pngPath))
            {
                Directory.CreateDirectory(pngPath);
            }
            Object[] files = new Object[]
            {
                new Object[3] {@"main titles\main background.bmp", "intro-screen.png", false},
                new Object[3] {@"main titles\game name.bmp", "message-game-name.png", true},
                new Object[3] {@"main titles\presents.bmp", "message-presents.png", true},
                new Object[3] {@"main titles\author.bmp", "message-author.png", true},
                new Object[3] {@"main titles\copyright.bmp", "message-port.png", true},
                new Object[3] {@"main titles\text background.bmp", "text-screen.png", true},
                new Object[3] {@"texts\in the absence.bmp", "text-in-the-absence.png", true},
                new Object[3] {@"texts\marry jaffar.bmp", "text-marry-jaffar.png", true},
                new Object[3] {@"texts\the tyrant.bmp", "text-the-tyrant.png", true}
            };
            try
            {
                for (int g = 0; g < files.Count(); g++)
                {
                    Object[] file = (Object[])files[g];
                    bmpName = file[0].ToString();
                    pngName = file[1].ToString();
                    result = Util.convertBitmap(Path.Combine(bmpPath, bmpName), Path.Combine(pngPath, pngName), (bool)file[2]);
                    if (!result) break;
                    Console.WriteLine("Title sprite converted: {0}", Path.Combine(pngPath, pngName));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting title: {0} {1}", Path.Combine(bmpPath, bmpName), ex.Message);
                result = false;
            }

            return result;
        }
    }
}
