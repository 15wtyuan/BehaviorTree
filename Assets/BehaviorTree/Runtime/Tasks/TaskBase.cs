#if UNITY_EDITOR
using UnityEngine.Events;
#endif

namespace BT.Runtime
{
    public abstract class TaskBase
    {
        #region 编辑器用

#if UNITY_EDITOR
        public bool HasBeenActive { get; private set; }
        public readonly UnityEvent EventActive = new UnityEvent();
#endif
        #endregion

        public string Name { get; set; }
        public TaskStatus LastStatus { get; protected set; }
        public bool Enabled { get; set; } = true;
        public Blackboard SharedBlackboard { get; set; }
        public IBehaviorTree Tree { get; set; }

        public virtual TaskStatus Update()
        {
#if UNITY_EDITOR
            EventActive.Invoke();
            HasBeenActive = true;
#endif

            return TaskStatus.Success;
        }

        public abstract void End();
    }
}