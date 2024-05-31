using System;

namespace BehaviorTree.Runtime
{
    public class Condition : ConditionBase
    {
        public Func<bool> UpdateLogic;
        public Action StartLogic;
        public Action InitLogic;
        public Action ExitLogic;

        protected override bool GetCondition()
        {
            return UpdateLogic == null || UpdateLogic();
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