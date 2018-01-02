using System;
using System.Collections.Generic;
using System.IO;
using OsuParser.Structures;
using OsuParser.Structures.Events;

namespace OsuParser.Parsers
{
    public static class EventParser
    {
        public static SbEvent Parse(string filename)
        {
            var data = new SbEvent();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    // Find Events Section.
                    if (currentLine == "[Events]")
                    {
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            if (currentLine == "[TimingPoints]")
                                break;

                            // 1st comment.
                            if (currentLine == "//Background and Video events")
                            {
                                while ((currentLine = reader.ReadLine()) != null)
                                {
                                    if (currentLine.StartsWith("//"))
                                        break;
                                    data.Background = currentLine.Split(',')[2].Replace("\"", string.Empty);
                                }
                            }

                            // 2nd comment.
                            if (currentLine == "//Break Periods")
                            {
                                while ((currentLine = reader.ReadLine()) != null)
                                {
                                    if (currentLine.StartsWith("//"))
                                        break;
                                    var brPeriod = currentLine.Split(',');
                                    data.Breaks.Add(Tuple.Create(Convert.ToInt32(brPeriod[1]), Convert.ToInt32(brPeriod[2])));
                                }
                            }

                            // 3rd ~ 6th comment.
                            if (currentLine == "//Storyboard Layer 0 (Background)")
                            {
                                while ((currentLine = reader.ReadLine()) != null)
                                {
                                    if (currentLine == "//Storyboard Sound Samples")
                                        break;
                                    if (currentLine.StartsWith("//"))
                                        continue;


                                }
                            }
                            
                            // Last comment.
                            if (currentLine == "//Storyboard Sound Samples")
                            {
                                while ((currentLine = reader.ReadLine()) != null)
                                {
                                    if (currentLine.Length == 0)
                                        break;

                                    var temp = currentLine.Split(',');
                                    var sampleSound = new SbSound
                                    {
                                        Time = Convert.ToInt32(temp[1]),
                                        Layer = (SbLayer)Convert.ToInt32(temp[2]),
                                        FileName = temp[3].Replace("\"", string.Empty),
                                        Volume = Convert.ToInt32(temp[4])
                                    };
                                    data.SampleSounds.Add(sampleSound);
                                }
                            }
                        }
                    }
                }
            }

            return data;
        }

        internal static void Writer(StreamWriter writer, SbEvent events)
        {
            // Section Header
            writer.WriteLine("[Events]");

            // Background and Video Events
            writer.WriteLine("//Background and Video events");
            writer.WriteLine($"0,0,\"{events.Background}\",0,0");

            // Break Periods
            writer.WriteLine("//Break Periods");
            foreach(var cur in events.Breaks)
                writer.WriteLine(SbEvent.BreaksToString(cur));

            // Storyborad Layer 0
            writer.WriteLine("//Storyboard Layer 0 (Background)");

            // Storyboard Layer 1
            writer.WriteLine("//Storyboard Layer 1 (Fail)");

            // Storyboard Layer 2
            writer.WriteLine("//Storyboard Layer 2 (Pass)");

            // Storyboard Layer 3
            writer.WriteLine("//Storyboard Layer 3 (Foreground)");

            // Storyboard Sound Sample
            writer.WriteLine("//Storyboard Sound Samples");
        }
    }
}