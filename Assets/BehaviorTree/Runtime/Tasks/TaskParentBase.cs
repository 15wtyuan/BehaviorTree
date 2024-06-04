using System.Collections.Generic;

namespace BehaviorTree.Runtime
{
    public abstract class TaskParentBase : TaskBase
    {
        private bool _init;
        private bool _start;
        private bool _exit;
        private int _lastRoundCount;

        public List<TaskBase> Children { get; } = new();

        protected virtual int MaxChildren => -1;

        #region 子类继承

        protected virtual void OnInit()
        {
        }

        protected virtual void OnStart()
        {
        }

        protected abstract TaskStatus OnUpdate();

        protected virtual void OnExit()
        {
        }

        protected virtual void OnReset()
        {
        }

        #endregion

        public override TaskStatus Update()
        {
            base.Update();
            UpdateRound();

            if (!_init)
            {
                Init();
                _init = true;
            }

            if (!_start)
            {
                Start();
                _start = true;
                _exit = true;
            }

            var status = OnUpdate();
            LastStatus = status;
            if (status != TaskStatus.Continue)
            {
                Exit();
            }

            return status;
        }

        private void UpdateRound()
        {
            if (Tree == null)
            {
                return;
            }

            if (_lastRoundCount != Tree.RoundCount)
            {
                Reset();
            }

            _lastRoundCount = Tree.RoundCount;
        }

        public virtual TaskParentBase AddChild(TaskBase child)
        {
            if (!child.Enabled)
            {
                return this;
            }

            if (Children.Count < MaxChildren || MaxChildren < 0)
            {
                Children.Add(child);
            }

            return this;
        }

        private void Init()
        {
            OnInit();
        }

        private void Start()
        {
            OnStart();
        }

        public override void End()
        {
            Exit();
        }

        protected override void Reset()
        {
            _start = false;
            _exit = false;

            OnReset();
        }

        private void Exit()
        {
            if (_exit)
            {
                OnExit();
            }

            Reset();
        }
    }
}