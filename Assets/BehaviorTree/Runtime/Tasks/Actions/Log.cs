namespace BehaviorTree.Runtime
{
    public class Log : ActionBase
    {
        public SharedString Text;

        protected override TaskStatus OnUpdate()
        {
            UnityEngine.Debug.Log(Text);
            return TaskStatus.Success;
        }
    }
}