using System;
using System.IO;

namespace popsc
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialization
            Console.WriteLine("");
            Console.WriteLine("PoP 1 Sprites Converter v1.0 - (C)2016 Marcelo Lv Cabral");
            Console.WriteLine("Convert bmp extracted from PoP 1 dat files into transparent png sprites");
            Console.WriteLine("<< Focused on converting Mods to Prince of Persia for Roku >>");
            Console.WriteLine("<< Source code is avaliable at: http://github.com/lvcabral >>");
            if (args.Length >= 2)
            {
                if (!Directory.Exists(args[0]))
                {
                    Console.WriteLine("PR resources folder does not exist!");
                    return;
                }
                if (!Directory.Exists(args[1]))
                {
                    Console.WriteLine("Output folder does not exist!");
                    return;
                }
                Tiles.palaceWall pwm = Tiles.palaceWall.changePalette;
                if (args.Length > 2)
                {
                    string[] param = args[2].Split('=');
                    int value = -1;
                    if (param.Length == 2 && param[0] == "-pwm" && int.TryParse(param[1], out value) )
                    {
                        if (value >= 0 && value <= 2)
                        {
                            pwm = (Tiles.palaceWall) value;
                        }
                        else
                        {
                            help();
                            return;
                        }
                    }
                    else
                    {
                        help();
                        return;
                    }
                }
                Tiles.convertTiles("dungeon", args[0], args[1]);
                Tiles.convertTiles("palace", args[0], args[1], pwm);
                Kid.convertKid(args[0], args[1]);
                Guards.convertGuards(args[0], args[1]);
                Guards.convertSpecialGuards(args[0], args[1]);
                Actors.convertActors(args[0], args[1]);
                General.convertGeneral(args[0], args[1]);
                Scenes.convertScenes(args[0], args[1]);
                Titles.convertTitles(args[0], args[1]);
                Console.ReadKey();
            }
            else
            {
                help();
            }
        }

        private static void help()
        {
            Console.WriteLine("");
            Console.WriteLine("Usage:");
            Console.WriteLine("popsc <PR resources path> <sprites output path> [-pwm=<palace marks mode>]");
            Console.WriteLine("Optional parameter:");
            Console.WriteLine("0 : Change palace wall marks palette to the 15th color of wall.pal (default)");
            Console.WriteLine("1 : Keep palace wall marks pallete from the bmp files");
            Console.WriteLine("2 : Special palace wall marks configuration for SNES Mods");
        }
    }
}
