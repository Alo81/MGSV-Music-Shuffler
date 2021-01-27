using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MGSV_Music_Shuffler
{
    class TaggedSong
    {
        bool tagged { get { return !String.IsNullOrWhiteSpace(tags); } }

        string? tags;
        FileInfo? file;
        string? fileName;
        public int index;

        public TaggedSong(FileInfo file, string tags = "", int index = -1)
        {
            this.tags = tags.ToUpper() ?? null;
            this.file = file ?? null;
            this.index = index;

            if (tagged)
            {
                fileName = file?.Name?.Split(Defs.tagDelim)?[2]?.Trim();
                ProcessTags();
            }
            else
            {
                fileName = file.Name.Replace(Defs.tagDelim.ToString(), "").Trim();
                try
                {
                    Convert.ToInt32(fileName.Substring(0, 3));  //Check if first three characters are an int.  If yes, remove them.  Otherwise, just move on.
                    fileName = fileName.Substring(3, fileName.Length - 3).Trim();
                }
                catch
                {
                    // Do nothing.  It doesn't lead as an int, so its fine.  
                }
            }

            SetFileNameFromIndex();
        }
        public void UpdateFileName()
        {
            try
            {
                File.Move(file.FullName, file.DirectoryName + @"\" + fileName, true);
            }
            catch
            {
                Console.WriteLine(String.Format("Can't update song: {0}", file.Name));
            }
        }

        private void SetFileNameFromIndex()
        {
            fileName = String.Format(
                "{0} {1}{2}{3} {4}", 
                index.ToString("D3"), 
                tagged? Defs.tagDelim.ToString() : "", 
                tags,
                tagged ? Defs.tagDelim.ToString() : "",
                fileName
                );
        }

        private void ProcessTags()
        {
            foreach(var tag in Defs.tags)
            {
                if (tags.Contains(tag))
                {
                    switch (tag)
                    {
                        case 'H':
                            ProcessH();
                            break;
                        case 'S':
                            ProcessS();
                            break;
                    }
                }
            }
        }

        /*
        public void ProcessTag(char tag)
        {
            var methodName = String.Format("Process{0}", tag);
            var method = typeof(TaggedSong).GetMethod(methodName);
            var result = method.Invoke(null, new object[] { });
        }
        */

        private void ProcessH()
        {
            index = 0;
        }

        private void ProcessS()
        {
            try
            {
                index = Convert.ToInt32(file.Name.Substring(0, Defs.indexLen));
            }
            catch
            {
                Console.WriteLine("Song with stored location did not have a stored location.  Put it somewhere random.");
                var rng = new Random();
                index = rng.Next(1, 99);
            }
        }
    }
}
