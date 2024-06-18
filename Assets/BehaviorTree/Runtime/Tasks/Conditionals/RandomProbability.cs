using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace BT.Runtime
{
    public static partial class BuilderExtensions
    {
        public static BehaviorTreeBuilder RandomProbability(this BehaviorTreeBuilder builder, string name,
            SharedFloat successProbability, SharedInt seed)
        {
            return builder.AddNode(new RandomProbability
            {
                Name = name,
                SuccessProbability = successProbability,
                Seed = seed
            });
        }

        public static BehaviorTreeBuilder RandomProbability(this BehaviorTreeBuilder builder,
            SharedFloat successProbability, SharedInt seed)
        {
            return builder.RandomProbability("Random Probability", successProbability, seed);
        }
    }

    public class RandomProbability : ConditionBase, IJsonDeserializer
    {
        public SharedFloat SuccessProbability;
        public SharedInt Seed;

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

        public void BuildFromJson(string title, Dictionary<string, object> properties)
        {
            Name = title;

            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("successProbability", out var value))
            {
                SuccessProbability = MiniJsonHelper.ParseFloat(value);
            }
            else if (properties.TryGetValue("b_successProbability", out value))
            {
                SuccessProbability = SelfBlackboard.Get<SharedFloat>(MiniJsonHelper.ParseString(value));
            }

            if (properties.TryGetValue("seed", out var value2))
            {
                Seed = MiniJsonHelper.ParseInt(value2);
            }
            else if (properties.TryGetValue("b_seed", out value2))
            {
                Seed = SelfBlackboard.Get<SharedInt>(MiniJsonHelper.ParseString(value2));
            }
        }
    }
}