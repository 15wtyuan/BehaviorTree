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
    public class Behavior3EditorJsonReader : IJsonReader
    {
        private Dictionary<string, object> _dict;

        public Behavior3EditorJsonReader(string jsonString)
        {
            var dict = Json.Deserialize(jsonString) as Dictionary<string, object>;
            Debug.Assert(dict != null, nameof(dict) + " != null");

            _dict = dict;
        }

        public Behavior3EditorJsonReader(Dictionary<string, object> dict)
        {
            _dict = dict;
        }

        public void Add2Tree(BehaviorTreeBuilder builder)
        {
            //添加黑板属性
            var properties = _dict["properties"] as Dictionary<string, object>;
            Debug.Assert(properties != null, nameof(properties) + " != null");
            foreach (var pair in properties)
            {
                var keys = pair.Key.Split('_');
                var variable = ParseVariables(keys[0], pair.Value);
                builder.SelfBlackboard.Set(keys[1], variable);
            }

            var nodes = _dict["nodes"] as Dictionary<string, object>;

            var firstNodeId = _dict["root"] as string;
            var node = ParseNodeByJson(builder.GetCurTree(), firstNodeId, nodes);
            builder.AddNode(node);
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

        private TaskBase ParseNodeByJson(BehaviorTree tree, string id, Dictionary<string, object> nodes)
        {
            var curNode = nodes[id] as Dictionary<string, object>;
            Debug.Assert(curNode != null, nameof(curNode) + " != null");
            var name = (string)curNode["name"];
            var task = CreateNodeInstance(name);

            var title = (string)curNode["title"];
            var properties = curNode["properties"] as Dictionary<string, object>;
            task.BuildFromJson(title, properties);

            if (curNode.TryGetValue("children", out var childrenObj))
            {
                var children = (List<object>)childrenObj;
                var parent = (TaskParentBase)task;
                foreach (var child in children)
                {
                    var childId = (string)child;
                    var childTask = ParseNodeByJson(tree, childId, nodes);
                    tree.AddNode(parent, childTask);
                }
            }

            if (curNode.TryGetValue("child", out var childObj))
            {
                var parent = (TaskParentBase)task;
                var childId = (string)childObj;
                var childTask = ParseNodeByJson(tree, childId, nodes);
                tree.AddNode(parent, childTask);
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