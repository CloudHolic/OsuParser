using System;
using System.Collections.Generic;

namespace OsuParser.Structures.Events
{
    public enum SbLayer
    {
        Background,
        Fail,
        Pass,
        Foreground
    }

    public class SbEvent
    {
        public SbEvent()
        {
            Background = String.Empty;
            Breaks = new List<Tuple<int, int>>();
            SbObjects = new List<SbObject>();
            SampleSounds = new List<SbSound>();
        }

        public SbEvent(string background, IEnumerable<Tuple<int, int>> breaks, IEnumerable<SbObject> sbObjects, IEnumerable<SbSound> sounds)
        {
            Background = background;
            Breaks = new List<Tuple<int, int>>(breaks);
            SbObjects = new List<SbObject>(sbObjects);
            SampleSounds = new List<SbSound>(sounds);
        }

        public SbEvent(SbEvent prevEvent)
        {
            Background = prevEvent.Background;
            Breaks = new List<Tuple<int, int>>(prevEvent.Breaks);
            SbObjects = new List<SbObject>(prevEvent.SbObjects);
            SampleSounds = new List<SbSound>(prevEvent.SampleSounds);
        }

        internal static string BreaksToString(Tuple<int, int> breakPoint)
        {
            return $"2,{breakPoint.Item1},{breakPoint.Item2}";
        }

        public string Background { get; set; }

        public List<Tuple<int, int>> Breaks { get; set; }

        public List<SbObject> SbObjects { get; set; }

        public List<SbSound> SampleSounds { get; set; }
    }
}