using System;
using System.IO;
using System.Linq;

using OsuParser.Structures;

namespace OsuParser.Parsers
{
    public static class EditorParser
    {
        public static Editor Parse(string filename)
        {
            var data = new Editor();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    //  Bookmarks
                    if (currentLine.StartsWith("Bookmarks:"))
                        data.Bookmarks = currentLine.Substring(currentLine.IndexOf(':') + 1).Split(',')
                            .Select(cur => Convert.ToInt32(cur)).ToList();

                    //  Distance Spacing
                    if (currentLine.StartsWith("DistanceSpacing:"))
                        data.DistanceSpacing = Convert.ToDouble(currentLine.Split(':')[1]);

                    //  Beat Divisor
                    if (currentLine.StartsWith("BeatDivisor:"))
                        data.BeatDivisor = Convert.ToInt32(currentLine.Split(':')[1]);

                    //  Grid Size
                    if (currentLine.StartsWith("GridSize:"))
                        data.GridSize = Convert.ToInt32(currentLine.Split(':')[1]);

                    //  Timeline Zoom
                    if (currentLine.StartsWith("TimelineZoom:"))
                    {
                        data.TimelineZoom = Convert.ToDouble(currentLine.Split(':')[1]);
                        break;
                    }
                }
            }

            return data;
        }

        internal static void Writer(StreamWriter writer, Editor edit)
        {
            // Section Header
            writer.WriteLine("[Editor]");

            // Bookmarks
            writer.WriteLine("Bookmarks: {0}", string.Join(",", edit.Bookmarks));

            // Distance Spacing
            writer.WriteLine("DistanceSpacing: {0}", edit.DistanceSpacing);

            // Beat Divisor
            writer.WriteLine("BeatDivisor: {0}", edit.BeatDivisor);

            // Grid Size
            writer.WriteLine("GridSize: {0}", edit.GridSize);

            // Timeline Zoom
            writer.WriteLine("TimelineZoom: {0}", edit.TimelineZoom);
        }
    }
}