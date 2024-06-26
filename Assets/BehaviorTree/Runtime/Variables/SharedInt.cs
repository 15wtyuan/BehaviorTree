﻿namespace BT.Runtime
{
    public class SharedInt : SharedVariable<int>
    {
        public static implicit operator SharedInt(int value)
        {
            return new SharedInt { Value = value };
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}