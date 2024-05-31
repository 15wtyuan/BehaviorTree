using UnityEngine;

namespace BehaviorTree.Runtime
{
    public class RandomProbability : ConditionBase
    {
        public float SuccessProbability;
        public int Seed;

        protected override bool GetCondition()
        {
            var oldState = Random.state;

            if (Seed != 0)
            {
                Random.InitState(Seed);
            }

            var randomValue = Random.value;

            if (Seed != 0)
            {
                Random.state = oldState;
            }

            return randomValue < SuccessProbability;
        }
    }
}