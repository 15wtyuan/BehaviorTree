namespace BehaviorTree.Runtime
{
    [TaskIcon("HourglassFill.png")]
    public class Wait : ActionBase
    {
        public int Turns = 1;
        private int _ticks;

        protected override void OnStart()
        {
            _ticks = 0;
        }

        protected override TaskStatus OnUpdate()
        {
            if (_ticks >= Turns) return TaskStatus.Success;
            _ticks++;
            return TaskStatus.Continue;
        }
    }
}