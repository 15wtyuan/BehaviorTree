namespace BT.Runtime
{
    [TaskIcon("Question.png")]
    [TaskIconPadding(10)]
    public abstract class ConditionBase : ActionBase
    {
        protected abstract bool GetCondition();

        protected override TaskStatus OnUpdate()
        {
            if (GetCondition())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}