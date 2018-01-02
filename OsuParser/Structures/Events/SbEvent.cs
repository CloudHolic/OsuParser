using System;
using System.Collections.Generic;

namespace OsuParser.Structures.Events
{
    public class SbEvent
    {
        public SbEvent()
        {
            Background = string.Empty;
            Breaks = new List<Tuple<int, int>>();
            SbObjects = new List<SbObjects>();
        }

        public SbEvent(string background, IEnumerable<Tuple<int, int>> breaks, IEnumerable<SbObjects> sbObjects)
        {
            Background = background;
            Breaks = new List<Tuple<int, int>>(breaks);
            SbObjects = new List<SbObjects>(sbObjects);
        }

        public SbEvent(SbEvent prevEvent)
        {
            Background = prevEvent.Background;
            Breaks = new List<Tuple<int, int>>(prevEvent.Breaks);
            SbObjects = new List<SbObjects>(prevEvent.SbObjects);
        }

        internal static string BreaksToString(Tuple<int, int> breakPoint)
        {
            return $"2,{breakPoint.Item1},{breakPoint.Item2}";
        }

        public string Background { get; set; }

        public List<Tuple<int, int>> Breaks { get; set; }

        public List<SbObjects> SbObjects { get; set; }
    }
}