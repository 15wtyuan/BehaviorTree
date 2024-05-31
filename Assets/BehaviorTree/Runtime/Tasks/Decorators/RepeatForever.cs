namespace BehaviorTree.Runtime
{
    [TaskIcon("Repeat.png")]
    public class RepeatForever : DecoratorBase
    {
        protected override TaskStatus OnUpdate()
        {
            Child.Update();
            return TaskStatus.Continue;
        }
    }
}