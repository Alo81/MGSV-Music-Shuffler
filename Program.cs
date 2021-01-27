using MGSV_Music_Shuffler;
using System;
using System.Diagnostics;
using System.IO;

namespace MGSV_Shuffler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var music = new FileManager();
                music.ShuffleMusic();
                var timer = new Stopwatch();

                //FileHelpers.LaunchGame();

                timer.Start();
                while (timer.ElapsedMilliseconds < 3000)
                {
                    System.Threading.Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
