namespace BehaviorTree.Runtime
{
    public class Log : ActionBase
    {
        public SharedString Text;

        protected override TaskStatus OnUpdate()
        {
            UnityEngine.Debug.Log(Text.Value);
            return TaskStatus.Success;
        }
    }
}