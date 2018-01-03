using System;
using System.Collections.Generic;
using System.IO;
using OsuParser.Exceptions;
using OsuParser.Parsers;

namespace OsuParser.Structures.Events
{
    public enum SbLayer
    {
        Background,
        Fail,
        Pass,
        Foreground
    }

    public class Storyboard
    {
        internal Storyboard()
        {
            Background = String.Empty;
            Breaks = new List<Tuple<int, int>>();
            SbObjects = new List<SbObject>();
            SampleSounds = new List<SbSound>();
        }

        internal Storyboard(string filename)
        {
            //  Load, and parse.
            if (File.Exists(filename))
            {
                if (filename.Split('.')[filename.Split('.').Length - 1] != "osb")
                    throw new InvalidBeatmapException("Unknown file format.");

                var temp = EventParser.Parse(filename);
                Background = temp.Background;
                Breaks = new List<Tuple<int, int>>(temp.Breaks);
                SbObjects = new List<SbObject>(temp.SbObjects);
                SampleSounds = new List<SbSound>(temp.SampleSounds);
            }
            else
                throw new FileNotFoundException();
        }

        internal Storyboard(string background, IEnumerable<Tuple<int, int>> breaks, IEnumerable<SbObject> sbObjects, IEnumerable<SbSound> sounds)
        {
            Background = background;
            Breaks = new List<Tuple<int, int>>(breaks);
            SbObjects = new List<SbObject>(sbObjects);
            SampleSounds = new List<SbSound>(sounds);
        }

        internal Storyboard(Storyboard prevEvent)
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