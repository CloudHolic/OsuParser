using System;
using System.Linq;
using OsuParser.Exceptions;

// ReSharper disable CompareOfFloatsByEqualityOperator
namespace OsuParser.Structures.Events
{
    public class SbAction
    {
        // Default constructor is made with Fade command.
        // No depth, no group.
        public SbAction()
        {
            Action = Command.Fade;
            Easing = 0;
            StartTime = 0;
            EndTime = null;
            StartParameter = Tuple.Create((double)0, (double)0, 0);
            EndParameter = Tuple.Create<double?, double?, int?>(null, null, null);
            EtcParameter = string.Empty;
            Depth = 1;
            Group = 0;
        }

        public SbAction(Command action, int easing, int startTime, int endTime,
            Tuple<double, double, int> startParameter, Tuple<double?, double?, int?> endParameter, string etcParameter,
            int depth, int group)
        {
            Action = action;
            Easing = easing;
            StartTime = startTime;
            EndTime = endTime;
            StartParameter = startParameter;
            EndParameter = endParameter;
            EtcParameter = etcParameter;
            Depth = depth;
            Group = group;
        }

        public SbAction(SbAction prevAction)
        {
            Action = prevAction.Action;
            Easing = prevAction.Easing;
            StartTime = prevAction.StartTime;
            EndTime = prevAction.EndTime;
            StartParameter = prevAction.StartParameter;
            EndParameter = prevAction.EndParameter;
            EtcParameter = prevAction.EtcParameter;
            Depth = prevAction.Depth;
            Group = prevAction.Group;
        }

        public override string ToString()
        {
            bool isInt;
            var result = string.Concat(Enumerable.Repeat(" ", Depth)) + $"{Action.MyToString()},";

            switch (Action)
            {
                case Command.Loop:
                    result += $"{StartTime},{EtcParameter}";
                    break;

                case Command.Trigger:
                    result += $"{EtcParameter},{StartTime},{EndTime}";
                    break;

                case Command.Fade:
                case Command.MoveX:
                case Command.MoveY:
                case Command.Scale:
                case Command.Rotate:
                    isInt = ParameterIsInt(1);
                    result += $"{Easing},{StartTime},{EndTime}" + StartParamToString(1, isInt) + EndParamToString(1, isInt);
                    break;

                case Command.Move:
                case Command.VectorScale:
                    isInt = ParameterIsInt(2);
                    result += $"{Easing},{StartTime},{EndTime}" + StartParamToString(2, isInt) + EndParamToString(2, isInt);
                    break;

                case Command.Color:
                    isInt = ParameterIsInt(3);
                    result += $"{Easing},{StartTime},{EndTime}" + StartParamToString(3, isInt) + EndParamToString(3, isInt);
                    break;

                case Command.Parameter:
                    result += $"{Easing},{StartTime},{EndTime},{EtcParameter}";
                    break;

                default:
                    throw new InvalidBeatmapException("Unknown SbAction Command.");
            }

            return result;
        }

        private bool ParameterIsInt(int num)
        {
            bool result = true;
            if (num > 0)
                result &= StartParameter.Item1 % 1 == 0 && (!EndParameter.Item1.HasValue || EndParameter.Item1.Value % 1 == 0);
            if (num > 1)
                result &= StartParameter.Item2 % 1 == 0 && (!EndParameter.Item2.HasValue || EndParameter.Item2.Value % 1 == 0);
            if (num > 2)
                result &= StartParameter.Item3 % 1 == 0 && (!EndParameter.Item3.HasValue || EndParameter.Item3.Value % 1 == 0);

            return result;
        }

        private string StartParamToString(int num, bool isInt)
        {
            var result = string.Empty;
            if (num > 0)
                result += $",{(isInt ? (int)StartParameter.Item1 : StartParameter.Item1)}";
            if (num > 1)
                result += $",{(isInt ? (int)StartParameter.Item2 : StartParameter.Item2)}";
            if (num > 2)
                result += $",{(isInt ? (int)StartParameter.Item3 : StartParameter.Item3)}";

            return result;
        }

        private string EndParamToString(int num, bool isInt)
        {
            var result = string.Empty;
            if (num > 0)
                result += EndParameter.Item1.HasValue ? $",{(isInt ? (int)EndParameter.Item1 : EndParameter.Item1)}" : string.Empty;
            if (num > 1)
                result += EndParameter.Item2.HasValue ? $",{(isInt ? (int)EndParameter.Item2 : EndParameter.Item2)}" : string.Empty;
            if (num > 2)
                result += EndParameter.Item3.HasValue ? $",{(isInt ? (int)EndParameter.Item3 : EndParameter.Item3)}" : string.Empty;

            return result;
        }

        public Command Action { get; set; }

        public int Easing { get; set; }

        public int StartTime { get; set; }

        public int? EndTime { get; set; }   // EndTime is nullable.

        // Some parameters doesn't use by Command.
        public Tuple<double, double, int> StartParameter { get; set; }

        public Tuple<double?, double?, int?> EndParameter { get; set; } // EndParameters are nullable.

        public string EtcParameter { get; set; }

        // Action depth (can be increased by Loop or Trigger)
        public int Depth { get; set; }

        // Action group (can only be used in commands below Trigger)
        public int Group { get; set; }
    }
}
