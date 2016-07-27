using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace popsc
{
    internal class Tiles
    {
        internal static string currentType;
        internal enum palaceWall
        {
            changePalette,
            keepPalette,
            SNES
        }

        internal static bool convertTiles(string type, string inputPath, string outputPath, palaceWall wall = palaceWall.changePalette)
        {
            // Building Dungeon tiles
            Object[] files, file;
            List<Object[]> parts;
            currentType = type;
            string tilesPath = Path.Combine(inputPath, "v" + type);
            string objectsPath = Path.Combine(inputPath, "prince");
            string spritesPath = Path.Combine(outputPath, @"tiles\" + type);
            if (!Directory.Exists(spritesPath))
            {
                Directory.CreateDirectory(spritesPath);
            }

            // TILE_SPACE
            if (!buildTile(new Object[] { }, spritesPath, type + "_0.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_0_0.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_0_fg.png")) return false;
            file = new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_0_1.png")) return false;
            file = new Object[4] { tilesPath, @"background\bricks04.bmp", new int[2] { 32, -23 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_0_2.png")) return false;
            file = new Object[4] { tilesPath, @"background\window.bmp", new int[2] { 32, -3 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_0_3.png")) return false;

            // TILE_FLOOR
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (!buildTile(files, spritesPath, type + "_1.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_1_fg.png")) return false;
            if (type == "dungeon")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_1_0.png")) return false;
                file = new Object[4] { tilesPath, @"background\bricks01.bmp", new int[2] { 32, -23 }, true };
                if (!buildTile(new Object[] { file }, spritesPath, type + "_1_1.png")) return false;
            }
            else
            {
                file = new Object[4] { tilesPath, @"background\bricks01.bmp", new int[2] { 32, -23 }, true };
                if (!buildTile(new Object[] { file }, spritesPath, type + "_1_0.png")) return false;
                if (!buildTile(new Object[] { }, spritesPath, type + "_1_1.png")) return false;
            }
            file = new Object[4] { tilesPath, @"background\bricks02.bmp", new int[2] { 32, -23 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_1_2.png")) return false;

            // TILE_SPIKES
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\spikes left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\spikes right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true});
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_2.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_2_fg.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_2_0.png")) return false;
            if (!buildTile(new Object[] { }, spritesPath, type + "_2_0_fg.png")) return false;
            files = new Object[]
            {
                new Object[4] {tilesPath, @"spikes\frame01 left.bmp", new int[2] {0, 64}, true},
                new Object[4] {tilesPath, @"spikes\frame01 right.bmp", new int[2] {32, 66}, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_1.png")) return false;
            file = new Object[4] { tilesPath, @"spikes\frame01 front.bmp", new int[2] { 0, 69 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_1_fg.png")) return false;
            files = new Object[]
            {
                new Object[4] {tilesPath, @"spikes\frame02 left.bmp", new int[2] {0, 54}, true},
                new Object[4] {tilesPath, @"spikes\frame02 right.bmp", new int[2] {32, 54}, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_2.png")) return false;
            file = new Object[4] { tilesPath, @"spikes\frame02 front.bmp", new int[2] { 0, 61 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_2_fg.png")) return false;
            files = new Object[]
            {
                new Object[4] {tilesPath, @"spikes\frame03 left.bmp", new int[2] {0, 47}, true},
                new Object[4] {tilesPath, @"spikes\frame03 right.bmp", new int[2] {32, 54}, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_3.png")) return false;
            file = new Object[4] { tilesPath, @"spikes\frame03 front.bmp", new int[2] { 0, 50 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_3_fg.png")) return false;
            files = new Object[]
            {
                new Object[4] {tilesPath, @"spikes\frame04 left.bmp", new int[2] {0, 48}, true},
                new Object[4] {tilesPath, @"spikes\frame04 right.bmp", new int[2] {32, 49}, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_4.png")) return false;
            file = new Object[4] { tilesPath, @"spikes\frame04 front.bmp", new int[2] { 0, 51 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_4_fg.png")) return false;
            files = new Object[]
            {
                new Object[4] {tilesPath, @"spikes\frame05 left.bmp", new int[2] {0, 48}, true},
                new Object[4] {tilesPath, @"spikes\frame05 right.bmp", new int[2] {32, 50}, true}
            };
            if (!buildTile(files, spritesPath, type + "_2_5.png")) return false;
            file = new Object[4] { tilesPath, @"spikes\frame05 front.bmp", new int[2] { 0, 53 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_2_5_fg.png")) return false;

            // TILE_PILLAR
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"pillar\pillar left.bmp", new int[2] {0, 16}, true},
                new Object[4] {tilesPath, @"pillar\pillar right main.bmp", new int[2] {32, 16}, true},
                new Object[4] {tilesPath, @"pillar\pillar right top.bmp", new int[2] {32, 9}, true}
            };
            if (!buildTile(files, spritesPath, type + "_3.png")) return false;
            if (type == "dungeon")
                files[1] = new Object[4] { tilesPath, @"pillar\pillar front.bmp", new int[2] { 8, 16 }, false };
            else
                files[1] = new Object[4] { tilesPath, @"pillar\pillar front.bmp", new int[2] { 8, 16 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_3_fg.png")) return false;

            // TILE_GATE
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true},
                new Object[4] {tilesPath, @"door\door frame right.bmp", new int[2] {32, 16}, true},
                new Object[4] {tilesPath, @"door\door frame right top.bmp", new int[2] {32, 3}, true}
            };
            if (!buildTile(files, spritesPath, type + "_4.png")) return false;
            files[1] = new Object[4] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_4_fg.png")) return false;
            files = new Object[]
            {
                new Object[4] {tilesPath, @"door\res00260.bmp", new int[2] {32, 7}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {32, 15}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {32, 23}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {32, 31}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {32, 39}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {32, 47}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {32, 55}, true},
                new Object[4] {tilesPath, @"door\res00251.bmp", new int[2] {32, 63}, true}
            };
            if (!buildTile(files, spritesPath, type + "_gate.png")) return false;
            files = new Object[]
            {
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {0, -1}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {0, 7}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {0, 15}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {0, 23}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {0, 31}, true},
                new Object[4] {tilesPath, @"door\res00252.bmp", new int[2] {0, 39}, true},
                new Object[4] {tilesPath, @"door\res00251.bmp", new int[2] {0, 47}, true}
            };
            if (!buildTile(files, spritesPath, type + "_gate_fg.png", 8, 57)) return false;

            // TILE_DROP_BUTTON
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\closer base unpressed.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_6.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_6_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\closer base pressed.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 64}, true},
                    new Object[4] {tilesPath, @"floor panels\closer right pressed.bmp", new int[2] {32, 63}, true}
                };
            }
            else
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\closer base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 64}, true},
                    new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 64}, true},
                    new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true }
                };
            }
            if (!buildTile(files, spritesPath, type + "_6_down.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_6_down_fg.png")) return false;

            // TILE_TAPESTRY
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true},
                    new Object[4] {tilesPath, @"arch\up part with carpet wall.bmp", new int[2] {32, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_7.png")) return false;
                files[1] = new Object[4] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true };
                if (!buildTile(files, spritesPath, type + "_7_fg.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true},
                    new Object[4] {tilesPath, @"carpets\style01.bmp", new int[2] {32, 16}, true},
                    new Object[4] {tilesPath, @"carpets\style01 top.bmp", new int[2] {32, 4}, true}
                };
                if (!buildTile(files, spritesPath, type + "_7_1.png")) return false;
                Rectangle[] rects = new Rectangle[2] { new Rectangle(0, 0, 25, 76), new Rectangle(40, 0, 24, 79) };
                Util.clipBitmap(Path.Combine(spritesPath, type + "_7_1.png"), Path.Combine(spritesPath, type + "_7_1_fg.png"), rects);
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"door\door frame left.bmp", new int[2] {0, 16}, true},
                    new Object[4] {tilesPath, @"carpets\style02.bmp", new int[2] {32, 16}, true},
                    new Object[4] {tilesPath, @"carpets\style02 top.bmp", new int[2] {32, 4}, true}
                };
                if (!buildTile(files, spritesPath, type + "_7_2.png")) return false;
                Util.clipBitmap(Path.Combine(spritesPath, type + "_7_2.png"), Path.Combine(spritesPath, type + "_7_2_fg.png"), rects);
            }

            // TILE_BOTTOM_BIG_PILLAR
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"big pillar\big pillar lower left.bmp", new int[2] {0, 16}, true},
                new Object[4] {tilesPath, @"big pillar\big pillar lower right.bmp", new int[2] {32, 16}, true}
            };
            if (!buildTile(files, spritesPath, type + "_8.png")) return false;
            files[1] = new Object[4] { tilesPath, @"big pillar\big pillar lower front.bmp", new int[2] { 8, 16 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_8_fg.png")) return false;

            // TILE_TOP_BIG_PILLAR
            files = new Object[]
            {
                new Object[4] {tilesPath, @"big pillar\big pillar upper left.bmp", new int[2] {8, 16}, true},
                new Object[4] {tilesPath, @"big pillar\big pillar upper right.bmp", new int[2] {32, 16}, true},
                new Object[4] {tilesPath, @"big pillar\big pillar upper right top.bmp", new int[2] {32, 10}, true}
            };
            if (!buildTile(files, spritesPath, type + "_9.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_9_fg.png")) return false;

            // TILE_POTION
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_10.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_10_fg.png")) return false;
            files[1] = new Object[4] { objectsPath, @"potions\base\small dungeon.bmp", new int[2] { 22, 62 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_10_fg_1.png")) return false;
            files[1] = new Object[4] { objectsPath, @"potions\base\big dungeon.bmp", new int[2] { 22, 58 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_10_fg_2.png")) return false;
            files[1] = new Object[4] { objectsPath, @"potions\base\big dungeon.bmp", new int[2] { 22, 58 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_10_fg_3.png")) return false;
            files[1] = new Object[4] { objectsPath, @"potions\base\big dungeon.bmp", new int[2] { 22, 58 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_10_fg_4.png")) return false;
            files[1] = new Object[4] { objectsPath, @"potions\base\small dungeon.bmp", new int[2] { 22, 62 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_10_fg_5.png")) return false;

            // TILE_LOOSE_BOARD
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_11.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_11_fg.png")) return false;
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\loose base01.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\loose left01.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\loose right01.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_loose_1.png")) return false;
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\loose base02.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\loose left02.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\loose right02.bmp", new int[2] {32, 63}, true}
            };
            if (!buildTile(parts.ToArray(), spritesPath, type + "_falling.png")) return false;
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_loose_3.png")) return false;
            if (!buildTile(files, spritesPath, type + "_loose_4.png")) return false;
            if (!buildTile(files, spritesPath, type + "_loose_8.png")) return false;
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
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
                    new Object[4] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true},
                    new Object[4] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true},
                    new Object[4] {tilesPath, @"arch\up part with carpet wall.bmp", new int[2] {32, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_12.png")) return false;
                if (!buildTile(files, spritesPath, type + "_12_fg.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true},
                    new Object[4] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true},
                    new Object[4] {tilesPath, @"carpets\style01.bmp", new int[2] {32, 16}, true},
                    new Object[4] {tilesPath, @"carpets\style01 top.bmp", new int[2] {32, 4}, true}
                };
                if (!buildTile(files, spritesPath, type + "_12_1.png")) return false;
                Rectangle[] rects = new Rectangle[1] { new Rectangle(40, 0, 24, 79) };
                Util.clipBitmap(Path.Combine(spritesPath, type + "_12_1.png"), Path.Combine(spritesPath, type + "_12_1_fg.png"), rects);
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true},
                    new Object[4] {tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true},
                    new Object[4] {tilesPath, @"carpets\style02.bmp", new int[2] {32, 16}, true},
                    new Object[4] {tilesPath, @"carpets\style02 top.bmp", new int[2] {32, 4}, true}
                };
                if (!buildTile(files, spritesPath, type + "_12_2.png")) return false;
                Util.clipBitmap(Path.Combine(spritesPath, type + "_12_2.png"), Path.Combine(spritesPath, type + "_12_2_fg.png"), rects);
            }
            else
            {
                files = new Object[]
                {
                    new Object[4] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 16 }, true },
                    new Object[4] { tilesPath, @"door\door frame left pole.bmp", new int[2] { 24, 20 }, true }
                };
                if (!buildTile(files, spritesPath, type + "_12_fg.png")) return false;
            }

            // TILE_MIRROR
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"mirror\mirror and floor.bmp", new int[2] {0, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_13.png")) return false;
                files[1] = new Object[4] { tilesPath, @"mirror\mirror.bmp", new int[2] { 0, 16 }, true };
                if (!buildTile(files, spritesPath, type + "_13_fg.png")) return false;
            }

            // TILE_DEBRIS
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\broken left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\broken right.bmp", new int[2] {32, 62}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_14.png")) return false;
            files[1] = new Object[4] { tilesPath, @"floor panels\broken front.bmp", new int[2] { 0, 67 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_14_fg.png")) return false;

            // TILE_RAISE_BUTTON
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\opener base unpressed.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\opener left unpressed.bmp", new int[2] {0, 62}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 62}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_15.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_15_fg.png")) return false;
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_15_down.png")) return false;

            // TILE_EXIT_LEFT
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_16.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_16_fg.png")) return false;

            // TILE_EXIT_RIGHT
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, false},
                new Object[4] {tilesPath, @"level door\frame upper left.bmp", new int[2] {0, 0}, false},
                new Object[4] {tilesPath, @"level door\frame lower left.bmp", new int[2] {0, 16}, true},
                new Object[4] {tilesPath, @"level door\frame upper right.bmp", new int[2] {32, 0}, false},
                new Object[4] {tilesPath, @"level door\frame lower right.bmp", new int[2] {32, 16}, true},
                new Object[4] {tilesPath, @"level door\stairs.bmp", new int[2] {8, 14}, false},
                new Object[4] {tilesPath, @"level door\floor.bmp", new int[2] {8, 59}, false}
            };
            if (!buildDoor(files, spritesPath, type + "_17.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_17_fg.png")) return false;
            files = new Object[]
                {
                    new Object[4] {tilesPath, @"level door\frame upper left.bmp", new int[2] {0, 0}, true},
                    new Object[4] {tilesPath, @"level door\frame upper right.bmp", new int[2] {32, 0}, true},
                    new Object[4] {tilesPath, @"level door\frame lower right.bmp", new int[2] {32, 16}, true}
                };
            if (!buildDoor(files, spritesPath, type + "_door_fg.png", -1, 16)) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 0}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 4}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 8}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 12}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 16}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 20}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 24}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 28}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 32}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 36}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 40}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {0, 44}, false}
                };
                if (!buildTile(files, spritesPath, type + "_door.png", 42, 50)) return false;
            }
            else
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 0}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 1}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 5}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 9}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 13}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 17}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 21}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 25}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 29}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 33}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 37}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 41}, false},
                    new Object[4] {tilesPath, @"level door\door bottom.bmp", new int[2] {1, 45}, false}
                };
                if (!buildTile(files, spritesPath, type + "_door.png", 49, 51)) return false;
            }
            // TILE_SLICER
            parts = new List<Object[]>
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
            };
            if (type == "palace") parts.Add(new Object[4] { tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true });
            files = parts.ToArray();
            if (!buildTile(files, spritesPath, type + "_18.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_18_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"chomper\frame02 top.bmp", new int[2] {0, 16}, true},
                    new Object[4] {tilesPath, @"chomper\frame02 bottom.bmp", new int[2] {0, 46}, true}
                };
                if (!buildTile(files, spritesPath, type + "_slicer_1.png")) return false;
            }
            else
            {
                file = new Object[4] { tilesPath, @"chomper\frame02.bmp", new int[2] { 0, 16 }, true };
                if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_1.png")) return false;
            }
            file = new Object[4] { tilesPath, @"chomper\frame02 front.bmp", new int[2] { 0, 16 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_1_fg.png")) return false;
            file = new Object[4] { tilesPath, @"chomper\frame03.bmp", new int[2] { 0, 16 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_2.png")) return false;
            file = new Object[4] { tilesPath, @"chomper\frame03 front.bmp", new int[2] { 0, 16 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_2_fg.png")) return false;
            file = new Object[4] { tilesPath, @"chomper\frame04.bmp", new int[2] { 0, 16 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_3.png")) return false;
            file = new Object[4] { tilesPath, @"chomper\frame04 front.bmp", new int[2] { 0, 16 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_3_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"chomper\frame05 top.bmp", new int[2] {0, 16}, true},
                    new Object[4] {tilesPath, @"chomper\frame05 bottom.bmp", new int[2] {0, 59}, true}
                };
                if (!buildTile(files, spritesPath, type + "_slicer_4.png")) return false;
            }
            else
            {
                file = new Object[4] { tilesPath, @"chomper\frame05.bmp", new int[2] { 0, 16 }, true };
                if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_4.png")) return false;
            }
            file = new Object[4] { tilesPath, @"chomper\frame05 front.bmp", new int[2] { 0, 16 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_4_fg.png")) return false;
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"chomper\frame01 top.bmp", new int[2] {0, 16}, true},
                    new Object[4] {tilesPath, @"chomper\frame01 bottom.bmp", new int[2] {0, 55}, true}
                };
                if (!buildTile(files, spritesPath, type + "_slicer_5.png")) return false;
            }
            else
            {
                file = new Object[4] { tilesPath, @"chomper\frame01.bmp", new int[2] { 0, 16 }, true };
                if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_5.png")) return false;
            }
            file = new Object[4] { tilesPath, @"chomper\frame01 front.bmp", new int[2] { 0, 16 }, true };
            if (!buildTile(new Object[] { file }, spritesPath, type + "_slicer_5_fg.png")) return false;

            // TILE_TORCH
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true},
                new Object[4] {tilesPath, @"background\torch.bmp", new int[2] {32, 36}, true}
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
                    new Object[4] {tilesPath, @"walls\face stack top.bmp", new int[2] {32, 4}, true},
                    new Object[4] {tilesPath, @"walls\face stack main.bmp", new int[2] {32, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_wall_0.png")) return false;
            }
            else
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true },
                    new Object[4] {tilesPath, @"walls\face stack top.bmp", new int[2] {32, 3}, true},
                    new Object[4] {tilesPath, @"walls\face stack main.bmp", new int[2] {32, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_wall_0.png")) return false;
                files = new Object[2] {files[1], files[2]};
            }
            if (!buildTile(files, spritesPath, type + "_wall_1.png")) return false;

            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"walls\single stack main.bmp", new int[2] {0, 16}, false},
                    new Object[4] {tilesPath, @"walls\single stack base.bmp", new int[2] {0, 76}, false}
                };
                if (!buildTile(files, spritesPath, "SWS.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"walls\left stack main.bmp", new int[2] {0, 16}, false},
                    new Object[4] {tilesPath, @"walls\left stack base.bmp", new int[2] {0, 76}, false}
                };
                if (!buildTile(files, spritesPath, "SWW.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"walls\right stack main.bmp", new int[2] {0, 16}, false},
                    new Object[4] {tilesPath, @"walls\right stack base.bmp", new int[2] {0, 76}, false},
                    new Object[4] {tilesPath, @"walls\divider01.bmp", new int[2] {2, 58}, true},
                    new Object[4] {tilesPath, @"walls\divider02.bmp", new int[2] {12, 37}, true}
                };
                if (!buildTile(files, spritesPath, "WWS.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"walls\centre stack main.bmp", new int[2] {0, 16}, false},
                    new Object[4] {tilesPath, @"walls\centre stack base.bmp", new int[2] {0, 76}, false}
                };
                if (!buildTile(files, spritesPath, "WWW.png")) return false;
            }
            else if (wall == palaceWall.changePalette)
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
            else if (wall == palaceWall.keepPalette)
            {
                int res = 363;
                for (int c = 0; c < 15; c++)
                {
                    if (!Util.convertBitmap(Path.Combine(tilesPath, @"walls\res" + res.ToString() + ".bmp"), Path.Combine(spritesPath, "W_" + c.ToString() + ".png"))) return false;
                    res++;
                }
            }
            else // SNES
            {
                int res = 373;
                for (int c = 0; c < 15; c++)
                {
                    if (c < 9)
                    {
                        if (!Util.convertBitmap(Path.Combine(tilesPath, @"walls\res" + res.ToString() + ".bmp"), Path.Combine(spritesPath, "W_" + c.ToString() + ".png"))) return false;
                    }
                    else if (c < 12)
                    {
                        if (!Util.convertBitmap(Path.Combine(tilesPath, @"walls\res364.bmp"), Path.Combine(spritesPath, "W_" + c.ToString() + ".png"), false)) return false;
                    }
                    else
                    {
                        if (!Util.convertBitmap(Path.Combine(tilesPath, @"walls\res363.bmp"), Path.Combine(spritesPath, "W_" + c.ToString() + ".png"), false)) return false;
                    }
                }
            }

            // TILE_SKELETON
            if (type == "dungeon")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"floor panels\skeleton left.bmp", new int[2] {0, 57}, true},
                    new Object[4] {tilesPath, @"floor panels\skeleton right.bmp", new int[2] {32, 57}, true}
                };
                if (!buildTile(files, spritesPath, type + "_21.png")) return false;
                if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_21_fg.png")) return false;
            }

            // TILE_SWORD
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true},
                new Object[4] {objectsPath, @"sword\in the floor\normal.bmp", new int[2] {0, 63}, true}
            };
            if (!buildTile(files, spritesPath, type + "_22.png")) return false;
            files[3] = new Object[4] { objectsPath, @"sword\in the floor\bright.bmp", new int[2] { 0, 63 }, true };
            if (!buildTile(files, spritesPath, type + "_22_bright.png")) return false;
            if (!buildTile(new Object[] { files[0] }, spritesPath, type + "_22_fg.png")) return false;

            // TILE_BALCONY_LEFT
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true },
                    new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                    new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
                };
                if (!buildTile(files, spritesPath, type + "_23.png")) return false;
                if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_23_fg.png")) return false;
            }

            // TILE_BALCONY_RIGHT
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true },
                    new Object[4] {tilesPath, @"floor panels\normal left.bmp", new int[2] {0, 63}, true},
                    new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
                };
                if (!buildTile(files, spritesPath, type + "_24.png")) return false;
                if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_24_fg.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"balcony\upper left.bmp", new int[2] {0, 0}, true},
                    new Object[4] {tilesPath, @"balcony\upper right.bmp", new int[2] {32, 0}, true },
                    new Object[4] {tilesPath, @"balcony\lower left.bmp", new int[2] {0, 20}, true},
                    new Object[4] {tilesPath, @"balcony\lower right.bmp", new int[2] {32, 20}, true}
                };
                if (!buildTile(files, spritesPath, type + "_balcony.png")) return false;
            }

            // TILE_LATTICE_PILLAR
            if (type == "palace")
            {
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"pillar\pillar left.bmp", new int[2] {0, 16}, true},
                    new Object[4] {tilesPath, @"background\bricks03.bmp", new int[2] { 32, -23 }, true },
                    new Object[4] {tilesPath, @"floor panels\normal right.bmp", new int[2] {32, 63}, true}
                };
                if (!buildTile(files, spritesPath, type + "_25.png")) return false;
                files[1] = new Object[4] { tilesPath, @"pillar\pillar front.bmp", new int[2] { 8, 16 }, true };
                if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_25_fg.png")) return false;
            }

            // TILE_LATTICE_SUPPORT
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_26.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"arch\down part base.bmp", new int[2] {0, 76}, true},
                    new Object[4] {tilesPath, @"arch\down part.bmp", new int[2] {0, 23}, true},
                    new Object[4] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_26_fg.png")) return false;
            }

            // TILE_SMALL_LATTICE
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_27.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"arch\up part small.bmp", new int[2] {0, 23}, true},
                    new Object[4] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_27_fg.png")) return false;
            }

            // TILE_LATTICE_LEFT
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_28.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"arch\up part big left.bmp", new int[2] {0, 23}, true},
                    new Object[4] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_28_fg.png")) return false;
            }

            // TILE_LATTICE_RIGHT
            if (type == "palace")
            {
                if (!buildTile(new Object[] { }, spritesPath, type + "_29.png")) return false;
                files = new Object[]
                {
                    new Object[4] {tilesPath, @"arch\up part big right.bmp", new int[2] {0, 23}, true},
                    new Object[4] {tilesPath, @"arch\other.bmp", new int[2] {0, 16}, true}
                };
                if (!buildTile(files, spritesPath, type + "_29_fg.png")) return false;
            }

            // TILE_TORCH_WITH_DEBRIS
            files = new Object[]
            {
                new Object[4] {tilesPath, @"floor panels\normal base.bmp", new int[2] {0, 76}, true},
                new Object[4] {tilesPath, @"floor panels\broken left.bmp", new int[2] {0, 63}, true},
                new Object[4] {tilesPath, @"floor panels\broken right.bmp", new int[2] {32, 62}, true},
                new Object[4] {tilesPath, @"background\torch.bmp", new int[2] {32, 36}, true}
            };
            if (!buildTile(files, spritesPath, type + "_30.png")) return false;
            files[1] = new Object[4] { tilesPath, @"floor panels\broken front.bmp", new int[2] { 0, 67 }, true };
            if (!buildTile(new Object[] { files[0], files[1] }, spritesPath, type + "_30_fg.png")) return false;
            // Conversion is done!
            return true;
        }

        internal static Boolean buildTile(Object[] files, string path, string output, int width = -1, int height = -1)
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
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    foreach (Object[] file in files)
                    {
                        string bmpPath = Path.Combine(file[0].ToString(), file[1].ToString());
                        if ((bool)file[3])
                        {
                            image = Util.getTransparentBitmap(bmpPath);
                        }
                        else
                        {
                            image = Image.FromFile(bmpPath);
                        }
                        int[] position = (int[])file[2];
                        if (position[1] < 0)
                        {
                            y = height - image.Height + position[1];
                        }
                        else
                        {
                            y = position[1];
                        }
                        g.DrawImage(image, (float)position[0], y, (float)image.Width, (float)image.Height);
                        if (image.Height > height) break;
                    }
                }
                bitmap.Save(Path.Combine(path, output));
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
                        if ((bool)file[3])
                        {
                            image = Util.getTransparentBitmap(bmpPath);
                        }
                        else
                        {
                            image = Image.FromFile(bmpPath);
                        }
                        int[] position = (int[])file[2];
                        if (position[1] > 0)
                            y = (float)position[1] + offset;
                        else
                            y = start;
                        g.DrawImage(image, (float)position[0], y, (float)image.Width, (float)image.Height);
                    }
                    if (crop > 0)
                    {
                        g.SetClip(new Rectangle(0, height - crop, width, crop), CombineMode.Replace);
                        g.Clear(Color.Transparent);
                    }
                }
                bitmap.Save(Path.Combine(path, output));
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