using System;

// Same with spinner, except 'Type' value
namespace OsuParser.Structures.HitObjects
{
    public class LongNote : HitObject
    {
        public LongNote()
        {
            Type = 128;
            EndTime = 0;
        }

        public LongNote(int x, int y, int time, int type, int hitsound, int endTIme, Tuple<int, int, int, int, string> extras)
            : base(x, y, time, hitsound, extras)
        {
            Type = type;
            EndTime = endTIme;
        }

        public LongNote(LongNote prevLongNote) : base(prevLongNote)
        {
            Type = prevLongNote.Type;
            EndTime = prevLongNote.EndTime;
        }

        public int EndTime { get; set; }

        public override string ToString()
        {
            return $"{X},{Y},{Time},{Type},{HitSound},{EndTime}:{ExtraToString()}";
        }
    }
}
