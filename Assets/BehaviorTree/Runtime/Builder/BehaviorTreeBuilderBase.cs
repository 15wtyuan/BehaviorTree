using System.Collections.Generic;

namespace BT.Runtime
{
    public partial class BehaviorTreeBuilder
    {
        private readonly BehaviorTree _tree;
        private readonly List<TaskParentBase> _pointers = new List<TaskParentBase>();
        private readonly Blackboard _selfBlackboard;

        private TaskParentBase PointerCurrent
        {
            get
            {
                if (_pointers.Count == 0) return null;
                return _pointers[_pointers.Count - 1];
            }
        }

        public BehaviorTreeBuilder(Blackboard selfBlackboard)
        {
            _selfBlackboard = selfBlackboard;
            _tree = new BehaviorTree(selfBlackboard);
            _pointers.Add(_tree.Root);
        }

        public BehaviorTreeBuilder(Blackboard selfBlackboard, List<Blackboard> sharedBlackboards)
        {
            _selfBlackboard = selfBlackboard;
            _tree = new BehaviorTree(selfBlackboard, sharedBlackboards);
            _pointers.Add(_tree.Root);
        }

        public BehaviorTreeBuilder Name(string name)
        {
            _tree.Name = name;
            return this;
        }

        public BehaviorTreeBuilder ParentTask<T>(string name) where T : TaskParentBase, new()
        {
            var parent = new T { Name = name };

            return AddNodeWithPointer(parent);
        }

        public BehaviorTreeBuilder AddNodeWithPointer(TaskParentBase task)
        {
            AddNode(task);
            _pointers.Add(task);

            return this;
        }

        public BehaviorTreeBuilder AddNode(TaskBase node)
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