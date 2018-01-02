using System;
using System.IO;
using System.Linq;

using OsuParser.Structures;

namespace OsuParser.Parsers
{
    public static class MetadataParser
    {
        public static Metadata Parse(string filename)
        {
            var data = new Metadata();

            using (var reader = new StreamReader(filename))
            {
                string currentLine;

                while ((currentLine = reader.ReadLine()) != null)
                {
                    //  Title
                    if (currentLine.StartsWith("Title:"))
                        data.Title = currentLine.Substring(currentLine.IndexOf(':') + 1);

                    //  Unicode Title
                    if (currentLine.StartsWith("TitleUnicode:"))
                        data.TitleUnicode = currentLine.Substring(currentLine.IndexOf(':') + 1);

                    //  Artist
                    if (currentLine.StartsWith("Artist:"))
                        data.Artist = currentLine.Substring(currentLine.IndexOf(':') + 1);

                    //  Unicode Artist
                    if (currentLine.StartsWith("ArtistUnicode:"))
                        data.ArtistUnicode = currentLine.Substring(currentLine.IndexOf(':') + 1);

                    //  Creator
                    if (currentLine.StartsWith("Creator:"))
                        data.Creator = currentLine.Substring(currentLine.IndexOf(':') + 1);

                    //  Difficulty
                    if (currentLine.StartsWith("Version:"))
                        data.Version = currentLine.Substring(currentLine.IndexOf(':') + 1);

                    //  Source
                    if (currentLine.StartsWith("Source:"))
                        data.Source = currentLine.Substring(currentLine.IndexOf(':') + 1);

                    //  Tags
                    if (currentLine.StartsWith("Tags:"))
                        data.Tags = currentLine.Substring(currentLine.IndexOf(':') + 1).Split(' ').Select(cur => cur).ToList();

                    //  Beatmap ID
                    if (currentLine.StartsWith("BeatmapID:"))
                        data.BeatmapId = Convert.ToInt32(currentLine.Split(':')[1]);

                    //  Beatmapset ID
                    if (currentLine.StartsWith("BeatmapSetID:"))
                    {
                        data.BeatmapSetId = Convert.ToInt32(currentLine.Split(':')[1]);
                        break;
                    }
                }
            }

            return data;
        }

        internal static void Writer(StreamWriter writer, Metadata meta)
        {
            // Section Header
            writer.WriteLine("[Metadata]");

            // Title
            writer.WriteLine("Title:{0}", meta.Title);

            // Unicode Title
            writer.WriteLine("TitleUnicode:{0}", meta.TitleUnicode);

            // Artist
            writer.WriteLine("Artist:{0}", meta.Artist);

            // Unicode Artist
            writer.WriteLine("ArtistUnicode:{0}", meta.ArtistUnicode);

            // Creator
            writer.WriteLine("Creator:{0}", meta.Creator);

            // Version
            writer.WriteLine("Version:{0}", meta.Version);

            // Source
            writer.WriteLine("Source:{0}", meta.Source);

            // Tags
            writer.WriteLine("Tags:{0}", string.Join(" ", meta.Tags));

            // Beatmap ID
            writer.WriteLine("BeatmapID:{0}", meta.BeatmapId);

            // Beatmapset ID
            writer.WriteLine("BeatmapSetID:{0}", meta.BeatmapSetId);
        }
    }
}