using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


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
            //var tb = new TestBench();


            /*
            Watch those wrist rockets:
            rep_unit_vo_slow
            RI1COM142
            */

            //var c = tb.LoadAndTrackContainer(args[0], out Level level);

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

            FileReader streamFileReader = FileReader.FromFile(args[0], false);
            if (streamFileReader == null)
            {
                Console.WriteLine("Unable to create file reader from path: " + args[0]);
                return 1;
            }


            Level level = Level.FromStream(streamFileReader);

            string streamName = args[1];
            string segName = args[2];

            //SoundStream

            SoundStream str = level.FindAndIndexSoundStream(streamFileReader, HashUtils.GetFNV(streamName));
            if (str == null)
            {
                Console.WriteLine(streamName + " stream not found...");
                return 1;
            } 

            int blockSize = (int) str.NumChannels * 36;

            Console.WriteLine(String.Format("Num channels: {0},\nNum substreams: {1},\nSubstream interleave: {2},\nNum samples in {3} bytes: {4}", 
                str.NumChannels, str.NumSubstreams, str.SubstreamInterleave, str.SubstreamInterleave, str.GetNumSamplesInBytes((int)str.SubstreamInterleave)));

            /*
            Sound segSnd = str.GetSound(HashUtils.GetFNV(segName));
            if (segSnd == null)
            {
                Console.WriteLine(segName + " segment not found...");
                return 1;
            } 
            */

            if (!str.SetFileReader(streamFileReader))
            {
                Console.WriteLine("Failed to set file reader for sound stream...");
            }

            //if (!str.SetFileStreamBuffer(streamBufferHandle.AddrOfPinnedObject(), streamBuffer.Length))
            //{
            //    Console.WriteLine("Failed to set file stream buffer for sound stream...");
            //}

            /*
            if (str.NumSubstreams == 1)
            {
                str.SetSegment(HashUtils.GetFNV(segName));
                str.ReadSamples(samplesBufferHandle.AddrOfPinnedObject(), samplesBuffer.Length * 2, segSnd.NumSamples, ESoundFormat.PCM16);   
                for (int j = 0; j < samplesBuffer.Length; j+=2000)
                {
                    Console.WriteLine(samplesBuffer[j]);
                }
            }
            */

            Directory.CreateDirectory(streamName);

            int segCounter = 0;
            foreach (Sound segment in str.GetSounds())
            {
                byte[] streamBuffer = new byte[2 * 72000];
                GCHandle streamBufferHandle = GCHandle.Alloc(streamBuffer, GCHandleType.Pinned);

                if (!str.SetFileStreamBuffer(streamBufferHandle.AddrOfPinnedObject(), streamBuffer.Length))
                {
                    Console.WriteLine("Failed to set file stream buffer for sound stream...");
                }

                string segmentName = HashUtils.FNVToString(segment.Name, true);
                Console.WriteLine("Extracting segment: " + segmentName + " with frequency: " + segment.SampleRate.ToString());

                short[] samplesBuffer = new short[segment.NumSamples];
                GCHandle samplesBufferHandle = GCHandle.Alloc(samplesBuffer, GCHandleType.Pinned);

                str.SetSegment(segment.Name);

                float[] floatSamples = new float[segment.NumSamples];
                

                Console.WriteLine(String.Format("Read {0} float samples...", str.ReadSamplesUnity(floatSamples)));

                for (int i = 0; i < 20; i++)
                {
                    //Console.WriteLine(String.Format("Float sample {0}: {1}", i, floatSamples[i]));
                }

                str.SetSegment(segment.Name);


                str.ReadSamples(samplesBufferHandle.AddrOfPinnedObject(), samplesBuffer.Length * 2, segment.NumSamples, ESoundFormat.PCM16);   
                
                for (int j = 0; j < 20; j++)
                {
                    //Console.WriteLine(String.Format("Short sample {0}: {1}", j, samplesBuffer[j]));
                }


                if (!SoundUtils.WriteToWAV(streamName + "/" + segmentName + ".wav", segment, samplesBuffer))
                {
                    Console.WriteLine("Failed to write wav...");
                }

                samplesBufferHandle.Free();
                streamBufferHandle.Free();


                //if (segCounter++ > 10) break;
            }

            
            return 1;            
        }
    }
}
