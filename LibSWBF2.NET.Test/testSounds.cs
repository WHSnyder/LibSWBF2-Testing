using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using LibSWBF2.Logging;
using LibSWBF2.Wrappers;
using LibSWBF2.Types;

namespace LibSWBF2.NET.Test
{
    class ScriptsTest
    {
        static int Main(string[] args)
        {               
            var tb = new TestBench();

            //var c = tb.LoadAndTrackContainerSB(args[0], out Level level);
            var c = tb.LoadAndTrackContainer(args[0], out Level level);

            /*
            if (level == null)
            {
                string tstSnd = "ui_load_lp";
                var sound = c.Get<Sound>(tstSnd);

                if (sound != null)
                {
                    Console.WriteLine("Found sound: " + tstSnd);
                }
                else 
                {
                    Console.WriteLine("Couldn't find sound: " + tstSnd);                
                }
            }
            else 
            {
                var sounds = level.Get<Sound>();

                Console.WriteLine("Found {0} sounds in level: {1}", sounds.Length, args[0]);

                foreach (var sound in sounds)
                {
                    Console.WriteLine(sound.Name);
                } 
            }
            */

            return 1;            
        }
    }
}
