namespace BehaviorTree.Runtime
{
    public interface ITimeMonitor
    {
        float DeltaTime { get; }
    }

    [TaskIcon("Hourglass.png")]
    public class WaitTime : ActionBase
    {
        private ITimeMonitor _timeMonitor;
        private float _timePassed;

        public float Time = 1;

        public WaitTime(ITimeMonitor timeMonitor)
        {
            _timeMonitor = timeMonitor;
        }

        protected override void OnStart()
        {
            _timePassed = 0;
        }

        protected override TaskStatus OnUpdate()
        {
            _timePassed += _timeMonitor.DeltaTime;

            if (_timePassed < Time)
            {
                return TaskStatus.Continue;
            }

            return TaskStatus.Success;
        }
    }
}