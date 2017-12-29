using OsuParser.Exceptions;
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
            
        }
    }
}