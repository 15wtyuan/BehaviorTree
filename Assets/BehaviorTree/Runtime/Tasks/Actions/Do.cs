using System;

namespace BehaviorTree.Runtime
{
    public class Do : ActionBase
    {
        public Func<TaskStatus> UpdateLogic;
        public Action StartLogic;
        public Action InitLogic;
        public Action ExitLogic;

        protected override TaskStatus OnUpdate()
        {
            return UpdateLogic?.Invoke() ?? TaskStatus.Success;
        }

        protected override void OnStart()
        {
            StartLogic?.Invoke();
        }

        protected override void OnExit()
        {
            ExitLogic?.Invoke();
        }

        protected override void OnInit()
        {
            InitLogic?.Invoke();
        }
    }
}