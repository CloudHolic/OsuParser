using System.Collections.Generic;
using System.IO;
using OsuParser.Exceptions;
using OsuParser.Parsers;
using OsuParser.Structures.HitObjects;

namespace OsuParser.Structures
{
    //  Represents a beatmap. It contains all information about a single osu-file.
    public class Beatmap
    {
        public General Gen { get; set; }
        public Editor Edit { get; set; }
        public Metadata Meta { get; set; }
        public Difficulty Diff { get; set; }
        public List<TimingPoint> Timing { get; set; }
        public List<Colours> Color { get; set; }
        public List<HitObject> HitObjects { get; set; }

        internal Beatmap()
        {
            Gen = new General();
            Edit = new Editor();
            Meta = new Metadata();
            Diff = new Difficulty();
            Timing = new List<TimingPoint>();
            Color = new List<Colours>();
            HitObjects = new List<HitObject>();

            Timing.Add(new TimingPoint());
            Color.Add(new Colours());
        }

        internal Beatmap(string filename)
        {
            //  Load, and parse.
            if (File.Exists(filename))
            {
                if (filename.Split('.')[filename.Split('.').Length - 1] != "osu")
                    throw new InvalidBeatmapException("Unknown file format.");

                Gen = GeneralParser.Parse(filename);
                Edit = EditorParser.Parse(filename);
                Meta = MetadataParser.Parse(filename);
                Diff = DifficultyParser.Parse(filename);
                Timing = TimingPointsParser.Parse(filename);
                Color = ColourParser.Parse(filename);
                HitObjects = HitObjectParser.Parse(filename);
            }
            else
                throw new FileNotFoundException();
        }

        internal Beatmap(Beatmap prevBeatmapInfo)
        {
            Gen = new General(prevBeatmapInfo.Gen);
            Edit = new Editor(prevBeatmapInfo.Edit);
            Meta = new Metadata(prevBeatmapInfo.Meta);
            Diff = new Difficulty(prevBeatmapInfo.Diff);

            Timing = new List<TimingPoint>();
            foreach (var cur in prevBeatmapInfo.Timing)
                Timing.Add(new TimingPoint(cur));

            Color = new List<Colours>();
            foreach (var cur in prevBeatmapInfo.Color)
                Color.Add(new Colours(cur));

            HitObjects = new List<HitObject>();
            foreach (var cur in prevBeatmapInfo.HitObjects)
            {
                if((cur.Type & 1) > 0)
                    HitObjects.Add(new Circle(cur as Circle));
                else if((cur.Type & 2) > 0)
                    HitObjects.Add(new Slider(cur as Slider));
                else if((cur.Type & 8) > 0)
                    HitObjects.Add(new Spinner(cur as Spinner));
                else if((cur.Type & 128) > 0)
                    HitObjects.Add(new LongNote(cur as LongNote));
                else
                    throw new InvalidBeatmapException("Unknown HitObject Type");
            }
        }
    }
}