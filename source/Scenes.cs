using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popsc
{
    internal class Scenes
    {
        internal static bool convertScenes(string inputPath, string outputPath)
        {
            bool result = true;
            // Local variables
            string bmpPath = "", bmpName = "", pngName;
            string pngPath = Path.Combine(outputPath, "scenes");
            if (!Directory.Exists(pngPath))
            {
                Directory.CreateDirectory(pngPath);
            }
            Object[] types = new Object[]
            {
                new Object[2] {@"pv\objects", "clock"},
                new Object[2] {@"pv\objects", "star"},
                new Object[2] {@"pv\objects", "room"}
            };
            try
            {
                for (int g = 0; g < types.Count(); g++)
                {
                    Object[] type = (Object[]) types[g];
                    bmpPath = Path.Combine(inputPath, type[0].ToString());
                    if (type[1].ToString() == "clock")
                    {
                        for (int c = 1; c < 8; c++)
                        {
                            string clName = "clock0" + c.ToString(), csName = "clocksand0" + c.ToString();
                            result = Util.convertBitmap(Path.Combine(bmpPath, clName + ".bmp"), Path.Combine(pngPath, clName + ".png"));
                            if (c < 4) Util.convertBitmap(Path.Combine(bmpPath, csName + ".bmp"), Path.Combine(pngPath, csName + ".png"));
                            Console.WriteLine("Clock frame converted: {0}", Path.Combine(pngPath, clName + ".png"));
                        }
                    }
                    else if (type[1].ToString() == "star")
                    {
                        result = buildStar(Color.FromArgb(85, 85, 85), Path.Combine(pngPath, "star0.png"));
                        result = buildStar(Color.FromArgb(170, 170, 170), Path.Combine(pngPath, "star1.png"));
                        result = buildStar(Color.FromArgb(252, 252, 252), Path.Combine(pngPath, "star2.png"));
                        Console.WriteLine("Star frames built: {0}", Path.Combine(pngPath, "star*.png"));
                    }
                    else if (type[1].ToString() == "room")
                    {
                        result = Util.convertBitmap(Path.Combine(bmpPath, "room pillar.bmp"), Path.Combine(pngPath, "room-pillar.png"));
                        Object[] files = new Object[]
                        {
                            new Object[4] {bmpPath, "room.bmp", new int[2] {0, 0}, true},
                            new Object[4] {bmpPath, "room bed.bmp", new int[2] {0, 142}, true}
                        };
                        Tiles.buildTile(files, pngPath, "princess-room.png", 320, 200);
                        Console.WriteLine("Scene frame built: {0}", Path.Combine(pngPath, "princess-room.png"));
                    }
                    if (!result) break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting scenes: {0} {1}", Path.Combine(bmpPath, bmpName), ex.Message);
                result = false;
            }

            return result;
        }

        private static bool buildStar(Color color, string output)
        {
            Bitmap bitmap;
            Boolean result = true;
            try
            {
                bitmap = new Bitmap(1, 1);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(color);
                }
                bitmap.Save(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error building star frame: {0} {1}", output, ex.Message);
                result = false;
            }
            return result;
        }
    }
}
