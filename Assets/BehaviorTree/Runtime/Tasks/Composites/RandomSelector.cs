namespace BehaviorTree.Runtime
{
    [TaskIcon("LinearScale.png")]
    public class RandomSelector : CompositeBase
    {
        private bool _init;

        protected override TaskStatus OnUpdate()
        {
            if (!_init)
            {
                ShuffleChildren();
                _init = true;
            }

            for (var i = ChildIndex; i < Children.Count; i++)
            {
                var child = Children[ChildIndex];

                switch (child.Update())
                {
                    case TaskStatus.Success:
                        return TaskStatus.Success;
                    case TaskStatus.Continue:
                        return TaskStatus.Continue;
                }

                ChildIndex++;
            }

            return TaskStatus.Failure;
        }

        protected override void Reset()
        {
            base.Reset();
            ShuffleChildren();
        }

        private void ShuffleChildren()
        {
            var rng = new System.Random();
            var n = Children.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (Children[k], Children[n]) = (Children[n], Children[k]);
            }
        }
    }
}