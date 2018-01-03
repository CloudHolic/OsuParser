using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OsuParser.Exceptions;

namespace OsuParser.Structures.Events
{
    public enum Imagetype
    {
        Sprite,
        Animation
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

    public enum Command
    {
        Fade,
        Move,
        MoveX,
        MoveY,
        Scale,
        VectorScale,
        Rotate,
        Color,
        Parameter,
        Loop,
        Trigger
    }

    internal static class EnumExtensions
    {
        internal static string MyToString(this Command command)
        {
            switch (command)
            {
                case Command.Fade:
                    return "F";
                case Command.Move:
                    return "M";
                case Command.MoveX:
                    return "MX";
                case Command.MoveY:
                    return "MY";
                case Command.Scale:
                    return "S";
                case Command.VectorScale:
                    return "V";
                case Command.Rotate:
                    return "R";
                case Command.Color:
                    return "C";
                case Command.Parameter:
                    return "P";
                case Command.Loop:
                    return "L";
                case Command.Trigger:
                    return "T";
                default:
                    throw new InvalidBeatmapException("Unknown SbAction Command.");
            }
        }

        internal static Command CommandParse(string parseStr)
        {
            if (parseStr == MyToString(Command.Fade))
                return Command.Fade;
            if (parseStr == MyToString(Command.Move))
                return Command.Move;
            if (parseStr == MyToString(Command.MoveX))
                return Command.MoveX;
            if (parseStr == MyToString(Command.MoveY))
                return Command.MoveY;
            if (parseStr == MyToString(Command.Scale))
                return Command.Scale;
            if (parseStr == MyToString(Command.VectorScale))
                return Command.VectorScale;
            if (parseStr == MyToString(Command.Rotate))
                return Command.Rotate;
            if (parseStr == MyToString(Command.Color))
                return Command.Color;
            if (parseStr == MyToString(Command.Parameter))
                return Command.Parameter;
            if (parseStr == MyToString(Command.Loop))
                return Command.Loop;
            if (parseStr == MyToString(Command.Trigger))
                return Command.Trigger;

            throw new InvalidBeatmapException("Unknown SbAction Command.");
        }
    }
}