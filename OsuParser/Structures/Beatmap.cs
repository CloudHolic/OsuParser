using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using OsuParser.Exceptions;
using OsuParser.Parsers;
using OsuParser.Structures.Events;
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
        public Storyboard Events { get; set; }
        public List<TimingPoint> Timing { get; set; }
        public List<Colors> Color { get; set; }
        public List<HitObject> HitObjects { get; set; }

        internal Beatmap()
        {
            Gen = new General();
            Edit = new Editor();
            Meta = new Metadata();
            Diff = new Difficulty();
            Events = new Storyboard();
            Timing = new List<TimingPoint>();
            Color = new List<Colors>();
            HitObjects = new List<HitObject>();

            Timing.Add(new TimingPoint());
            Color.Add(new Colors());
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
                Events = EventParser.Parse(filename);
                Timing = TimingPointsParser.Parse(filename);
                Color = ColorParser.Parse(filename);
                HitObjects = HitObjectParser.Parse(filename);
            }
            else
                throw new FileNotFoundException();
        }

        internal Beatmap(General gen, Editor edit, Metadata meta, Difficulty diff, Storyboard events,
            List<TimingPoint> timings, List<Colors> colors, List<HitObject> hitObjects)
        {
            Gen = gen;
            Edit = edit;
            Meta = meta;
            Diff = diff;
            Events = events;
            Timing = new List<TimingPoint>(timings);
            Color = new List<Colors>(colors);
            HitObjects = new List<HitObject>(hitObjects);
        }

        internal Beatmap(Beatmap prevBeatmapInfo)
        {
            Gen = new General(prevBeatmapInfo.Gen);
            Edit = new Editor(prevBeatmapInfo.Edit);
            Meta = new Metadata(prevBeatmapInfo.Meta);
            Diff = new Difficulty(prevBeatmapInfo.Diff);
            Events = new Storyboard(prevBeatmapInfo.Events);

            Timing = new List<TimingPoint>();
            foreach (var cur in prevBeatmapInfo.Timing)
                Timing.Add(new TimingPoint(cur));

            Color = new List<Colors>();
            foreach (var cur in prevBeatmapInfo.Color)
                Color.Add(new Colors(cur));

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