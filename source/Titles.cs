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
using System.IO;
using System.Linq;

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
