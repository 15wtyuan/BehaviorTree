using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Runtime
{
    public interface IBehaviorTree
    {
        string Name { get; }
        TaskRoot Root { get; }
        int TickCount { get; }

        void AddActiveTask(TaskBase task);
        void RemoveActiveTask(TaskBase task);
    }

    [System.Serializable]
    public class BehaviorTree : IBehaviorTree
    {
        private readonly GameObject _owner;
        private readonly List<TaskBase> _tasks = new List<TaskBase>();

        public int TickCount { get; private set; }

        public string Name { get; set; }
        public TaskRoot Root { get; } = new TaskRoot();
        public IReadOnlyList<TaskBase> ActiveTasks => _tasks;

        public BehaviorTree(GameObject owner)
        {
            _owner = owner;
            SyncNodes(Root);
        }

        public TaskStatus Tick()
        {
            var status = Root.Update();
            if (status != TaskStatus.Continue)
            {
                Reset();
            }

            return status;
        }

        public void Reset()
        {
            foreach (var task in _tasks)
            {
                task.End();
            }

            _tasks.Clear();
            TickCount++;
        }

        public void AddNode(TaskParentBase parent, TaskBase child)
        {
            parent.AddChild(child);
            child.Tree = this;
            child.Owner = _owner;
        }

        public void Splice(TaskParentBase parent, BehaviorTree tree)
        {
            parent.AddChild(tree.Root);

            SyncNodes(tree.Root);
        }

        private void SyncNodes(TaskParentBase taskParent)
        {
            taskParent.Owner = _owner;
            taskParent.Tree = this;

            foreach (var child in taskParent.Children)
            {
                child.Owner = _owner;
                child.Tree = this;

                if (child is TaskParentBase parent)
                {
                    SyncNodes(parent);
                }
            }
        }

        public void AddActiveTask(TaskBase task)
        {
            _tasks.Add(task);
        }

        public void RemoveActiveTask(TaskBase task)
        {
            _tasks.Remove(task);
        }
    }
}