using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using LibSWBF2.Logging;
using LibSWBF2.Wrappers;
using LibSWBF2.Types;
using LibSWBF2.Utils;

namespace LibSWBF2.NET.Test
{
    class WorldInstancesRegionsAnimsTest
    {
        static int Main(string[] args)
        {
            TestBench testBench = new TestBench();

            Container container = testBench.LoadAndTrackContainer(new List<string>(args), out List<Level> lvls);

            Level level = lvls[0];
            if (level == null)
            {
                return -1;
            }
        
            World[] worlds = level.Get<World>();

            foreach (World world in worlds)
            {
                Console.WriteLine("\n" + world.Name);

                Console.WriteLine("  Barriers:");
                Barrier[] barriers = world.GetBarriers();
                foreach (Barrier barrier in barriers)
                {
                    Console.WriteLine("\n\tBarrier: " + barrier.Name);
                
                    Console.WriteLine("\t  Size: " + barrier.Size.ToString());
                    Console.WriteLine("\t  Position: " + barrier.Position.ToString());
                    Console.WriteLine("\t  Rotation: " + barrier.Rotation.ToString());
                    Console.WriteLine("\t  Flag: " + barrier.Flag);
                }

                /*
                Console.WriteLine("  HintNodes:");
                HintNode[] hintNodes = world.GetHintNodes();
                foreach (HintNode hintNode in hintNodes)
                {
                    Console.WriteLine("\n\tHintNode: " + hintNode.Name);
                    Console.WriteLine("\t  Position: " + hintNode.Position.ToString());
                    Console.WriteLine("\t  Rotation: " + hintNode.Rotation.ToString());
                    Console.WriteLine("\t  Type: " + hintNode.Type);
                }
                */

                /*
                Console.WriteLine("  Instances: ");
                foreach (Instance instance in world.GetInstances())
                {
                    var ec = container.FindWrapper<EntityClass>(instance.entityClassName);

                    if (ec == null)
                    {
                        continue;
                    }

                    string baseName = ec.GetBaseName();

                    
                    string instName = instance.name;
                    Vector4 rot = instance.rotation;
                    Vector3 pos = instance.position;

                    Console.WriteLine("\n\tInstance: " + instName);
                    
                    Console.WriteLine("\t  Class: " + ec.name);
                    Console.WriteLine("\t  Parent: " + baseName);
                    Console.WriteLine("\t  Rotation: " + rot.ToString());
                    Console.WriteLine("\t  Position: " + pos.ToString());

                    Console.WriteLine("\t  Overridden properties: ");
                    if (instance.GetOverriddenProperties(out uint[] props, out string[] values))
                    {
                        for (int j = 0; j < props.Length; j++)
                        {
                            Console.WriteLine("\t    Hash: {0}, Value: {1}", props[j], values[j]);
                        }
                    }                  
                }
                */

                /*
                Console.WriteLine("  Regions:");
                foreach (Region region in world.GetRegions())
                {
                    Console.WriteLine("\n\tRegion: " + region.Name);
                
                    Console.WriteLine("\t  Size: " + region.Size.ToString());
                    Console.WriteLine("\t  Rotation: " + region.Rotation.ToString());
                    Console.WriteLine("\t  Type: " + region.Type);

                    region.GetProperties(out uint[] properties, out string[] values);

                    Console.WriteLine("\t  " + properties.Length.ToString() + " properties:");

                    for (int i = 0; i < properties.Length; i++)
                    {
                        Console.WriteLine(String.Format("\t\t{0}: {1}", HashUtils.FNVToString(properties[i],true), values[i]));
                    }
                }
                */
                 

                /*
                Console.WriteLine("  Animations:");
                foreach (WorldAnimation anim in world.GetAnimations())
                {
                    Console.WriteLine(String.Format("\n\t{0}:", anim.ToString()));
                    Console.WriteLine("\t  Rotation keys: ");
                    foreach (WorldAnimationKey rk in anim.GetRotationKeys())
                    {
                        Console.WriteLine("\t    " + rk.ToString());
                    }

                    Console.WriteLine("\t  Position keys: ");
                    foreach (WorldAnimationKey pk in anim.GetPositionKeys())
                    {
                        Console.WriteLine("\t    " + pk.ToString());
                    }
                }

                Console.WriteLine("\n  Animation Groups:");
                foreach (WorldAnimationGroup animGroup in world.GetAnimationGroups())
                {
                    Console.WriteLine(String.Format("\n\t{0}:", animGroup.ToString()));
                    Console.WriteLine("\t  Anim-Instance pairs: ");
                    foreach (Tuple<string,string> pair in animGroup.GetAnimationInstancePairs())
                    {
                        Console.WriteLine($"\t    ({pair.Item1}, {pair.Item2})");
                    }
                }

                Console.WriteLine("\n  Animation Hierarchies:");
                foreach (WorldAnimationHierarchy animHier in world.GetAnimationHierarchies())
                {
                    Console.WriteLine(String.Format("\n\t{0}:", animHier.ToString()));
                    Console.WriteLine("\t  Children: ");
                    foreach (string child in animHier.ChildrenNames)
                    {
                        Console.WriteLine($"\t    {child}");
                    }
                }
                */
            }

            return 1;
        }
    }
}
