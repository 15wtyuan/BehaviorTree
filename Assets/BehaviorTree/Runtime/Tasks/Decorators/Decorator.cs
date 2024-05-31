using System;

namespace BehaviorTree.Runtime
{
    public class Decorator : DecoratorBase
    {
        public Func<TaskBase, TaskStatus> UpdateLogic;

        protected override TaskStatus OnUpdate()
        {
            return UpdateLogic?.Invoke(Child) ?? Child.Update();
        }
    }
}