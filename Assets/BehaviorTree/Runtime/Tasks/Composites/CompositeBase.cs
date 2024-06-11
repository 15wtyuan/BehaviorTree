namespace BT.Runtime
{
    public abstract class CompositeBase : TaskParentBase
    {
        public int ChildIndex { get; protected set; }

        protected override void OnExit()
        {
            if (ChildIndex < Children.Count)
            {
                Children[ChildIndex].End();
            }
        }

        protected override void OnReset()
        {
            ChildIndex = 0;
        }
    }
}