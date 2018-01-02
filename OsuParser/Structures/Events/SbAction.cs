using System;

namespace OsuParser.Structures.Events
{
    public class SbAction
    {
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

        // Default constructor is made with Fade command.
        // No depth, no group.
        public SbAction()
        {
            Action = Command.Fade;
            Easing = 0;
            StartTime = 0;
            EndTime = 0;
            StartParameter = Tuple.Create((double)0, (double)0, 0);
            EndParameter = Tuple.Create((double) 0, (double) 0, 0);
            EtcParameter = string.Empty;
            Depth = 1;
            Group = 0;
        }

        public SbAction(Command action, int easing, int startTime, int endTime,
            Tuple<double, double, int> startParameter, Tuple<double, double, int> endParameter, string etcParameter,
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

        public Command Action { get; set; }

        public int Easing { get; set; }

        public int StartTime { get; set; }

        public int EndTime { get; set; }

        // Some parameters doesn't use by Command.
        public Tuple<double, double, int> StartParameter { get; set; }

        public Tuple<double, double, int> EndParameter { get; set; }

        public string EtcParameter { get; set; }

        // Action depth (can be increased by Loop or Trigger)
        public int Depth { get; set; }

        // Action group (can only be used in commands below Trigger)
        public int Group { get; set; }
    }
}
