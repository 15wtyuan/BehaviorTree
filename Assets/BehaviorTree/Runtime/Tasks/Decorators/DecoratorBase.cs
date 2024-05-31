namespace BehaviorTree.Runtime
{
    public abstract class DecoratorBase : TaskParentBase
    {
        public TaskBase Child => Children.Count > 0 ? Children[0] : null;

        public override void End()
        {
            Child.End();
        }

        protected override void Reset()
        {
        }

        public override TaskParentBase AddChild(TaskBase child)
        {
            if (Child == null)
            {
                Children.Add(child);
            }

            return this;
        }
    }
}