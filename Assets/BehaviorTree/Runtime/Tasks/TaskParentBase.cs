using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public abstract class TaskParentBase : TaskBase
    {
        private int _lastTickCount;

        public List<TaskBase> Children { get; } = new();

        protected virtual int MaxChildren => -1;

        public override TaskStatus Update()
        {
            base.Update();
            UpdateTicks();

            var status = OnUpdate();
            LastStatus = status;
            if (status != TaskStatus.Continue)
            {
                Reset();
            }

            return status;
        }

        private void UpdateTicks()
        {
            if (Tree == null)
            {
                return;
            }

            if (_lastTickCount != Tree.TickCount)
            {
                Reset();
            }

            _lastTickCount = Tree.TickCount;
        }

        protected abstract TaskStatus OnUpdate();

        public virtual TaskParentBase AddChild(TaskBase child)
        {
            if (!child.Enabled)
            {
                return this;
            }

            if (Children.Count < MaxChildren || MaxChildren < 0)
            {
                Children.Add(child);
            }

            return this;
        }
    }
}