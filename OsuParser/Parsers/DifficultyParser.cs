using System;
using System.IO;

using OsuParser.Structures;

namespace OsuParser.Parsers
{
    public static class DifficultyParser
    {
        public static Difficulty Parse(string filename)
        {
            var data = new Difficulty();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    //  HP
                    if (currentLine.StartsWith("HPDrainRate:"))
                        data.HPDrainRate = Convert.ToDouble(currentLine.Split(':')[1]);

                    //  CS
                    if (currentLine.StartsWith("CircleSize:"))
                        data.CircleSize = Convert.ToDouble(currentLine.Split(':')[1]);

                    //  OD
                    if (currentLine.StartsWith("OverallDifficulty:"))
                        data.OverallDifficulty = Convert.ToDouble(currentLine.Split(':')[1]);

                    //  AR
                    if (currentLine.StartsWith("ApproachRate:"))
                        data.ApproachRate = Convert.ToDouble(currentLine.Split(':')[1]);

                    //  Slider Multiplier
                    if (currentLine.StartsWith("SliderMultiplier:"))
                        data.SliderMultiplier = Convert.ToDouble(currentLine.Split(':')[1]);

                    //  Slider Tick Rate
                    if (currentLine.StartsWith("SliderTickRate:"))
                    {
                        data.SliderTickRate = Convert.ToDouble(currentLine.Split(':')[1]);
                        break;
                    }
                }
            }

            return data;
        }

        internal static void Writer(StreamWriter writer, Difficulty diff)
        {
            // Section Header
            writer.WriteLine("[Difficulty]");

            // HP
            writer.WriteLine("HPDrainRate:{0}", diff.HPDrainRate);

            // CS
            writer.WriteLine("CircleSize:{0}", diff.CircleSize);

            // OD
            writer.WriteLine("OverallDifficulty:{0}", diff.OverallDifficulty);

            // AR
            writer.WriteLine("ApproachRate:{0}", diff.ApproachRate);

            // Slider Multiplier
            writer.WriteLine("SliderMultiplier:{0}", diff.SliderMultiplier);

            // Slider Tick Rate
            writer.WriteLine("SliderTickRate:{0}", diff.SliderTickRate);
        }
    }
}