using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MGSV_Music_Shuffler
{
    class FileManager
    {
        List<TaggedSong> taggedSongs;
        List<FileInfo> untaggedSongs;
        List<TaggedSong> sortedSongs;

        public FileManager()
        {
            taggedSongs = new List<TaggedSong>();
            untaggedSongs = new List<FileInfo>();
            sortedSongs = new List<TaggedSong>();
        }

        public void ShuffleMusic()
        {
            var timer = new Stopwatch();
            var dir = GetMusicDir();
            if (!IsMusicDir(dir))
            {
                Console.WriteLine("Please place application in MGSV game directory. ");
                Environment.Exit(0);
            }
            timer.Start();
            Console.WriteLine("Sort into tagged vs untagged songs.  Elapsed time: " + timer.ElapsedMilliseconds);
            OrganizeTags(dir);

            Console.WriteLine("Process tags and randomize order.  Elapsed time: " + timer.ElapsedMilliseconds);
            ProcessSongs();

            Console.WriteLine("Update file names.  Elapsed time: " + timer.ElapsedMilliseconds);
            UpdateSongNames();

            Console.WriteLine("Complete.  Closing in 3 seconds.  Elapsed time: " + timer.ElapsedMilliseconds);
            timer.Stop();
        }

        private void OrganizeTags(string dir)
        {
            // Grab files.  
            foreach (var ext in Defs.exts)
            {
                var files = new DirectoryInfo(dir).GetFiles(ext);
                foreach (var file in files)
                {
                    var tags = GetTags(file.Name);
                    if (!String.IsNullOrWhiteSpace(tags))
                    {
                        taggedSongs.Add(new TaggedSong(file, tags:tags));
                    }
                    else
                    {
                        untaggedSongs.Add(file);
                    }
                }
            }
        }

        private void ProcessSongs()
        {
            try
            {
                var usedIndexes = taggedSongs.Select(x => x.index);
                var totalSongs = taggedSongs.Count + untaggedSongs.Count;

                Random rnd = new Random();
                for (int i = 0; i < totalSongs; i++)
                {
                    if (usedIndexes.Contains(i))
                    {
                        sortedSongs.Add(taggedSongs.Where(x => x.index == i).First());
                    }
                    else
                    {
                        var song = PullSongFromSongs(rnd.Next(0, untaggedSongs.Count));
                        sortedSongs.Add(new TaggedSong(song, index: i));
                    }
                }
            }
            catch
            {

            }
        }

        private void UpdateSongNames()
        {
            foreach(var song in sortedSongs)
            {
                song.UpdateFileName();
            }
        }

        private FileInfo PullSongFromSongs(int rng)
        {
            var song = untaggedSongs[rng];
            untaggedSongs.RemoveAt(rng);
            return song;
        }

        private static string GetMusicDir()
        {
            var dir = FileHelpers.GetCurrentDirectory();
            if (!IsMusicDir(dir))
            {
                dir = String.Concat(dir, Defs.musicDir);
            }

            return dir;
        }

        private static bool IsMusicDir(string dir)
        {
            return FileHelpers.GetLastFolder(dir).Equals(Defs.musicDir);
        }

        // Tag format.  [NUMS] #[TAGS]# FILE NAME.[EXT]
        private static string? GetTags(string file)
        {
            if (!IsTagged(file)) { return null; }

            return file.Split(Defs.tagDelim)?[1];
        }

        public static Tuple<int, int>? GetDelimLocations(string file)
        {
            List<int> locs = new List<int>();

            for(int i = 0; locs.Count <2 && i < file.Length; i++)
            {
                if(file[i] == Defs.tagDelim)
                {
                    locs.Add(i);
                }
            }

            if(locs.Count == 2)
            {
                return new Tuple<int, int>(locs[0]+1, (locs[1]-1) - locs[0]);
            }

            return null;
        }

        private static bool IsTagged(string file)
        {
            return file.Count(x => x == Defs.tagDelim) >= 2;
        }
    }
}
