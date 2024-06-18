using System.Collections.Generic;

namespace BT.Runtime
{
    public class BehaviorTreeBuilder
    {
        private readonly BehaviorTree _tree;
        private readonly List<TaskParentBase> _pointers = new List<TaskParentBase>();

        private TaskParentBase PointerCurrent
        {
            get
            {
                if (_pointers.Count == 0) return null;
                return _pointers[_pointers.Count - 1];
            }
        }

        public Blackboard SelfBlackboard { get; }

        public BehaviorTreeBuilder(Blackboard selfBlackboard)
        {
            SelfBlackboard = selfBlackboard;
            _tree = new BehaviorTree(selfBlackboard);
            _pointers.Add(_tree.Root);
        }

        public BehaviorTreeBuilder(Blackboard selfBlackboard, List<Blackboard> sharedBlackboards)
        {
            SelfBlackboard = selfBlackboard;
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

        public BehaviorTree GetCurTree()
        {
            return _tree;
        }

        public BehaviorTreeBuilder AddTreeFromJsonReader(IJsonReader reader)
        {
            reader.Add2Tree(this);
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