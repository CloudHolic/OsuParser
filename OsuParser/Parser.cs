using System.IO;
using System.Text;
using OsuParser.Parsers;
using OsuParser.Structures;
using OsuParser.Structures.Events;

namespace OsuParser
{
    public static class Parser
    {
        public static Beatmap CreateBeatmap()
        {
            return new Beatmap();
        }

        public static Storyboard CreateStoryboard()
        {
            return new Storyboard();
        }

        public static Beatmap CopyBeatmap(Beatmap prevBeatmap)
        {
            return new Beatmap(prevBeatmap);
        }

        public static Storyboard CopyStoryboard(Storyboard prevStoryboard)
        {
            return new Storyboard(prevStoryboard);
        }

        public static Beatmap LoadOsuFile(string filename)
        {
            return new Beatmap(filename);
        }

        public static Storyboard LoadOsbFile(string filename)
        {
            return new Storyboard(filename);
        }

        public static string PrintOsuFile(Beatmap beatmap)
        {
            var encoding = new UTF8Encoding(false);
            var mem = new MemoryStream();
            var stream = new StreamWriter(mem, encoding);
            SaveOsuFile(stream, beatmap);

            return encoding.GetString(mem.ToArray(), 0, (int)mem.Length);
        }

        public static void SaveOsuFile(string filename, Beatmap beatmap)
        {
            using (var writer = new StreamWriter(filename, false, new UTF8Encoding(false)))
            {
                SaveOsuFile(writer, beatmap);
            }
        }

        public static void SaveOsuFile(StreamWriter writer, Beatmap beatmap)
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
            ColorParser.Writer(writer, beatmap.Color);
            writer.WriteLine();

            // HitObjects section
            HitObjectParser.Writer(writer, beatmap.HitObjects);
            writer.WriteLine();
        }

        public static string PrintOsbFile(Storyboard storyboard)
        {
            var encoding = new UTF8Encoding(false);
            var mem = new MemoryStream();
            var stream = new StreamWriter(mem, encoding);
            SaveOsbFile(stream, storyboard);

            return encoding.GetString(mem.ToArray(), 0, (int)mem.Length);
        }

        public static void SaveOsbFile(string filename, Storyboard storyboard)
        {
            using (var writer = new StreamWriter(filename, false, new UTF8Encoding(false)))
            {
                SaveOsbFile(writer, storyboard);
            }
        }

        public static void SaveOsbFile(StreamWriter writer, Storyboard storyboard)
        {
            // Event section only in .osb file.
            EventParser.Writer(writer, storyboard);
            writer.WriteLine();
        }
    }
}