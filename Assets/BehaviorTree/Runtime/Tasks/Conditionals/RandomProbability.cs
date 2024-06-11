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

        public void BuildFromJson(Dictionary<string, object> jsonData)
        {
            Name = (string)jsonData["title"];

            var properties = jsonData["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");

            if (properties.TryGetValue("successProbability", out var value))
            {
                var floatValue = MiniJsonHelper.ParseFloat(value);
                SuccessProbability = floatValue;
            }
            else if (properties.TryGetValue("b_successProbability", out value))
            {
                var strValue = MiniJsonHelper.ParseString(value);
                SuccessProbability = SharedBlackboard.Get<SharedFloat>(strValue);
            }

            if (properties.TryGetValue("seed", out var value2))
            {
                var intValue = MiniJsonHelper.ParseInt(value2);
                Seed = intValue;
            }
            else if (properties.TryGetValue("b_seed", out value2))
            {
                var strValue = MiniJsonHelper.ParseString(value2);
                Seed = SharedBlackboard.Get<SharedInt>(strValue);
            }
        }
    }
}