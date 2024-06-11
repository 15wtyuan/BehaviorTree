using System;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Condition(this BehaviorTreeBuilder builder, string name,
            Func<bool> action)
        {
            return builder.AddNode(new Condition
            {
                Name = name,
                UpdateLogic = action
            });
        }

        public static BehaviorTreeBuilder Condition(this BehaviorTreeBuilder builder,
            Func<bool> action)
        {
            return builder.Condition("Condition", action);
        }
    }

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