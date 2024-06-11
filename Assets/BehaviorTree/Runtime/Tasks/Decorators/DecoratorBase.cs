namespace BT.Runtime
{
    public abstract class DecoratorBase : TaskParentBase
    {
        public TaskBase Child => Children.Count > 0 ? Children[0] : null;

        protected override void OnExit()
        {
            base.OnExit();
            Child.End();
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