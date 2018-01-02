using System.IO;
using OsuParser.Structures;
using OsuParser.Structures.Events;

namespace OsuParser.Parsers
{
    public static class EventParser
    {
        public static SbEvent Parse(string filename)
        {
            return new SbEvent();
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