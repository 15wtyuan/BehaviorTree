using BehaviorTree.Runtime;
using UnityEngine;

namespace BehaviorTree.Samples
{
    public class TreeTestSample : MonoBehaviour {
        [SerializeField]
        private Runtime.BehaviorTree _treeA;

        [SerializeField]
        private Runtime.BehaviorTree _treeB;

        [SerializeField]
        private Runtime.BehaviorTree _treeC;

        [SerializeField]
        private bool _condition = false;

        private void Awake () {
            _treeA = new BehaviorTreeBuilder(gameObject)
                .Sequence()
                    .Condition("Custom Condition", () => true)
                    .Do("Custom Action A", () => TaskStatus.Success)
                    .Selector()
                        .Sequence("Nested Sequence")
                            .Condition("Custom Condition", () => _condition)
                            .Do("Custom Action", () => TaskStatus.Success)
                        .End()
                        .Sequence("Nested Sequence")
                            .Do("Custom Action", () => TaskStatus.Success)
                            .Sequence("Nested Sequence")
                                .Condition("Custom Condition", () => true)
                                .Do("Custom Action", () => TaskStatus.Success)
                            .End()
                        .End()
                        .Do("Custom Action", () => TaskStatus.Success)
                        .Condition("Custom Condition", () => true)
                    .End()
                    .Do("Custom Action B", () => TaskStatus.Success)
                .End()
                .Build();

            _treeB = new BehaviorTreeBuilder(gameObject)
                .Name("Basic")
                .Sequence()
                    .Condition("Custom Condition", () => _condition)
                    .Do("Custom Action A", () => TaskStatus.Success)
                .End()
                .Build();

            _treeC = new BehaviorTreeBuilder(gameObject)
                .Name("Basic")
                .Sequence()
                    .Condition("Custom Condition", () => _condition)
                    .Do("Continue", () => _condition ? TaskStatus.Continue : TaskStatus.Success)
                .End()
                .Build();
        }

        private void Update () {
            // Update our tree every frame
            _treeA.Tick();
            _treeB.Tick();
            _treeC.Tick();
        }
    }
}