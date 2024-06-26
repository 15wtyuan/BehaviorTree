using System.Collections.Generic;

namespace BT.Runtime
{
    public interface IBehaviorTree
    {
        string Name { get; }
        TaskRoot Root { get; }
        int RoundCount { get; }

        void AddActiveTask(TaskBase task);
        void RemoveActiveTask(TaskBase task);

        string GetSharedBlackboardPrint();
    }

    [System.Serializable]
    public class BehaviorTree : IBehaviorTree
    {
        private readonly List<TaskBase> _activeTasks = new List<TaskBase>();
        private TreeStatus _status = TreeStatus.End;
        private bool _loop;

        public int RoundCount { get; private set; }

        public string Name { get; set; }
        public TaskRoot Root { get; } = new TaskRoot();
        public IReadOnlyList<TaskBase> ActiveTasks => _activeTasks;
        public Blackboard SelfBlackboard { get; }
        public List<Blackboard> SharedBlackboards;

        public BehaviorTree(Blackboard selfBlackboard, List<Blackboard> sharedBlackboards)
        {
            SelfBlackboard = selfBlackboard;
            SharedBlackboards = sharedBlackboards;
            SyncNodes(Root);
        }

        public BehaviorTree(Blackboard selfBlackboard)
        {
            SelfBlackboard = selfBlackboard;
            SyncNodes(Root);
        }

        public void Start(bool loop = false)
        {
            if (_status != TreeStatus.End)
            {
                return;
            }

            _status = TreeStatus.Running;
            _loop = loop;
        }

        public TaskStatus Tick()
        {
            if (_status != TreeStatus.Running)
            {
                return TaskStatus.Failure;
            }

            var status = Root.Update();
            if (status != TaskStatus.Continue)
            {
                End(true);
            }

            return status;
        }

        public void Stop()
        {
            End(false);
        }

        private void End(bool checkLoop)
        {
            if (_status == TreeStatus.End)
            {
                return;
            }

            _status = TreeStatus.End;
            Reset();

            if (_loop && checkLoop)
            {
                Start(_loop);
            }
        }

        private void Reset()
        {
            foreach (var task in _activeTasks)
            {
                task.End();
            }

            _activeTasks.Clear();
            RoundCount++;
        }

        public void AddNode(TaskParentBase parent, TaskBase child)
        {
            parent.AddChild(child);
            child.Tree = this;
            child.SelfBlackboard = SelfBlackboard;
            child.SharedBlackboards = SharedBlackboards;
        }

        public void Splice(TaskParentBase parent, BehaviorTree tree)
        {
            parent.AddChild(tree.Root);

            SyncNodes(tree.Root);
        }

        private void SyncNodes(TaskParentBase taskParent)
        {
            taskParent.SelfBlackboard = SelfBlackboard;
            taskParent.SharedBlackboards = SharedBlackboards;
            taskParent.Tree = this;

            foreach (var child in taskParent.Children)
            {
                child.SelfBlackboard = SelfBlackboard;
                child.SharedBlackboards = SharedBlackboards;
                child.Tree = this;

                if (child is TaskParentBase parent)
                {
                    SyncNodes(parent);
                }
            }
        }

        public void AddActiveTask(TaskBase task)
        {
            _activeTasks.Add(task);
        }

        public void RemoveActiveTask(TaskBase task)
        {
            _activeTasks.Remove(task);
        }

        public string GetSharedBlackboardPrint()
        {
            return SelfBlackboard.GetSharedVariablePrint();
        }
    }
}