using System;

namespace BT.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TaskIconAttribute : Attribute
    {
        public TaskIconAttribute(string iconString)
        {
            IconPath = iconString;
        }

        public string IconPath { get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TaskIconPaddingAttribute : Attribute
    {
        public TaskIconPaddingAttribute(float iconPadding)
        {
            IconPadding = iconPadding;
        }

        public float IconPadding { get; }
    }
}