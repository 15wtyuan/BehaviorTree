using BehaviorTree.Runtime;
using UnityEngine;

namespace BehaviorTree.Samples
{
    public class TestSample : MonoBehaviour
    {
        public bool isAirborne;
        [SerializeField] private Runtime.BehaviorTree tree;

        private Blackboard _sharedBlackboard;

        private void Awake()
        {
            //英雄：索敌，有敌人，先靠近敌人，再发动攻击，攻击的同时如果有技能就释放技能。如果被击飞了，就从头再来
            _sharedBlackboard = new Blackboard();
            
            // @formatter:off
            tree = new BehaviorTreeBuilder(_sharedBlackboard)
                .Sequence()
                    .RepeatUntilSuccess()
                        .Sequence()
                            .Log("索敌中")
                            .WaitTime(2)
                            .RandomProbability(0.5f,0)//50%索敌成功
                        .End()
                    .End()
                    .Log("索敌成功，走向怪物")
                    .WaitTime(2)
                    .ParallelSelector()
                        .Inverter("发动攻击")
                            .RepeatUntilFailure()
                                .EventBreak("StopAttack")
                                    .Selector()
                                        .Sequence()
                                            .RandomProbability(0.1f,0)//10%放技能
                                            .Log("放技能")
                                            .WaitTime(2)
                                        .End()
                                        .Sequence()
                                            .Log("普攻")
                                            .WaitTime(2)
                                        .End()
                                    .End()
                                .End()
                            .End()
                        .End()
                        .Sequence("检查击飞")
                            .WaitEvent("Airborne")
                            .SendEvent("StopAttack")
                            .Log("被击飞")
                            .WaitTime(2)
                        .End()
                    .End()
                .End()
                .Build();
            // @formatter:on

            tree.Start(true);
        }

        private void Update()
        {
            if (isAirborne)
            {
                _sharedBlackboard.SendEvent("Airborne");
                isAirborne = false;
            }

            tree.Tick();
        }
    }
}