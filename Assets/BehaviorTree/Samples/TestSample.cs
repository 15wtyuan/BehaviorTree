using System.Collections;
using System.Collections.Generic;
using BT.Runtime;
using MiniJSON;
using UnityEngine;

namespace BT.Samples
{
    public class TestSample : MonoBehaviour
    {
        public bool isAirborne;
        [SerializeField] private BehaviorTree treeA;
        [SerializeField] private BehaviorTree treeB;

        private Blackboard _sharedBlackboardA;
        private Blackboard _sharedBlackboardB;

        private List<BehaviorTree> _trees = new();

        private void Awake()
        {
            //英雄：索敌，有敌人，先靠近敌人，再发动攻击，攻击的同时如果有技能就释放技能。如果被击飞了，就从头再来
            _sharedBlackboardA = new Blackboard();
            _sharedBlackboardB = new Blackboard();
            
            // @formatter:off
            treeA = new BehaviorTreeBuilder(_sharedBlackboardA)
                .Sequence()
                    .RepeatUntilSuccess()
                        .Sequence()
                            .Log("索敌中")
                            .Wait(2000)
                            .RandomProbability(0.5f,0)//50%索敌成功
                        .End()
                    .End()
                    .Log("索敌成功，走向怪物")
                    .Wait(2000)
                    .ParallelSelector()
                        .Inverter("发动攻击")
                            .RepeatUntilFailure()
                                .EventBreak("StopAttack")
                                    .Selector()
                                        .Sequence()
                                            .RandomProbability(0.1f,0)//10%放技能
                                            .Log("放技能")
                                            .Wait(2000)
                                        .End()
                                        .Sequence()
                                            .Log("普攻")
                                            .Wait(2000)
                                        .End()
                                    .End()
                                .End()
                            .End()
                        .End()
                        .Sequence("检查击飞")
                            .WaitEvent("Airborne")
                            .SendEvent("StopAttack")
                            .Log("被击飞")
                            .Wait(2000)
                        .End()
                    .End()
                .End()
                .Build();
            // @formatter:on
            // treeA.Start(true);


            const string jsonFilePath = "JsonSamples";
            var jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
            var jsonFileContent = jsonTextAsset.text;
            treeB = new BehaviorTreeBuilder(_sharedBlackboardB)
                .AddTreeFromJsonReader(new Behavior3EditorCnJsonReader(jsonFileContent))
                .Build();
            treeB.Start(true);

            // StartCoroutine(PerformanceTesting());
        }

        private void Update()
        {
            if (isAirborne)
            {
                // _sharedBlackboardA.SendEvent("Airborne");
                _sharedBlackboardB.SendEvent("Airborne");
                isAirborne = false;
            }

            // treeA.Tick();
            treeB.Tick();

            // foreach (var tree in _trees)
            // {
            //     tree.Tick();
            // }
        }

        private IEnumerator PerformanceTesting()
        {
            yield return new WaitForSeconds(2f);
            const string jsonFilePath = "JsonSamples";
            var jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
            var reader = new Behavior3EditorJsonReader(jsonTextAsset.text);

            for (int i = 0; i < 100; i++)
            {
                var tree = new BehaviorTreeBuilder(new Blackboard())
                    .AddTreeFromJsonReader(reader)
                    .Build();
                tree.Start(true);
                _trees.Add(tree);
            }
        }
    }
}