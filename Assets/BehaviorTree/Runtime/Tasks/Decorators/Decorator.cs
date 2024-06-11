using System;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Decorator(this BehaviorTreeBuilder builder, string name,
            Func<TaskBase, TaskStatus> logic)
        {
            var decorator = new Decorator
            {
                UpdateLogic = logic,
                Name = name
            };

            return builder.AddNodeWithPointer(decorator);
        }

        public static BehaviorTreeBuilder Decorator(this BehaviorTreeBuilder builder,
            Func<TaskBase, TaskStatus> logic)
        {
            return builder.Decorator("Decorator", logic);
        }
    }

    public class Decorator : DecoratorBase
    {
        public Func<TaskBase, TaskStatus> UpdateLogic;

        protected override TaskStatus OnUpdate()
        {
            return UpdateLogic?.Invoke(Child) ?? Child.Update();
        }
    }
}