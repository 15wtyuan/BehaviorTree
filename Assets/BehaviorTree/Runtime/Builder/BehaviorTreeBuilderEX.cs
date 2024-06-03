using System;

namespace BehaviorTree.Runtime
{
    public partial class BehaviorTreeBuilder
    {
        public BehaviorTreeBuilder Decorator(string name, Func<TaskBase, TaskStatus> logic)
        {
            var decorator = new Decorator
            {
                UpdateLogic = logic,
                Name = name
            };

            return AddNodeWithPointer(decorator);
        }

        public BehaviorTreeBuilder Decorator(Func<TaskBase, TaskStatus> logic)
        {
            return Decorator("decorator", logic);
        }

        public BehaviorTreeBuilder Inverter(string name = "inverter")
        {
            return ParentTask<Inverter>(name);
        }

        public BehaviorTreeBuilder ReturnSuccess(string name = "return success")
        {
            return ParentTask<ReturnSuccess>(name);
        }

        public BehaviorTreeBuilder ReturnFailure(string name = "return failure")
        {
            return ParentTask<ReturnFailure>(name);
        }

        public BehaviorTreeBuilder RepeatUntilSuccess(string name = "repeat until success")
        {
            return ParentTask<RepeatUntilSuccess>(name);
        }

        public BehaviorTreeBuilder RepeatUntilFailure(string name = "repeat until failure")
        {
            return ParentTask<RepeatUntilFailure>(name);
        }

        public BehaviorTreeBuilder RepeatForever(string name = "repeat forever")
        {
            return ParentTask<RepeatForever>(name);
        }

        public BehaviorTreeBuilder Sequence(string name = "sequence")
        {
            return ParentTask<Sequence>(name);
        }

        public BehaviorTreeBuilder Selector(string name = "selector")
        {
            return ParentTask<Selector>(name);
        }

        public BehaviorTreeBuilder RandomSelector(string name = "selector random")
        {
            return ParentTask<RandomSelector>(name);
        }

        public BehaviorTreeBuilder Parallel(string name = "parallel")
        {
            return ParentTask<Parallel>(name);
        }

        public BehaviorTreeBuilder Do(string name, Func<TaskStatus> action)
        {
            return AddNode(new Do
            {
                Name = name,
                UpdateLogic = action
            });
        }

        public BehaviorTreeBuilder Do(Func<TaskStatus> action)
        {
            return Do("action", action);
        }

        public BehaviorTreeBuilder Condition(string name, Func<bool> action)
        {
            return AddNode(new Condition
            {
                Name = name,
                UpdateLogic = action
            });
        }

        public BehaviorTreeBuilder Condition(Func<bool> action)
        {
            return Condition("condition", action);
        }

        public BehaviorTreeBuilder RandomProbability(string name, object successProbability, object seed)
        {
            return AddNode(new RandomProbability
            {
                Name = name,
                SuccessProbability = (SharedFloat)successProbability,
                Seed = (SharedInt)seed
            });
        }

        public BehaviorTreeBuilder RandomProbability(object successProbability, object seed)
        {
            return RandomProbability("Random Probability", successProbability, seed);
        }

        public BehaviorTreeBuilder WaitTime(string name, object time)
        {
            return AddNode(new WaitTime(new TimeMonitor())
            {
                Name = name,
                Time = (SharedFloat)time
            });
        }

        public BehaviorTreeBuilder WaitTime(object time)
        {
            return WaitTime("Wait Time", time);
        }

        public BehaviorTreeBuilder Wait(string name, object turns)
        {
            return AddNode(new Wait
            {
                Name = name,
                Turns = (SharedInt)turns
            });
        }

        public BehaviorTreeBuilder Wait(object turns)
        {
            return Wait("wait", turns);
        }
    }
}