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
    class PrintWav
    {
        static int Main(string[] args)
        {               
            string path = args[0];

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                br.BaseStream.Position = 0x2c;

                while (br.BaseStream.Position < br.BaseStream.Length - 16)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        var newInt = br.ReadInt16();

                        Console.Write(String.Format("{0,6}, ", newInt));
                    }
                    Console.Write("\n");
                }
            } 

            return 0;         
        }
    }
}
