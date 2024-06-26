using System.Collections.Generic;
using BT.Runtime;
using UnityEngine;

namespace BT.Editors
{
    public class GraphNode
    {
        private IGraphNodePrinter _printer;

        public float ContainerHeight => Size.y
                                        + VerticalConnectorBottomHeight
                                        + HorizontalConnectorHeight
                                        + VerticalConnectorTopHeight;

        public Vector2 Position { get; private set; }
        public List<GraphNode> Children { get; } = new List<GraphNode>();
        public Vector2 Size { get; }
        public TaskBase Task { get; }
        public int VerticalConnectorBottomHeight { get; }
        public int VerticalConnectorTopHeight { get; }
        public int HorizontalConnectorHeight { get; }

        public GraphNode(TaskBase task, IGraphNodePrinter printer, GraphNodeOptions options)
        {
            _printer = printer;
            Task = task;

            Size = options.Size;
            VerticalConnectorBottomHeight = options.VerticalConnectorBottomHeight;
            VerticalConnectorTopHeight = options.VerticalConnectorTopHeight;
            HorizontalConnectorHeight = options.HorizontalConnectorHeight;

            var taskParentBase = task as TaskParentBase;
            if (taskParentBase == null) return;
            foreach (var child in taskParentBase.Children)
            {
                Children.Add(new GraphNode(child, printer, options));
            }
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;

            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var childPos = new Vector2(position.x, position.y + ContainerHeight);

                // Center the child, then align it to the expected position
                childPos.x += Size.x / 2 + Size.x * i;

                // Shift the child as if it were in a container so it lines up properly
                childPos.x -= Size.x * (Children.Count / 2f);

                child.SetPosition(childPos);
            }
        }

        public void Print()
        {
            _printer.Print(this);

            foreach (var child in Children)
            {
                child.Print();
            }
        }
    }
}