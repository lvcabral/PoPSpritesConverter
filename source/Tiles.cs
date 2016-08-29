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
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

namespace popsc
{
    internal class Tiles
    {
        internal static string currentType;
        internal static Color[] tileColors;

        internal static bool convertTiles(string type, string inputPath, string outputPath, bool wda = false, int color = -1)
        {
            // Load alternative colors
            string palettePath = Path.Combine(inputPath, @"prince\binary\level color variations.pal");
            string colorId = "";
            if (color >= 0 && File.Exists(palettePath))
            {
                tileColors = Util.readPalette(palettePath, color);
                colorId = color.ToString();
            }
            else
            {
                tileColors = new Color[0];
            }

            // Building tiles
            Object[] files, file;
            List<Object[]> parts;
            currentType = type;
            Util.images = new List<string>();
            string tilesPath = Path.Combine(inputPath, "v" + type);
            string objectsPath = Path.Combine(inputPath, "prince");
            string sheetPath = Path.Combine(outputPath, type + colorId);
            string spritesPath = Path.Combine(outputPath, @"tiles\" + type + colorId);
            if (!Directory.Exists(spritesPath))
            {
                Directory.CreateDirectory(spritesPath);
            }

            // TILE_SPACE
            if (!buildTile(new Object[] { }, spritesPath, type + "_0.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_0_0.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_0_fg.png")) return false;
            file = new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_0_1.png")) return false;
            file = new Object[5] { tilesPath, @"background\bricks04.bmp", new int[2] { 32, -23 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_0_2.png")) return false;
            file = new Object[5] { tilesPath, @"background\window.bmp", new int[2] { 32, -3 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_0_3.png")) return false;

            // TILE_FLOOR
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_1.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_1_fg.png")) return false;
            if (type == "dungeon")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_1_0.png")) return false;
                file = new Object[5] { tilesPath, @"background\bricks01.bmp", new int[2] { 32, -23 }, true, true};
                if (!buildTile(new Object[] { file }, spritesPath, type + "_1_1.png")) return false;
            }
            else
            {
                file = new Object[5] { tilesPath, @"background\bricks01.bmp", new int[2] { 32, -23 }, true, true};
                if (!buildTile(new Object[] { file }, spritesPath, type + "_1_0.png")) return false;
                if (!buildTile(new Object[] { }, spritesPath, type + "_1_1.png")) return false;
            }
            file = new Object[5] { tilesPath, @"background\bricks02.bmp", new int[2] { 32, -23 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_1_2.png")) return false;

            // TILE_SPIKES
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\spikes left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\spikes right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_2.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_2_fg.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_2_0.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_2_0_fg.png")) return false;
            files = new Object[]
            {
                new Object[5] {tilesPath, @"spikes\frame01 left.bmp", new int[2] {0, 64}, true, true},
                new Object[5] {tilesPath, @"spikes\frame01 right.bmp", new int[2] {32, 66}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_1.png")) return false;
            file = new Object[5] { tilesPath, @"spikes\frame01 front.bmp", new int[2] { 0, 69 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_1_fg.png")) return false;
            files = new Object[]
            {
                new Object[5] {tilesPath, @"spikes\frame02 left.bmp", new int[2] {0, 54}, true, true},
                new Object[5] {tilesPath, @"spikes\frame02 right.bmp", new int[2] {32, 54}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_2.png")) return false;
            file = new Object[5] { tilesPath, @"spikes\frame02 front.bmp", new int[2] { 0, 61 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_2_fg.png")) return false;
            files = new Object[]
            {
                new Object[5] {tilesPath, @"spikes\frame03 left.bmp", new int[2] {0, 47}, true, true},
                new Object[5] {tilesPath, @"spikes\frame03 right.bmp", new int[2] {32, 54}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_3.png")) return false;
            file = new Object[5] { tilesPath, @"spikes\frame03 front.bmp", new int[2] { 0, 50 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_3_fg.png")) return false;
            files = new Object[]
            {
                new Object[5] {tilesPath, @"spikes\frame04 left.bmp", new int[2] {0, 48}, true, true},
                new Object[5] {tilesPath, @"spikes\frame04 right.bmp", new int[2] {32, 49}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_4.png")) return false;
            file = new Object[5] { tilesPath, @"spikes\frame04 front.bmp", new int[2] { 0, 51 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_4_fg.png")) return false;
            files = new Object[]
            {
                new Object[5] {tilesPath, @"spikes\frame05 left.bmp", new int[2] {0, 48}, true, true},
                new Object[5] {tilesPath, @"spikes\frame05 right.bmp", new int[2] {32, 50}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_5.png")) return false;
            file = new Object[5] { tilesPath, @"spikes\frame05 front.bmp", new int[2] { 0, 53 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_5_fg.png")) return false;

            // TILE_PILLAR
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"pillar\pillar left.bmp", new int[2] {0, 16}, true, true},
                new Object[5] {tilesPath, @"pillar\pillar right main.bmp", new int[2] {32, 16}, true, true},
                new Object[5] {tilesPath, @"pillar\pillar right top.bmp", new int[2] {32, 9}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_3.png")) return false;
            if (type == "dungeon")
                files[1] = new Object[5] { tilesPath, @"pillar\pillar front.bmp", new int[2] { 8, 16 }, false, true};
            else
                files[1] = new Object[5] { tilesPath, @"pillar\pillar front.bmp", new int[2] { 8, 16 }, true, true};
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_3_fg.png")) return false;

            // TILE_GATE
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true, true},
                new Object[5] {tilesPath, @"door\door frame right.bmp", new int[2] {32, 16}, true, true},
                new Object[5] {tilesPath, @"door\door frame right top.bmp", new int[2] {32, 3}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_4.png")) return false;
            files[1] = new Object[5] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true, true};
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_4_fg.png")) return false;
            files = new Object[]
            {
                new Object[5] {tilesPath, @"door\res00260.bmp", new int[2] {32, 7}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {32, 15}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {32, 23}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {32, 31}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {32, 39}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {32, 47}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {32, 55}, true, true},
                new Object[5] {tilesPath, @"door\res00251.bmp", new int[2] {32, 63}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_gate.png")) return false;
            files = new Object[]
            {
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {0, -1}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {0, 7}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {0, 15}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {0, 23}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {0, 31}, true, true},
                new Object[5] {tilesPath, @"door\res00252.bmp", new int[2] {0, 39}, true, true},
                new Object[5] {tilesPath, @"door\res00251.bmp", new int[2] {0, 47}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_gate_fg.png", 8, 57)) return false;

            // TILE_DROP_BUTTON
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\closer base unpressed.bmp", new int[2] {0, 76}, false, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_6.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_6_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\closer base pressed.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 64}, true, true},
                    new Object[5] {tilesPath, @"floor panels\closer right pressed.bmp", new int[2] {32, 63}, true, true}
                };
            }
            else
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\closer base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 64}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 64}, true, true},
                    new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true}
                };
            }
            if (!buildTile(files, spritesPath, type + "_6_down.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_6_down_fg.png")) return false;

            // TILE_TAPESTRY
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true, true},
                    new Object[5] {tilesPath, @"arch\up part with carpet wall.bmp", new int[2] {32, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_7.png")) return false;
                files[1] = new Object[5] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true, true};
                if (!buildTile(files, spritesPath, type + "_7_fg.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true, true},
                    new Object[5] {tilesPath, @"carpets\style01.bmp", new int[2] {32, 16}, true, true},
                    new Object[5] {tilesPath, @"carpets\style01 top.bmp", new int[2] {32, 4}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_7_1.png")) return false;
                Rectangle[] rects = new Rectangle[4] { new Rectangle(0, 0, 25, 76), new Rectangle(0, 0, 64, 16), new Rectangle(40, 0, 24, 79), new Rectangle(32, 71, 8, 8) };
                Util.clipBitmap(Path.Combine(spritesPath, type + "_7_1.png"), Path.Combine(spritesPath, type + "_7_1_fg.png"), rects);
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true, true},
                    new Object[5] {tilesPath, @"carpets\style02.bmp", new int[2] {32, 16}, true, true},
                    new Object[5] {tilesPath, @"carpets\style02 top.bmp", new int[2] {32, 4}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_7_2.png")) return false;
                Util.clipBitmap(Path.Combine(spritesPath, type + "_7_2.png"), Path.Combine(spritesPath, type + "_7_2_fg.png"), rects);
            }

            // TILE_BOTTOM_BIG_PILLAR
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"big pillar\big pillar lower left.bmp", new int[2] {0, 16}, true, true},
                new Object[5] {tilesPath, @"big pillar\big pillar lower right.bmp", new int[2] {32, 16}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_8.png")) return false;
            files[1] = new Object[5] { tilesPath, @"big pillar\big pillar lower front.bmp", new int[2] { 8, 16 }, true, true};
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_8_fg.png")) return false;

            // TILE_TOP_BIG_PILLAR
            files = new Object[]
            {
                new Object[5] {tilesPath, @"big pillar\big pillar upper left.bmp", new int[2] {8, 16}, true, true},
                new Object[5] {tilesPath, @"big pillar\big pillar upper right.bmp", new int[2] {32, 16}, true, true},
                new Object[5] {tilesPath, @"big pillar\big pillar upper right top.bmp", new int[2] {32, 10}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_9.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_9_fg.png")) return false;

            // TILE_POTION
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_10.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_10_fg.png")) return false;
            files[1] = new Object[5] { objectsPath, @"potions\base\small " + type + ".bmp", new int[2] { 22, -6 }, true, false};
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_10_fg_1.png")) return false;
            files[1] = new Object[5] { objectsPath, @"potions\base\big " + type + ".bmp", new int[2] { 22, -6 }, true, false };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_10_fg_2.png")) return false;

            // TILE_LOOSE_BOARD
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_11.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_11_fg.png")) return false;
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\loose base01.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\loose left01.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\loose right01.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_loose_1.png")) return false;
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\loose base02.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\loose left02.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\loose right02.bmp", new int[2] {32, 63}, true, true}
            };
            if (!buildTile(parts.ToArray(), spritesPath, type + "_falling.png")) return false;
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_loose_3.png")) return false;
            if (!buildTile(files, spritesPath, type + "_loose_4.png")) return false;
            if (!buildTile(files, spritesPath, type + "_loose_8.png")) return false;
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_loose_2.png")) return false;
            if (!buildTile(files, spritesPath, type + "_loose_5.png")) return false;
            if (!buildTile(files, spritesPath, type + "_loose_6.png")) return false;
            if (!buildTile(files, spritesPath, type + "_loose_7.png")) return false;

            // TILE_TAPESTRY_TOP
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true, true},
                    new Object[5] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true, true},
                    new Object[5] {tilesPath, @"arch\up part with carpet wall.bmp", new int[2] {32, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_12.png")) return false;
                if (!buildTile(files, spritesPath, type + "_12_fg.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true, true},
                    new Object[5] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true, true},
                    new Object[5] {tilesPath, @"carpets\style01.bmp", new int[2] {32, 16}, true, true},
                    new Object[5] {tilesPath, @"carpets\style01 top.bmp", new int[2] {32, 4}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_12_1.png")) return false;
                Rectangle[] rects = new Rectangle[3] { new Rectangle(0, 0, 64, 16), new Rectangle(40, 0, 24, 79), new Rectangle(32, 71, 8, 8) };
                Util.clipBitmap(Path.Combine(spritesPath, type + "_12_1.png"), Path.Combine(spritesPath, type + "_12_1_fg.png"), rects);
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true, true},
                    new Object[5] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true, true},
                    new Object[5] {tilesPath, @"carpets\style02.bmp", new int[2] {32, 16}, true, true},
                    new Object[5] {tilesPath, @"carpets\style02 top.bmp", new int[2] {32, 4}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_12_2.png")) return false;
                Util.clipBitmap(Path.Combine(spritesPath, type + "_12_2.png"), Path.Combine(spritesPath, type + "_12_2_fg.png"), rects);
            }
            else
            {
                files = new Object[]
                {
                    new Object[5] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true, true},
                    new Object[5] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_12_fg.png")) return false;
            }

            // TILE_MIRROR
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"mirror\mirror and floor.bmp", new int[2] {0, 16}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_13.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] { tilesPath, @"mirror\mirror.bmp", new int[2] { 0, 16 }, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_13_fg.png")) return false;
            }

            // TILE_DEBRIS
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\broken left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\broken right.bmp", new int[2] {32, 62}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_14.png")) return false;
            files[1] = new Object[5] { tilesPath, @"floor panels\broken front.bmp", new int[2] { 0, 67 }, true, true};
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_14_fg.png")) return false;

            // TILE_RAISE_BUTTON
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\opener base unpressed.bmp", new int[2] {0, 76}, false, true},
                new Object[5] {tilesPath, @"floor panels\opener left unpressed.bmp", new int[2] {0, 62}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 62}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_15.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_15_fg.png")) return false;
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_15_down.png")) return false;

            // TILE_EXIT_LEFT
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_16.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_16_fg.png")) return false;

            // TILE_EXIT_RIGHT
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, false, true},
                new Object[5] {tilesPath, @"level door\frame upper left.bmp", new int[2] {0, 0}, false, true},
                new Object[5] {tilesPath, @"level door\frame lower left.bmp", new int[2] {0, 16}, true, true},
                new Object[5] {tilesPath, @"level door\frame upper right.bmp", new int[2] {32, 0}, false, true},
                new Object[5] {tilesPath, @"level door\frame lower right.bmp", new int[2] {32, 16}, true, true},
                new Object[5] {tilesPath, @"level door\door top.bmp", new int[2] {8, -67}, true, true},
                new Object[5] {tilesPath, @"level door\stairs.bmp", new int[2] {8, 14}, false, true},
                new Object[5] {tilesPath, @"level door\floor.bmp", new int[2] {8, 59}, false, true}
            };
            if (!buildDoor(files, spritesPath, type + "_17.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_17_fg.png")) return false;
            files = new Object[]
                {
                    new Object[5] {tilesPath, @"level door\frame upper left.bmp", new int[2] {0, 0}, true, true},
                    new Object[5] {tilesPath, @"level door\frame upper right.bmp", new int[2] {32, 0}, true, true},
                    new Object[5] {tilesPath, @"level door\frame lower right.bmp", new int[2] {32, 16}, true, true},
                    new Object[5] {tilesPath, @"level door\door top.bmp", new int[2] {8, -67}, true, true}
                };
            if (!buildDoor(files, spritesPath, type + "_door_fg.png", -1, 16)) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, -3}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 1}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 5}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 9}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 13}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 17}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 21}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 25}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 29}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 33}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 37}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 41}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 45}, false, true}
                };
                if (!buildTile(files, spritesPath, type + "_door.png", 42, 51, false)) return false;
            }
            else
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 0}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 1}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 5}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 9}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 13}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 17}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 21}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 25}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 29}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 33}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 37}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 41}, false, true},
                    new Object[5] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 45}, false, true}
                };
                if (!buildTile(files, spritesPath, type + "_door.png", 49, 51)) return false;
            }
            // TILE_SLICER
            parts = new List<Object[]>
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
            };
            if (type == "palace") parts.Add(new Object[5] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_18.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_18_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"chomper\frame02 top.bmp", new int[2] {0, -40}, true, true},
                    new Object[5] {tilesPath, @"chomper\frame02 bottom.bmp", new int[2] {0, -3}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_slicer_1.png")) return false;
            }
            else
            {
                file = new Object[5] { tilesPath, @"chomper\frame02.bmp", new int[2] { 0, 16 }, true, true};
                if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_1.png")) return false;
            }
            file = new Object[5] { tilesPath, @"chomper\frame02 front.bmp", new int[2] { 0, 16 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_1_fg.png")) return false;
            file = new Object[5] { tilesPath, @"chomper\frame03.bmp", new int[2] { 0, 16 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_2.png")) return false;
            file = new Object[5] { tilesPath, @"chomper\frame03 front.bmp", new int[2] { 0, 16 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_2_fg.png")) return false;
            file = new Object[5] { tilesPath, @"chomper\frame04.bmp", new int[2] { 0, 16 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_3.png")) return false;
            file = new Object[5] { tilesPath, @"chomper\frame04 front.bmp", new int[2] { 0, 16 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_3_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"chomper\frame05 top.bmp", new int[2] {0, -53}, true, true},
                    new Object[5] {tilesPath, @"chomper\frame05 bottom.bmp", new int[2] {0, -3}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_slicer_4.png")) return false;
            }
            else
            {
                file = new Object[5] { tilesPath, @"chomper\frame05.bmp", new int[2] { 0, 16 }, true, true};
                if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_4.png")) return false;
            }
            file = new Object[5] { tilesPath, @"chomper\frame05 front.bmp", new int[2] { 0, 16 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_4_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"chomper\frame01 top.bmp", new int[2] {0, -50}, true, true},
                    new Object[5] {tilesPath, @"chomper\frame01 bottom.bmp", new int[2] {0, -3}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_slicer_5.png")) return false;
            }
            else
            {
                file = new Object[5] { tilesPath, @"chomper\frame01.bmp", new int[2] { 0, 16 }, true, true};
                if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_5.png")) return false;
            }
            file = new Object[5] { tilesPath, @"chomper\frame01 front.bmp", new int[2] { 0, 16 }, true, true};
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_5_fg.png")) return false;

            // TILE_TORCH
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true},
                new Object[5] {tilesPath, @"background\torch.bmp", new int[2] {32, -28}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_19.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_19_fg.png")) return false;

            // TILE_WALL
            if (!buildTile(new Object[] { }, spritesPath, type + "_20.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_20_fg.png")) return false;

            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"walls\face stack top.bmp", new int[2] {32, 4}, true, true},
                    new Object[5] {tilesPath, @"walls\face stack main.bmp", new int[2] {32, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_wall_0.png")) return false;
            }
            else
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true},
                    new Object[5] {tilesPath, @"walls\face stack top.bmp", new int[2] {32, 3}, true, true},
                    new Object[5] {tilesPath, @"walls\face stack main.bmp", new int[2] {32, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_wall_0.png")) return false;
                files = new Object[2] {files[1], files[2]};
            }
            if (!buildTile(files, spritesPath, type + "_wall_1.png")) return false;

            if (type == "dungeon" || wda)
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"walls\single stack main.bmp", new int[2] {0, 16}, false, true},
                    new Object[5] {tilesPath, @"walls\single stack base.bmp", new int[2] {0, 76}, false, true}
                };
                if (!buildTile(files, spritesPath, "SWS.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"walls\left stack main.bmp", new int[2] {0, 16}, false, true},
                    new Object[5] {tilesPath, @"walls\left stack base.bmp", new int[2] {0, 76}, false, true}
                };
                if (!buildTile(files, spritesPath, "SWW.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"walls\right stack main.bmp", new int[2] {0, 16}, false, true},
                    new Object[5] {tilesPath, @"walls\right stack base.bmp", new int[2] {0, 76}, false, true},
                };
                if (!buildTile(files, spritesPath, "WWS.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"walls\centre stack main.bmp", new int[2] {0, 16}, false, true},
                    new Object[5] {tilesPath, @"walls\centre stack base.bmp", new int[2] {0, 76}, false, true}
                };
                if (!buildTile(files, spritesPath, "WWW.png")) return false;

                if (tileColors.Length > 0)
                {
                    Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\random block.bmp"), Path.Combine(spritesPath, type + "_wall_random.png"), tileColors);
                    Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\divider01.bmp"), Path.Combine(spritesPath, type + "_wall_divider_1.png"), tileColors);
                    Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\divider02.bmp"), Path.Combine(spritesPath, type + "_wall_divider_2.png"), tileColors);
                    Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\mark01.bmp"), Path.Combine(spritesPath, type + "_wall_mark_1.png"), tileColors);
                    Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\mark02.bmp"), Path.Combine(spritesPath, type + "_wall_mark_2.png"), tileColors);
                    Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\mark03.bmp"), Path.Combine(spritesPath, type + "_wall_mark_3.png"), tileColors);
                    Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\mark04.bmp"), Path.Combine(spritesPath, type + "_wall_mark_4.png"), tileColors);
                }
                else
                {
                    Util.convertBitmap(Path.Combine(tilesPath, @"walls\random block.bmp"), Path.Combine(spritesPath, type + "_wall_random.png"));
                    Util.convertBitmap(Path.Combine(tilesPath, @"walls\divider01.bmp"), Path.Combine(spritesPath, type + "_wall_divider_1.png"));
                    Util.convertBitmap(Path.Combine(tilesPath, @"walls\divider02.bmp"), Path.Combine(spritesPath, type + "_wall_divider_2.png"));
                    Util.convertBitmap(Path.Combine(tilesPath, @"walls\mark01.bmp"), Path.Combine(spritesPath, type + "_wall_mark_1.png"));
                    Util.convertBitmap(Path.Combine(tilesPath, @"walls\mark02.bmp"), Path.Combine(spritesPath, type + "_wall_mark_2.png"));
                    Util.convertBitmap(Path.Combine(tilesPath, @"walls\mark03.bmp"), Path.Combine(spritesPath, type + "_wall_mark_3.png"));
                    Util.convertBitmap(Path.Combine(tilesPath, @"walls\mark04.bmp"), Path.Combine(spritesPath, type + "_wall_mark_4.png"));
                }
            }
            else 
            {
                Color[] palette = Util.readPalette(Path.Combine(inputPath, @"vpalace\palettes\wall.pal"));
                List<Color> clr = new List<Color>();
                clr.Add(Color.Black);
                for (int c = 0; c < 15; c++)
                {
                    clr.Add(palette[14]);
                }
                Color[] colors = clr.ToArray();
                int res = 363;
                for (int c = 0; c < 15; c++)
                {
                    if (!Util.convertBitmapPalette(Path.Combine(tilesPath, @"walls\res" + res.ToString() + ".bmp"), Path.Combine(spritesPath, "W_" + c.ToString() + ".png"), colors)) return false;
                    res++;
                }
            }

            // TILE_SKELETON
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"floor panels\skeleton left.bmp", new int[2] {0, 57}, true, true},
                    new Object[5] {tilesPath, @"floor panels\skeleton right.bmp", new int[2] {32, -1}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_21.png")) return false;
                if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_21_fg.png")) return false;
            }

            // TILE_SWORD
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true},
                new Object[5] {objectsPath, @"sword\in the floor\normal.bmp", new int[2] {0, 63}, true, false}
            };
            if (!buildTile(files, spritesPath, type + "_22.png")) return false;
            files[3] = new Object[5] { objectsPath, @"sword\in the floor\bright.bmp", new int[2] { 0, 63 }, true, false};
            if (!buildTile(files, spritesPath, type + "_22_bright.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_22_fg.png")) return false;

            // TILE_BALCONY_LEFT
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_23.png")) return false;
                if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_23_fg.png")) return false;
            }

            // TILE_BALCONY_RIGHT
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_24.png")) return false;
                if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_24_fg.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"balcony\upper left.bmp", new int[2] {0, -59}, true, true},
                    new Object[5] {tilesPath, @"balcony\upper right.bmp", new int[2] {32, -59}, true, true},
                    new Object[5] {tilesPath, @"balcony\lower left.bmp", new int[2] {0, 20}, true, true},
                    new Object[5] {tilesPath, @"balcony\lower right.bmp", new int[2] {32, 20}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_balcony.png")) return false;
            }

            // TILE_LATTICE_PILLAR
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"pillar\pillar left.bmp", new int[2] {0, 16}, true, true},
                    new Object[5] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true, true},
                    new Object[5] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_25.png")) return false;
                files[1] = new Object[5] { tilesPath, @"pillar\pillar front.bmp", new int[2] { 8, 16 }, true, true};
                if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_25_fg.png")) return false;
            }

            // TILE_LATTICE_SUPPORT
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_26.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"arch\down part base.bmp", new int[2] {0, 76}, true, true},
                    new Object[5] {tilesPath, @"arch\down part.bmp", new int[2] {0, 23}, true, true},
                    new Object[5] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_26_fg.png")) return false;
            }

            // TILE_SMALL_LATTICE
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_27.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"arch\up part small.bmp", new int[2] {0, 23}, true, true},
                    new Object[5] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_27_fg.png")) return false;
            }

            // TILE_LATTICE_LEFT
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_28.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"arch\up part big left.bmp", new int[2] {0, 23}, true, true},
                    new Object[5] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_28_fg.png")) return false;
            }

            // TILE_LATTICE_RIGHT
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_29.png")) return false;
                files = new Object[]
                {
                    new Object[5] {tilesPath, @"arch\up part big right.bmp", new int[2] {0, 23}, true, true},
                    new Object[5] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true, true}
                };
                if (!buildTile(files, spritesPath, type + "_29_fg.png")) return false;
            }

            // TILE_TORCH_WITH_DEBRIS
            files = new Object[]
            {
                new Object[5] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true, true},
                new Object[5] {tilesPath, @"floor panels\broken left.bmp", new int[2] {0, 63}, true, true},
                new Object[5] {tilesPath, @"floor panels\broken right.bmp", new int[2] {32, 62}, true, true},
                new Object[5] {tilesPath, @"background\torch.bmp", new int[2] {32, -28}, true, true}
            };
            if (!buildTile(files, spritesPath, type + "_30.png")) return false;
            files[1] = new Object[5] { tilesPath, @"floor panels\broken front.bmp", new int[2] { 0, 67 }, true, true};
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_30_fg.png")) return false;

            // Conversion is done!
            string mapPath = (color == -1 ? sheetPath + ".json" : "");
            return Util.packSprites(sheetPath + ".png", mapPath);
        }

        internal static Boolean buildTile(Object[] files, string path, string output, int width = -1, int height = -1, bool relativeNegative = true)
        {
            // Dimentions
            if (width == -1)
            {
                if (currentType == "dungeon") width = 60;
                else width = 64;
            }
            if (height == -1) height = 79;
            // Local variables
            Image image;
            Bitmap bitmap;
            Boolean result = true;
            float y = 0;
            try
            {
                bitmap = new Bitmap(width, height);
                foreach (Object[] file in files)
                {
                    string bmpPath = Path.Combine(file[0].ToString(), file[1].ToString());
                    if (tileColors.Length > 0 && (bool)file[4])
                    {
                        image = Util.getBitmapPalette(bmpPath, tileColors, (bool)file[3]);
                    }
                    else
                    {
                        if ((bool)file[3])
                        {
                            image = Util.getTransparentBitmap(bmpPath);
                        }
                        else
                        {
                            image = Image.FromFile(bmpPath);
                        }
                    }
                    int[] position = (int[])file[2];
                    if (image.Height > height)
                    {
                        int imgWidth = image.Width + position[0];
                        y = 0;
                        int imgHeight = image.Height + Math.Abs(position[1]);
                        bitmap = new Bitmap(imgWidth, imgHeight);
                    }
                    else
                    {
                        if (position[1] < 0 && relativeNegative)
                        {
                            y = height - image.Height + position[1];
                        }
                        else
                        {
                            y = position[1];
                        }
                    }
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(image, position[0], y, (float)image.Width, (float)image.Height);
                    }
                    if (image.Height > height) break;
                }
                bitmap.Save(Path.Combine(path, output));
                Util.images.Add(Path.Combine(path, output));
                Console.WriteLine("Tile built: {0}", Path.Combine(path, output));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error building tile: {0} {1}", Path.Combine(path, output), ex.Message);
                result = false;
            }
            return result;
        }

        private static Boolean buildDoor(Object[] files, string path, string output, int width = -1, int crop = -1)
        {
            // Dimentions
            if (width == -1)
            {
                if (currentType == "dungeon") width = 60;
                else width = 64;
            }
            int height = 79;
            // Local variables
            Image image;
            Bitmap bitmap;
            Boolean result = true;
            try
            {
                Object[] top = (Object[])files[1];
                string bmpPath = Path.Combine(top[0].ToString(), top[1].ToString());
                image = Image.FromFile(bmpPath);
                float start = 0, y = 0;
                int offset = image.Height - 16;
                if (offset < 0)
                {
                    start = Math.Abs(offset);
                    offset = 0;
                }
                height += offset;
                bitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    foreach (Object[] file in files)
                    {
                        bmpPath = Path.Combine(file[0].ToString(), file[1].ToString());
                        if (tileColors.Length > 0)
                        {
                            image = Util.getBitmapPalette(bmpPath, tileColors, (bool)file[3]);
                        }
                        else
                        {
                            if ((bool)file[3])
                            {
                                image = Util.getTransparentBitmap(bmpPath);
                            }
                            else
                            {
                                image = Image.FromFile(bmpPath);
                            }                           
                        }
                        int[] position = (int[])file[2];
                        if (position[1] > 0)
                            y = (float)position[1] + offset;
                        else if (position[1] == 0)
                            y = start;
                        else
                            y = height - image.Height + position[1];
                        g.DrawImage(image, (float)position[0], y, (float)image.Width, (float)image.Height);
                    }
                    if (crop > 0)
                    {
                        g.SetClip(new Rectangle(0, height - crop, width, crop), CombineMode.Replace);
                        g.Clear(Color.Transparent);
                    }
                }
                bitmap.Save(Path.Combine(path, output));
                Util.images.Add(Path.Combine(path, output));
                Console.WriteLine("Door built: {0}", Path.Combine(path, output));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error building door: {0} {1}", Path.Combine(path, output), ex.Message);
                result = false;
            }
            return result;
        }
    }
}