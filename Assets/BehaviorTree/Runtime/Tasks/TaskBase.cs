using UnityEngine;

namespace BehaviorTree.Runtime
{
    public abstract class TaskBase
    {
        #region 编辑器用

        public bool HasBeenActive { get; private set; }
        public TaskStatus LastStatus { get; set; }

        private EditorRuntimeUtilities _editorUtils;
        public EditorRuntimeUtilities EditorUtils => _editorUtils ??= new EditorRuntimeUtilities();

        #endregion

        public string Name { get; set; }

        public bool Enabled { get; set; } = true;
        public GameObject Owner { get; set; }
        public IBehaviorTree Tree { get; set; }

        public virtual TaskStatus Update()
        {
#if UNITY_EDITOR
            EditorUtils.EventActive.Invoke();
            HasBeenActive = true;
#endif

            return TaskStatus.Success;
        }

        public abstract void End();

        protected abstract void Reset();
    }
}