using System.Collections.Generic;
using System.ComponentModel;
using OsuParser.Exceptions;

namespace OsuParser.Structures.Events
{
    public class SbObjects
    {
        public enum Imagetype
        {
            Sprite,
            Animation
        }

        public enum SbLayer
        {
            Background,
            Fail,
            Pass,
            Foreground
        }

        public enum SbOrigin
        {
            TopLeft,
            TopCentre,
            TopRight,
            CentreLeft,
            Centre,
            CentreRight,
            BottomLeft,
            BottomCentre,
            BottomRight
        }

        public enum SbLooptype
        {
            LoopForever,
            LoopOnce
        }

        // Default constructor is made with Sprite Object
        public SbObjects()
        {
            Type = Imagetype.Sprite;
            Layer = SbLayer.Background;
            Origin = SbOrigin.Centre;
            FilePath = string.Empty;
            X = Y = 0;

            // Only for Animation object.
            FrameCount = 0;
            FrameDelay = 0;
            Looptype = SbLooptype.LoopOnce;

            // Initialize Action List
            ActionList = new List<SbAction>();
        }
        
        [Description("Only for creating a Sprite object")]
        public SbObjects(Imagetype type, SbLayer layer, SbOrigin origin, string filePath, int x, int y)
        {
            if(type != Imagetype.Sprite)
                throw new InvalidBeatmapException("SbType not matched");

            Type = type;    // Imagetype.Sprite
            Layer = layer;
            Origin = origin;
            FilePath = filePath;
            X = x;
            Y = y;

            ActionList = new List<SbAction>();
        }

        [Description("Only for creating a Animation object")]
        public SbObjects(Imagetype type, SbLayer layer, SbOrigin origin, string filePath, int x, int y, int frameCount,
            int frameDelay, SbLooptype loopType)
        {
            if(type != Imagetype.Animation)
                throw new InvalidBeatmapException("SbType not matched");

            Type = type;
            Layer = layer;
            Origin = origin;
            FilePath = filePath;
            X = x;
            Y = y;
            FrameCount = frameCount;
            FrameDelay = frameDelay;
            Looptype = loopType;

            ActionList = new List<SbAction>();
        }

        public SbObjects(SbObjects prevSbObjects)
        {
            Type = prevSbObjects.Type;
            Layer = prevSbObjects.Layer;
            Origin = prevSbObjects.Origin;
            FilePath = prevSbObjects.FilePath;
            X = prevSbObjects.X;
            Y = prevSbObjects.Y;

            FrameCount = prevSbObjects.FrameCount;
            FrameDelay = prevSbObjects.FrameDelay;
            Looptype = prevSbObjects.Looptype;

            ActionList = new List<SbAction>(prevSbObjects.ActionList);
        }
        
        public Imagetype Type { get; set; }

        public SbLayer Layer { get; set; }

        public SbOrigin Origin { get; set; }

        public string FilePath { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        // The followings are for 'Animation' only.
        public int FrameCount { get; set; }

        public int FrameDelay { get; set; }

        public SbLooptype Looptype { get; set; }

        // Actions belonging this SbObject.
        public List<SbAction> ActionList { get; set; }
    }
}