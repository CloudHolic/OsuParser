using System;
using OsuParser.Exceptions;

namespace OsuParser.Structures.HitObjects
{
    public abstract class HitObject
    {
        protected HitObject()
        {
            X = 0;
            Y = 0;
            Time = 0;
            HitSound = 0;
            Extras = Tuple.Create(0, 0, 0, 0, string.Empty);
        }

        protected HitObject(int x, int y, int time, int hitsound, Tuple<int, int, int, int, string> extras)
        {
            X = x;
            Y = y;
            Time = time;
            HitSound = hitsound;
            Extras = extras;
        }

        protected HitObject(HitObject prevHitObject)
        {
            X = prevHitObject.X;
            Y = prevHitObject.Y;
            Time = prevHitObject.Time;
            HitSound = prevHitObject.HitSound;
            Extras = prevHitObject.Extras;
        }

        protected string ExtraToString()
        {
            return $"{Extras.Item1}:{Extras.Item2}:{Extras.Item3}:{Extras.Item4}:{Extras.Item5}";
        }

        public override string ToString()
        {
            if ((Type & 1) > 0)
                return (this as Circle)?.ToString() ?? throw new InvalidOperationException();
            if ((Type & 2) > 0)
                return (this as Slider)?.ToString() ?? throw new InvalidOperationException();
            if ((Type & 8) > 0)
                return (this as Spinner)?.ToString() ?? throw new InvalidOperationException();
            if ((Type & 128) > 0)
                return (this as LongNote)?.ToString() ?? throw new InvalidOperationException();
            throw new InvalidBeatmapException("Unknown HitObject Type");
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Time { get; set; }

        internal int Type { get; set; }

        public int HitSound { get; set; }

        public Tuple<int, int, int, int, string> Extras { get; set; }
    }
}
