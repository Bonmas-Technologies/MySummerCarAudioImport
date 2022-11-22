using System;
using System.Diagnostics;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            var info = process.StartInfo;
            info.FileName = "./ffmpeg/bin/ffmpeg.exe";
            info.Arguments = $"-i C:\\Games\\vortex.mp3 .\\test.ogg";
            process.Start();
        }
    }
}
