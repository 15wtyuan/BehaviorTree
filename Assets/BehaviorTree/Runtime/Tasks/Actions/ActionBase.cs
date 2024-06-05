namespace BehaviorTree.Runtime
{
    public abstract class ActionBase : TaskBase
    {
        private bool _init;
        private bool _start;
        private bool _exit;
        private int _lastRoundCount;
        private bool _active;

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
                if (_active) Tree?.RemoveActiveTask(this);
                Exit();
            }
            else if (!_active)
            {
                Tree?.AddActiveTask(this);
                _active = true;
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

        private void Reset()
        {
            _active = false;
            _start = false;
            _exit = false;

            OnReset();
        }

        public override void End()
        {
            Exit();
        }

        private void Init()
        {
            OnInit();
        }

        private void Start()
        {
            OnStart();
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