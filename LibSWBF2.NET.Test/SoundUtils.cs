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
    class SoundUtils
    {
        public static bool WriteToWAV(string path, Sound sound, short[] data_ = null)
        {
            if (!sound.HasData && data_ == null) return false;
            
            ushort numchannels = (ushort) sound.NumChannels;
            ushort samplelength = 2; // in bytes
            uint samplerate = (uint) sound.SampleRate;

            short[] data = data_ == null ? sound.GetPCM16() : data_;
            uint numsamples = data_ == null ? (uint) sound.NumSamples : (uint) data_.Length;


            FileStream f = new FileStream(path, FileMode.Create);
            BinaryWriter wr = new BinaryWriter(f);

            wr.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
            wr.Write((uint) (36 + numsamples * numchannels * samplelength));
            wr.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt "));
            wr.Write((uint)16);
            wr.Write((ushort)1);
            wr.Write((ushort)numchannels);
            wr.Write((uint)samplerate);
            wr.Write(samplerate * samplelength * numchannels);
            wr.Write((ushort) (samplelength * numchannels));
            wr.Write((ushort) (8 * samplelength));
            wr.Write(System.Text.Encoding.ASCII.GetBytes("data"));
            wr.Write(numsamples * samplelength);

            for (int i = 0; i < data.Length; i++)
            {
                wr.Write((short)data[i]);
            }

            wr.Flush();
            wr.Close();

            return true;
        }
    }
}
