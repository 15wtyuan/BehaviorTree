using System;

namespace BehaviorTree.Runtime
{
    public static class MiniJsonHelper
    {
        public static int ParseInt(object value)
        {
            switch (value)
            {
                case long l:
                    return (int)l;
                case string s:
                    return int.Parse(s);
                default:
                    throw new FormatException();
            }
        }

        public static float ParseFloat(object value)
        {
            switch (value)
            {
                case double d:
                    return (float)d;
                case long l:
                    return l;
                case string s:
                    return float.Parse(s);
                default:
                    throw new FormatException();
            }
        }

        public static bool ParseBool(object value)
        {
            switch (value)
            {
                case bool b:
                    return b;
                case string s:
                    return bool.Parse(s);
                default:
                    throw new FormatException();
            }
        }

        public static string ParseString(object value)
        {
            return value.ToString();
        }
    }
}