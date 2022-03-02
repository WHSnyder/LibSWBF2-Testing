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
    class SoundBanksTest
    {
        static int Main(string[] args)
        {               
            //Console.WriteLine("HERE");

            var tb = new TestBench();

            var c = tb.LoadAndTrackContainer(args[0], out Level level);

            SoundBank bank = level.Get<SoundBank>(args[1]);

            //Console.WriteLine("HERE");


            Sound[] sounds = bank.GetSounds();
            Console.WriteLine(String.Format("Got {0} sounds, has data? {1}", sounds.Length, bank.HasData));

            /*
            Dictionary<uint, Field> HashesToSoundProps = new Dictionary<uint, Field>();

            foreach (Config sndCfg in level.GetConfigs(EConfigType.Sound))
            {
                foreach (Field soundField in sndCfg.GetFields("SoundProperties"))
                {
                    uint name = soundField.Scope.GetUInt("Name");

                    Field sampleList = soundField.Scope.GetField("SampleList");
                    Field[] samples = sampleList.Scope.GetFields("Sample");
                    //if (samples.Length > 0)
                    //{
                    //}
                }
            }
            */


            int i = 0;
            foreach (Sound sound in sounds)
            {
                short[] buf = sound.GetPCM16();
                Console.WriteLine("  Sound: 0x{0:X} ({2}) with buffsize: {1}", sound.Name, buf.Length, sound.NumSamples * sound.NumChannels);
            
                if (buf.Length < 10000) continue;

                Console.WriteLine("    {0},{1},{2}", buf[0], buf[100], buf[2500]);

                SoundUtils.WriteToWAV(HashUtils.FNVToString(sound.Name) + ".wav", sound);

                i++;
                if (i > 5) break;
            }
            

            return 1;            
        }
    }
}
