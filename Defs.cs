using System;
using System.Collections.Generic;
using System.Text;

namespace MGSV_Music_Shuffler
{
    class Defs
    {
        public static string musicDir = @"CustomSoundtrack";
        public static string steamLaunchCommand = "steam steam://rungameid/287700";
        public static string gameDir = @"MGS_TPP";
        public static string gameExe = @"mgsvtpp.exe";

        public static List<string> exts = new List<string> { "*.mp3" };

        public static List<char> tags = new List<char> { 'H', 'S' };    // Helicopter.  Always put song in same position.  

        public static char tagDelim = '#';
        public static int indexLen = 3;
    }
}
