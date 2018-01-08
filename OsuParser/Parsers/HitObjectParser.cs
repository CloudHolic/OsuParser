using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OsuParser.Exceptions;
using OsuParser.Structures.HitObjects;

namespace OsuParser.Parsers
{
    internal static class HitObjectParser
    {
        internal static List<HitObject> Parse(string filename)
        {
            var rawObjects = RawParse(filename);
            var hitObjects = new List<HitObject>();

            foreach (var cur in rawObjects)
            {
                if ((cur.Type & 1) > 0)
                    hitObjects.Add(new Circle(cur.X, cur.Y, cur.Time, cur.Type, cur.Hitsound, cur.Extras));
                else if ((cur.Type & 2) > 0)
                    hitObjects.Add(new Slider(cur.X, cur.Y, cur.Time, cur.Type, cur.Hitsound, cur.SliderType,
                        cur.CurvePoints, cur.Repeat, cur.PixelLength, cur.EdgeHitsounds, cur.EdgeAdditions,
                        cur.Extras));
                else if ((cur.Type & 8) > 0)
                    hitObjects.Add(new Spinner(cur.X, cur.Y, cur.Time, cur.Type, cur.Hitsound, cur.EndTime, cur.Extras));
                else if ((cur.Type & 128) > 0)
                    hitObjects.Add(new LongNote(cur.X, cur.Y, cur.Time, cur.Type, cur.Hitsound, cur.EndTime, cur.Extras));
                else
                    throw new InvalidBeatmapException("Unknown HitObject Type");
            }

            return hitObjects;
        }

        private static List<RawHitObject> RawParse(string filename)
        {
            var data = new List<RawHitObject>();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    //Finding HitObject section
                    if (currentLine == "[HitObjects]")
                    {
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            if (currentLine.Length == 0)
                                break;

                            var parsed = currentLine.Split(',');
                            if (!new[] { 6, 7, 11 }.Contains(parsed.Length))
                                throw new InvalidBeatmapException("Wrong HitObject Found.");

                            var temp = new RawHitObject
                            {
                                // X
                                X = Convert.ToInt32(parsed[0]),

                                // Y
                                Y = Convert.ToInt32(parsed[1]),

                                // Time
                                Time = Convert.ToInt32(parsed[2]),

                                // Type
                                Type = Convert.ToInt32(parsed[3]),

                                // Hitsound
                                Hitsound = Convert.ToInt32(parsed[4]),
                            };

                            if (parsed.Length == 11)
                            {
                                // SliderType
                                temp.SliderType = parsed[5].Split('|')[0].ToCharArray()[0];

                                // CurvePoints
                                foreach (var cur in parsed[5].Substring(2).Split('|'))
                                    temp.CurvePoints.Add(Tuple.Create(Convert.ToInt32(cur.Split(':')[0]), Convert.ToInt32(cur.Split(':')[1])));

                                // Repeat
                                temp.Repeat = Convert.ToInt32(parsed[6]);

                                // PixelLength
                                temp.PixelLength = Convert.ToInt32(parsed[7]);

                                // EdgeHitSounds
                                foreach (var cur in parsed[8].Split('|'))
                                    temp.EdgeHitsounds.Add(Convert.ToInt32(cur));

                                // EdgeAdditions
                                foreach (var cur in parsed[9].Split('|'))
                                    temp.EdgeAdditions.Add(Tuple.Create(Convert.ToInt32(cur.Split(':')[0]), Convert.ToInt32(cur.Split(':')[1])));
                            }

                            // If it's a circle or slider or spinner,
                            if (parsed.Length != 6 || (parsed.Length == 6 && parsed[5].Split(new[] { ':' }, StringSplitOptions.None).Length == 5))
                            {
                                // EndTime
                                if (parsed.Length == 7)
                                    temp.EndTime = Convert.ToInt32(parsed[5]);

                                // Addition
                                var additions = parsed.Last().Split(new[] { ':' }, StringSplitOptions.None);
                                temp.Extras = Tuple.Create(Convert.ToInt32(additions[0]), Convert.ToInt32(additions[1]), Convert.ToInt32(additions[2]), Convert.ToInt32(additions[3]), additions[4]);
                            }
                            // If it's a Mania LN,
                            else
                            {
                                var last = parsed.Last().Split(new[] { ':' }, StringSplitOptions.None);

                                // EndTime
                                temp.EndTime = Convert.ToInt32(last[0]);

                                // Addition
                                temp.Extras = Tuple.Create(Convert.ToInt32(last[1]), Convert.ToInt32(last[2]), Convert.ToInt32(last[3]), Convert.ToInt32(last[4]), last[5]);
                            }

                            data.Add(temp);
                        }
                    }
                }
            }

            return data;
        }

        internal static void Writer(StreamWriter writer, List<HitObject> hits)
        {
            // Section Header
            writer.WriteLine("[HitObjects]");

            // Each Hit Objects
            foreach(var hit in hits)
                writer.WriteLine(hit.ToString());
        }
    }
}
