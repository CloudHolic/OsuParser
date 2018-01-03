using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OsuParser.Structures;
using OsuParser.Structures.Events;

namespace OsuParser.Parsers
{
    public static class EventParser
    {
        public static Storyboard Parse(string filename)
        {
            var data = new Storyboard();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;
                var tempLine = string.Empty;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    // Find Events Section.
                    if (currentLine == "[Events]")
                    {
                        while (tempLine != string.Empty || (currentLine = reader.ReadLine()) != null)
                        {
                            if (tempLine != string.Empty)
                            {
                                currentLine = tempLine;
                                tempLine = string.Empty;
                            }

                            if (currentLine == "[TimingPoints]" || currentLine == string.Empty)
                            {
                                tempLine = currentLine;
                                break;
                            }

                            // 1st comment.
                            if (currentLine == "//Background and Video events")
                            {
                                while (tempLine != string.Empty || (currentLine = reader.ReadLine()) != null)
                                {
                                    if (tempLine != string.Empty)
                                    {
                                        currentLine = tempLine;
                                        tempLine = string.Empty;
                                    }

                                    if (currentLine.StartsWith("//"))
                                    {
                                        tempLine = currentLine;
                                        break;
                                    }

                                    data.Background = currentLine.Split(',')[2].Replace("\"", string.Empty);
                                }
                            }

                            // 2nd comment.
                            if (currentLine == "//Break Periods")
                            {
                                while (tempLine != string.Empty || (currentLine = reader.ReadLine()) != null)
                                {
                                    if (tempLine != string.Empty)
                                    {
                                        currentLine = tempLine;
                                        tempLine = string.Empty;
                                    }

                                    if (currentLine.StartsWith("//"))
                                    {
                                        tempLine = currentLine;
                                        break;
                                    }

                                    var brPeriod = currentLine.Split(',');
                                    data.Breaks.Add(Tuple.Create(Convert.ToInt32(brPeriod[1]), Convert.ToInt32(brPeriod[2])));
                                }
                            }

                            // 3rd ~ 6th comment.
                            if (currentLine == "//Storyboard Layer 0 (Background)")
                            {
                                while (tempLine != string.Empty || (currentLine = reader.ReadLine()) != null)
                                {
                                    if (tempLine != string.Empty)
                                    {
                                        currentLine = tempLine;
                                        tempLine = string.Empty;
                                    }

                                    if (currentLine == "//Storyboard Sound Samples")
                                    {
                                        tempLine = currentLine;
                                        break;
                                    }

                                    if (currentLine.StartsWith("//"))
                                        continue;

                                    // Find object
                                    if (currentLine.StartsWith("Sprite") || currentLine.StartsWith("Animation"))
                                    {
                                        var rawObject = currentLine.Split(',');
                                        var sbObject = new SbObject
                                        {
                                            Type = (Imagetype) Enum.Parse(typeof(Imagetype), rawObject[0]),
                                            Layer = (SbLayer) Enum.Parse(typeof(SbLayer), rawObject[1]),
                                            Origin = (SbOrigin) Enum.Parse(typeof(SbOrigin), rawObject[2]),
                                            FilePath = rawObject[3].Replace("\"", string.Empty),
                                            X = Convert.ToInt32(rawObject[4]),
                                            Y = Convert.ToInt32(rawObject[5]),
                                            FrameCount = rawObject.Length == 9 ? Convert.ToInt32(rawObject[6]) : 0,
                                            FrameDelay = rawObject.Length == 9 ? Convert.ToInt32(rawObject[7]) : 0,
                                            Looptype = rawObject.Length == 9 ? (SbLooptype) Enum.Parse(typeof(SbLooptype), rawObject[8]) : SbLooptype.LoopOnce,
                                            ActionList = new List<SbAction>()
                                        };

                                        // Find Actions
                                        while (tempLine != string.Empty || (currentLine = reader.ReadLine()) != null)
                                        {
                                            if (tempLine != string.Empty)
                                            {
                                                currentLine = tempLine;
                                                tempLine = string.Empty;
                                            }

                                            if (!currentLine.StartsWith(" "))
                                            {
                                                tempLine = currentLine;
                                                break;
                                            }

                                            SbAction sbAction;
                                            var depth = currentLine.TakeWhile(c => c == ' ').Count();
                                            var rawAction = currentLine.Trim().Split(new []{','}, StringSplitOptions.None);
                                            var action = EnumExtensions.CommandParse(rawAction[0]);

                                            if (action == Command.Loop)
                                                sbAction = new SbAction
                                                {
                                                    Action = action,
                                                    StartTime = Convert.ToInt32(rawAction[1]),
                                                    EtcParameter = rawAction[2]
                                                };
                                            else if(action == Command.Trigger)
                                                sbAction = new SbAction
                                                {
                                                    Action = action,
                                                    EtcParameter = rawAction[1],
                                                    StartTime = Convert.ToInt32(rawAction[2]),
                                                    EndTime = Convert.ToInt32(rawAction[3])
                                                };
                                            else
                                            {
                                                sbAction = new SbAction
                                                {
                                                    Action = action,
                                                    Easing = Convert.ToInt32(rawAction[1]),
                                                    StartTime = Convert.ToInt32(rawAction[2]),
                                                    EndTime = rawAction[3] == string.Empty ? null : (int?) Convert.ToInt32(rawAction[3]),
                                                    Depth = depth
                                                };

                                                switch (action)
                                                {
                                                    // 1-Parameter Commands
                                                    case Command.Fade:
                                                    case Command.MoveX:
                                                    case Command.MoveY:
                                                    case Command.Scale:
                                                    case Command.Rotate:
                                                        sbAction.StartParameter = Tuple.Create(Convert.ToDouble(rawAction[4]), 0.0, 0);
                                                        sbAction.EndParameter = Tuple.Create<double?, double?, int?>
                                                            (rawAction.Length > 5 ? (double?) Convert.ToDouble(rawAction[5]) : null, null, null);
                                                        break;

                                                    // 2-Parameters Commands
                                                    case Command.Move:
                                                    case Command.VectorScale:
                                                        sbAction.StartParameter = Tuple.Create(Convert.ToDouble(rawAction[4]), Convert.ToDouble(rawAction[5]), 0);
                                                        sbAction.EndParameter = Tuple.Create<double?, double?, int?>
                                                            (rawAction.Length > 7 ? (double?)Convert.ToDouble(rawAction[6]) : null,
                                                            rawAction.Length > 7 ? (double?)Convert.ToDouble(rawAction[7]) : null, null);
                                                        break;

                                                    // 3-Parameters Command
                                                    case Command.Color:
                                                        sbAction.StartParameter = Tuple.Create(Convert.ToDouble(rawAction[4]), Convert.ToDouble(rawAction[5]),
                                                            Convert.ToInt32(rawAction[6]));
                                                        sbAction.EndParameter = Tuple.Create(rawAction.Length > 9 ? (double?) Convert.ToDouble(rawAction[7]) : null,
                                                            rawAction.Length > 9 ? (double?) Convert.ToDouble(rawAction[8]) : null,
                                                            rawAction.Length > 9 ? (int?) Convert.ToInt32(rawAction[9]) : null);
                                                        break;

                                                    // Special-Paramter Command
                                                    case Command.Parameter:
                                                        sbAction.EtcParameter = rawAction[4];
                                                        break;
                                                }
                                            }

                                            sbObject.ActionList.Add(sbAction);
                                        }

                                        data.SbObjects.Add(sbObject);
                                    }
                                }
                            }
                            
                            // Last comment.
                            if (currentLine == "//Storyboard Sound Samples")
                            {
                                while (tempLine != string.Empty || (currentLine = reader.ReadLine()) != null)
                                {
                                    if (tempLine != string.Empty)
                                    {
                                        currentLine = tempLine;
                                        tempLine = string.Empty;
                                    }

                                    if (currentLine.Length == 0)
                                    {
                                        tempLine = currentLine;
                                        break;
                                    }

                                    if (currentLine.StartsWith("//"))
                                        continue;

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

        internal static void Writer(StreamWriter writer, Storyboard events)
        {
            // Section Header
            writer.WriteLine("[Events]");

            // Background and Video Events
            writer.WriteLine("//Background and Video events");
            if(events.Background != string.Empty)
                writer.WriteLine($"0,0,\"{events.Background}\",0,0");

            // Break Periods
            writer.WriteLine("//Break Periods");
            foreach(var cur in events.Breaks)
                writer.WriteLine(Storyboard.BreaksToString(cur));

            // Storyborad Layer 0
            writer.WriteLine("//Storyboard Layer 0 (Background)");
            foreach (var cur in events.SbObjects)
            {
                if (cur.Layer == SbLayer.Background)
                {
                    writer.WriteLine(cur);
                    foreach(var action in cur.ActionList)
                        writer.WriteLine(action.ToString());
                }
            }

            // Storyboard Layer 1
            writer.WriteLine("//Storyboard Layer 1 (Fail)");
            foreach (var cur in events.SbObjects)
            {
                if (cur.Layer == SbLayer.Fail)
                {
                    writer.WriteLine(cur);
                    foreach (var action in cur.ActionList)
                        writer.WriteLine(action.ToString());
                }
            }

            // Storyboard Layer 2
            writer.WriteLine("//Storyboard Layer 2 (Pass)");
            foreach (var cur in events.SbObjects)
            {
                if (cur.Layer == SbLayer.Pass)
                {
                    writer.WriteLine(cur);
                    foreach (var action in cur.ActionList)
                        writer.WriteLine(action.ToString());
                }
            }

            // Storyboard Layer 3
            writer.WriteLine("//Storyboard Layer 3 (Foreground)");
            foreach (var cur in events.SbObjects)
            {
                if (cur.Layer == SbLayer.Foreground)
                {
                    writer.WriteLine(cur);
                    foreach (var action in cur.ActionList)
                        writer.WriteLine(action.ToString());
                }
            }

            // Storyboard Sound Sample
            writer.WriteLine("//Storyboard Sound Samples");
            foreach(var cur in events.SampleSounds)
                writer.WriteLine(cur.ToString());
        }
    }
}