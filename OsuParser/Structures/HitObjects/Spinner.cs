using System;

// Same with longnote, except 'Type' value
namespace OsuParser.Structures.HitObjects
{
    class Spinner : HitObject
    {
        public Spinner()
        {
            Type = 8;
            EndTime = 0;
        }

        public Spinner(int x, int y, int time, int type, int hitsound, int endTIme, Tuple<int, int, int, int, string> addition)
            : base(x, y, time, hitsound, addition)
        {
            Type = type;
            EndTime = endTIme;
        }

        public Spinner(Spinner prevSpinner) : base(prevSpinner)
        {
            Type = prevSpinner.Type;
            EndTime = prevSpinner.EndTime;
        }

        public int EndTime { get; set; }
    }
}
