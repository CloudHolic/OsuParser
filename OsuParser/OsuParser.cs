using System.IO;
using OsuParser.Exceptions;
using OsuParser.Parsers;
using OsuParser.Structures;

namespace OsuParser
{
    public static class OsuParser
    {
        public static Beatmap CreateBeatmap()
        {
            return new Beatmap();
        }

        public static Beatmap CopyBeatmap(Beatmap prevBeatmap)
        {
            return new Beatmap(prevBeatmap);
        }

        public static Beatmap LoadOsuFile(string filename)
        {
            return new Beatmap(filename);
        }

        public static void SaveBeatmap(string filename, Beatmap beatmap)
        {
            using (var writer = new StreamWriter(filename))
            {
                // osu file version
                writer.WriteLine("osu file format v14");
                writer.WriteLine();

                // General section
                GeneralParser.Writer(writer, beatmap.Gen);
                writer.WriteLine();

                // Editor section
                EditorParser.Writer(writer, beatmap.Edit);
                writer.WriteLine();

                // Metadata section
                MetadataParser.Writer(writer, beatmap.Meta);
                writer.WriteLine();

                // Difficulty section
                DifficultyParser.Writer(writer, beatmap.Diff);
                writer.WriteLine();

                // Events section
                EventParser.Writer(writer, beatmap.Events);
                writer.WriteLine();

                // TimingPoints setcion
                TimingPointsParser.Writer(writer, beatmap.Timing);
                writer.WriteLine();

                // Colours section
                ColourParser.Writer(writer, beatmap.Color);
                writer.WriteLine();

                // HitObjects section
                HitObjectParser.Writer(writer, beatmap.HitObjects);
                writer.WriteLine();
            }
        }
    }
}