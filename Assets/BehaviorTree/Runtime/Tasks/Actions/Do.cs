using System;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder Do(this BehaviorTreeBuilder builder, string name,
            Func<TaskStatus> action)
        {
            return builder.AddNode(new Do
            {
                Name = name,
                UpdateLogic = action
            });
        }

        public static BehaviorTreeBuilder Do(this BehaviorTreeBuilder builder,
            Func<TaskStatus> action)
        {
            return builder.Do("Do", action);
        }
    }

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