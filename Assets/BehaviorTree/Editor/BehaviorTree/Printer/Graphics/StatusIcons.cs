using System;
using BT.Runtime;

namespace BT.Editors
{
    public class StatusIcons
    {
        private TextureLoader Success { get; } = new TextureLoader("Success.png");
        private TextureLoader Failure { get; } = new TextureLoader("Success.png");
        private TextureLoader Continue { get; } = new TextureLoader("Success.png");

        public TextureLoader GetIcon(TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.Success:
                    return Success;
                case TaskStatus.Failure:
                    return Failure;
                case TaskStatus.Continue:
                    return Continue;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}