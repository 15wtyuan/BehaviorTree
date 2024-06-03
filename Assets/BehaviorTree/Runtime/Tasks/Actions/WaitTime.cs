namespace BehaviorTree.Runtime
{
    public interface ITimeMonitor
    {
        float DeltaTime { get; }
    }

    [TaskIcon("Hourglass.png")]
    public class WaitTime : ActionBase
    {
        public SharedFloat Time;
        private readonly ITimeMonitor _timeMonitor;
        private float _timePassed;

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

            if (_timePassed < Time.Value)
            {
                return TaskStatus.Continue;
            }

            return TaskStatus.Success;
        }
    }
}