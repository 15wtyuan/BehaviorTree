using UnityEngine;

namespace BehaviorTree.Editors {
    public class GraphNodeOptions {
        public int VerticalConnectorBottomHeight { get; set; }
        public int HorizontalConnectorHeight { get; set; }
        public int VerticalConnectorTopHeight { get; set; }
        public Vector2 Size { get; set; } = new Vector2(50, 100);
    }
}