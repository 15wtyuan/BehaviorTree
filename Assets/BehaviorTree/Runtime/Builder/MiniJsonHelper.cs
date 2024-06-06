using System;

namespace BehaviorTree.Runtime
{
    public static class MiniJsonHelper
    {
        public static int ParseInt(object value)
        {
            return value switch
            {
                long l => (int)l,
                string s => int.Parse(s),
                _ => throw new FormatException()
            };
        }

        public static float ParseFloat(object value)
        {
            return value switch
            {
                double d => (float)d,
                long l => l,
                string s => float.Parse(s),
                _ => throw new FormatException()
            };
        }

        public static bool ParseBool(object value)
        {
            return value switch
            {
                bool b => b,
                string s => bool.Parse(s),
                _ => throw new FormatException()
            };
        }

        public static string ParseString(object value)
        {
            return value.ToString();
        }
    }
}