using System;
using System.IO;

using OsuParser.Structures;

namespace OsuParser.Parsers
{
    public static class GeneralParser
    {
        public static General Parse(string filename)
        {
            var data = new General();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    //  Audio File Name
                    if (currentLine.StartsWith("AudioFilename:"))
                        data.AudioFilename = currentLine.Substring(currentLine.IndexOf(' ') + 1);

                    //  Audio Leadin
                    if (currentLine.StartsWith("AudioLeadIn:"))
                        data.AudioLeadIn = Convert.ToInt32(currentLine.Split(':')[1]);

                    //  Preview Time
                    if (currentLine.StartsWith("PreviewTime:"))
                        data.PreviewTime = Convert.ToInt32(currentLine.Split(':')[1]);

                    //  Countdown
                    if (currentLine.StartsWith("Countdown:"))
                        data.Countdown = Convert.ToInt32(currentLine.Split(':')[1]) == 1;

                    //  Sample Set
                    if (currentLine.StartsWith("SampleSet:"))
                        data.SampleSet = currentLine.Substring(currentLine.IndexOf(' ') + 1);

                    //  Stack Leniency
                    if (currentLine.StartsWith("StackLeniency:"))
                        data.StackLeniency = Convert.ToDouble(currentLine.Split(':')[1]);

                    //  Mode
                    if (currentLine.StartsWith("Mode:"))
                        data.Mode = Convert.ToInt32(currentLine.Split(':')[1]);

                    //  Letterbox In Breaks
                    if (currentLine.StartsWith("LetterboxInBreaks:"))
                        data.LetterboxInBreaks = Convert.ToInt32(currentLine.Split(':')[1]) == 1;

                    //  Special Style
                    if (currentLine.StartsWith("SpecialStyle:"))
                        data.SpecialStyle = Convert.ToInt32(currentLine.Split(':')[1]) == 1;

                    //  Widescreen Storyboard
                    if (currentLine.StartsWith("WidescreenStoryboard:"))
                    {
                        data.WidescreenStoryboard = Convert.ToInt32(currentLine.Split(':')[1]) == 1;
                        break;
                    }
                }
            }

            return data;
        }

        internal static void Writer(StreamWriter writer, General gen)
        {
            // Section Header
            writer.WriteLine("[General]");

            // Audio File Name
            writer.WriteLine("AudioFilename: {0}", gen.AudioFilename);

            // Audio Leadin
            writer.WriteLine("AudioLeadIn: {0}", gen.AudioLeadIn);

            // Preview Time
            writer.WriteLine("PreviewTime: {0}", gen.PreviewTime);

            // Countdown
            writer.WriteLine("Countdown: {0}", gen.Countdown ? 1 : 0);

            // Sample Set
            writer.WriteLine("SampleSet: {0}", gen.SampleSet);

            // Stack Leniency
            writer.WriteLine("StackLeniency: {0}", gen.StackLeniency);

            // Mode
            writer.WriteLine("Mode: {0}", gen.Mode);

            // Letterbox In Breaks
            writer.WriteLine("LetterboxInBreaks: {0}", gen.LetterboxInBreaks ? 1 : 0);

            // Special Style
            writer.WriteLine("SpecialStyle: {0}", gen.SpecialStyle ? 1 : 0);

            // Widescreen Storyboard
            writer.WriteLine("WidescreenStoryboard: {0}", gen.WidescreenStoryboard ? 1 : 0);
        }
    }
}