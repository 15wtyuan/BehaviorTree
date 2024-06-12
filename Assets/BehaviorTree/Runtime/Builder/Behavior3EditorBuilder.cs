using System;
using System.Collections.Generic;
using System.Diagnostics;
using MiniJSON;

namespace BT.Runtime
{
    /// <summary>
    /// https://github.com/behavior3/behavior3editor
    /// from json
    /// </summary>
    public partial class BehaviorTreeBuilder
    {
        public BehaviorTreeBuilder AddTreeFromJson(string jsonString)
        {
            var dict = Json.Deserialize(jsonString) as Dictionary<string, object>;
            Debug.Assert(dict != null, nameof(dict) + " != null");
            return AddTreeFromJson(dict);
        }

        public BehaviorTreeBuilder AddTreeFromJson(Dictionary<string, object> dict)
        {
            //添加黑板属性
            var properties = dict["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");
            foreach (var pair in properties)
            {
                var keys = pair.Key.Split('_');
                var variable = ParseVariables(keys[0], pair.Value);
                _selfBlackboard.Set(keys[1], variable);
            }

            var nodes = dict["nodes"] as Dictionary<string, object>;

            var firstNodeId = dict["root"] as string;
            var node = ParseNodeByJson(firstNodeId, nodes);
            return AddNode(node);
        }

        private SharedVariable ParseVariables(string type, object value)
        {
            SharedVariable result;
            switch (type)
            {
                case "Int":
                case "int":
                {
                    var intValue = MiniJsonHelper.ParseInt(value);
                    SharedInt sharedInt = intValue;
                    result = sharedInt;
                }
                    break;
                case "Float":
                case "float":
                {
                    var floatValue = MiniJsonHelper.ParseFloat(value);
                    SharedFloat sharedFloat = floatValue;
                    result = sharedFloat;
                }
                    break;
                case "Bool":
                case "bool":
                {
                    var boolValue = MiniJsonHelper.ParseBool(value);
                    SharedBool sharedBool = boolValue;
                    result = sharedBool;
                }
                    break;
                case "String":
                case "string":
                {
                    var stringValue = MiniJsonHelper.ParseString(value);
                    SharedString sharedString = stringValue;
                    result = sharedString;
                }
                    break;
                default:
                {
                    throw new KeyNotFoundException($"type {type} NotFound");
                }
            }

            return result;
        }

        private TaskBase ParseNodeByJson(string id, Dictionary<string, object> nodes)
        {
            var curNode = nodes[id] as Dictionary<string, object>;
            Debug.Assert(curNode != null, nameof(curNode) + " != null");
            var name = (string)curNode["name"];
            var task = CreateNodeInstance(name);
            task.BuildFromJson(curNode);

            if (curNode.TryGetValue("children", out var childrenObj))
            {
                var children = (List<object>)childrenObj;
                var parent = (TaskParentBase)task;
                foreach (var child in children)
                {
                    var childId = (string)child;
                    var childTask = ParseNodeByJson(childId, nodes);
                    _tree.AddNode(parent, childTask);
                }
            }

            if (curNode.TryGetValue("child", out var childObj))
            {
                var parent = (TaskParentBase)task;
                var childId = (string)childObj;
                var childTask = ParseNodeByJson(childId, nodes);
                _tree.AddNode(parent, childTask);
            }

            return (TaskBase)task;
        }

        private static IJsonDeserializer CreateNodeInstance(string fullName)
        {
            if (!fullName.Contains("."))
            {
                fullName = $"BT.Runtime.{fullName}";
            }

            var o = Type.GetType(fullName);
            Debug.Assert(o != null, nameof(o) + " != null");
            var obj = Activator.CreateInstance(o, true);
            return (IJsonDeserializer)obj;
        }
    }
}