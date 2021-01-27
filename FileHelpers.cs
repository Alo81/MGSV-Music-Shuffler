using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MGSV_Music_Shuffler
{
    class FileHelpers
    {
        public static string GetLastFolder(string path)
        {
            return new DirectoryInfo(path).Name;
        }
        public static string GetCurrentDirectory()
        {
            return System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\";
        }

        public static string GetGameLocation()
        {
            var dir = GetCurrentDirectory();

            if (!IsGameDirectory(dir))
            {
                var folderLoc = dir.LastIndexOf(Defs.gameDir);

                if(folderLoc >= 0)
                {
                    dir = dir.Substring(0, folderLoc + Defs.gameDir.Length);
                }
                else
                {
                    return null;
                }
            }

            //Its the game directory.  Lets launch.  
            return dir + Defs.gameExe;
        }

        public static bool IsGameDirectory(string dir)
        {
            return FileHelpers.GetLastFolder(dir).Equals(Defs.gameDir);
        }

        public static void LaunchGame()
        {
            Process.Start(Defs.steamLaunchCommand);
        }
    }
}
