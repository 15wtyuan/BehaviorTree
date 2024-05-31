using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Runtime
{
    public partial class BehaviorTreeBuilder
    {
        private readonly BehaviorTree _tree;
        private readonly List<TaskParentBase> _pointers = new();

        private TaskParentBase PointerCurrent
        {
            get
            {
                if (_pointers.Count == 0) return null;
                return _pointers[^1];
            }
        }

        public BehaviorTreeBuilder(GameObject owner)
        {
            _tree = new BehaviorTree(owner);
            _pointers.Add(_tree.Root);
        }

        public BehaviorTreeBuilder Name(string name)
        {
            _tree.Name = name;
            return this;
        }

        private BehaviorTreeBuilder ParentTask<T>(string name) where T : TaskParentBase, new()
        {
            var parent = new T { Name = name };

            return AddNodeWithPointer(parent);
        }

        private BehaviorTreeBuilder AddNodeWithPointer(TaskParentBase task)
        {
            AddNode(task);
            _pointers.Add(task);

            return this;
        }

        private BehaviorTreeBuilder AddNode(TaskBase node)
        {
            _tree.AddNode(PointerCurrent, node);
            return this;
        }

        public BehaviorTreeBuilder Splice(BehaviorTree tree)
        {
            _tree.Splice(PointerCurrent, tree);

            return this;
        }

        public BehaviorTreeBuilder End()
        {
            _pointers.RemoveAt(_pointers.Count - 1);

            return this;
        }

        public BehaviorTree Build()
        {
            return _tree;
        }
    }
}