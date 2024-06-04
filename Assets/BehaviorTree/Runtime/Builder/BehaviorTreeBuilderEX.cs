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

        public BehaviorTreeBuilder EventBreak(SharedString eventType)
        {
            return EventBreak("EventBreak", eventType);
        }

        public BehaviorTreeBuilder EventBreak(string name, SharedString eventType)
        {
            var eventBreak = new EventBreak
            {
                Name = name,
                EventType = eventType
            };

            return AddNodeWithPointer(eventBreak);
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

        public BehaviorTreeBuilder ParallelSelector(string name = "ParallelSelector")
        {
            return ParentTask<ParallelSelector>(name);
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

        public BehaviorTreeBuilder RandomProbability(string name, SharedFloat successProbability, SharedInt seed)
        {
            return AddNode(new RandomProbability
            {
                Name = name,
                SuccessProbability = successProbability,
                Seed = seed
            });
        }

        public BehaviorTreeBuilder RandomProbability(SharedFloat successProbability, SharedInt seed)
        {
            return RandomProbability("Random Probability", successProbability, seed);
        }

        public BehaviorTreeBuilder WaitTime(string name, SharedFloat time)
        {
            return AddNode(new WaitTime(new TimeMonitor())
            {
                Name = name,
                Time = time
            });
        }

        public BehaviorTreeBuilder WaitTime(SharedFloat time)
        {
            return WaitTime("Wait Time", time);
        }

        public BehaviorTreeBuilder Wait(string name, SharedInt turns)
        {
            return AddNode(new Wait
            {
                Name = name,
                Turns = turns
            });
        }

        public BehaviorTreeBuilder Wait(SharedInt turns)
        {
            return Wait("wait", turns);
        }

        public BehaviorTreeBuilder Log(string name, SharedString text)
        {
            return AddNode(new Log
            {
                Name = name,
                Text = text
            });
        }

        public BehaviorTreeBuilder Log(SharedString text)
        {
            return Log("log", text);
        }

        public BehaviorTreeBuilder SendEvent(string name, SharedString eventType)
        {
            return AddNode(new SendEvent
            {
                Name = name,
                EventType = eventType
            });
        }

        public BehaviorTreeBuilder SendEvent(SharedString eventType)
        {
            return SendEvent("SendEvent", eventType);
        }

        public BehaviorTreeBuilder WaitEvent(string name, SharedString eventType)
        {
            return AddNode(new WaitEvent
            {
                Name = name,
                EventType = eventType
            });
        }

        public BehaviorTreeBuilder WaitEvent(SharedString eventType)
        {
            return WaitEvent("WaitEvent", eventType);
        }
    }
}