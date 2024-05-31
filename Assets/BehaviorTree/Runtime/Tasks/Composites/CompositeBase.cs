namespace BehaviorTree.Runtime
{
    public abstract class CompositeBase : TaskParentBase
    {
        public int ChildIndex { get; protected set; }

        public override void End()
        {
            if (ChildIndex < Children.Count)
            {
                Children[ChildIndex].End();
            }
        }

        protected override void Reset()
        {
            ChildIndex = 0;
        }
    }
}