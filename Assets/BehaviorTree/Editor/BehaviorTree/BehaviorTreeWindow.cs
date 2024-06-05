using BehaviorTree.Runtime;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree.Editors
{
    public class BehaviorTreeWindow : EditorWindow
    {
        private BehaviorTreePrinter _printer;
        private string _name;
        private IBehaviorTree _tree;
        public static void ShowTree(IBehaviorTree tree, string name)
        {
            var window = GetWindow<BehaviorTreeWindow>(false);
            window.titleContent = new GUIContent($"Behavior Tree: {name}");
            window.SetTree(tree, name);
        }

        private void SetTree(IBehaviorTree tree, string name)
        {
            _printer?.Unbind();
            _printer = new BehaviorTreePrinter(tree, position.size);
            _name = name;
            _tree = tree;
        }

        private void OnGUI()
        {
            if (!Application.isPlaying)
            {
                ClearView();
            }

            GUILayout.Label($"Behavior Tree: {_name}", EditorStyles.boldLabel);
            if (_tree != null)
            {
                GUILayout.Label($"Blackboard:\n{_tree.GetSharedBlackboardPrint()}", EditorStyles.boldLabel);
            }
            _printer?.Print(position.size);
        }

        private void ClearView()
        {
            _name = null;
            _printer = null;
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                Repaint();
            }
        }
    }
}