using System.IO;
using UnityEditor;
using UnityEngine;

namespace BT.Editors
{
    public class TextureLoader
    {
        public Texture2D Texture { get; }

        public TextureLoader(string spriteName)
        {
            Texture = AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(AssetPath.IconPath, spriteName));
        }

        public void Paint(Rect rect, Color color)
        {
            var oldColor = GUI.color;
            GUI.color = color;

            if (Texture == null) return;
            GUI.Label(rect, Texture);

            GUI.color = oldColor;
        }
    }
}