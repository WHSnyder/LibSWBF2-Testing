using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using LibSWBF2.Logging;
using LibSWBF2.Wrappers;
using LibSWBF2.Types;
using LibSWBF2.Utils;
using LibSWBF2.Enums;

namespace LibSWBF2.NET.Test
{
    class SoundStreamsTest
    {
        static int Main(string[] args)
        {               
            var tb = new TestBench();

            var c = tb.LoadAndTrackContainer(args[0], out Level level);

            //SoundStream stream = level.Get<SoundStream>(args[1]);

            
            //Sound[] sounds = stream.GetSounds();
            //Console.WriteLine(String.Format("Stream {0} has {1} sounds", HashUtils.FNVToString(stream.Name), sounds.Length));

            /*
            foreach (Sound sound in sounds)
            {
                short[] buf = sound.GetPCM16();
                Console.WriteLine("  Sound: 0x{0:X} ({2}) with buffsize: {1}, num channels: {3} num samples: {4}", sound.Name, buf.Length, sound.NumSamples * sound.NumChannels, sound.NumChannels, sound.NumSamples);
                
                if (buf.Length < 10000) continue;

                Console.WriteLine("    {0},{1},{2}", buf[0], buf[100], buf[2500]);

                break;
            }
            */

            SoundStream str = level.Get<SoundStream>(0xfe6eae40);
            if (str == null)
            {
                Console.WriteLine("vo stream not found...");
                return 1;
            } 

            Sound segSnd = str.GetSound(0xe4466df0);
            if (segSnd == null)
            {
                Console.WriteLine("wookie segment not found...");
                return 1;
            } 

            Console.WriteLine("writing wav");

            SoundUtils.WriteToWAV("wookie_tst.wav", segSnd);
            
            return 1;            
        }
    }
}
