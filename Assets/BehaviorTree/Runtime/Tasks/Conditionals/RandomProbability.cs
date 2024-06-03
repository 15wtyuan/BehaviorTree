using UnityEngine;

namespace BehaviorTree.Runtime
{
    public class RandomProbability : ConditionBase
    {
        public  SharedFloat SuccessProbability;
        public  SharedInt Seed;

        protected override bool GetCondition()
        {
            var oldState = Random.state;

            if (Seed.Value != 0)
            {
                Random.InitState(Seed.Value);
            }

            var randomValue = Random.value;

            if (Seed.Value != 0)
            {
                Random.state = oldState;
            }

            return randomValue < SuccessProbability.Value;
        }
    }
}