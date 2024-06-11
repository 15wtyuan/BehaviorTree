using UnityEditor;
using UnityEngine;

namespace BT.Editors
{
    /// <summary>
    /// Determine if this is a package of the Unity Editor since Unity has no API to determine this
    /// </summary>
    public static class AssetPath
    {
        private const string PathIcon = "Assets/BehaviorTree/Editor/Icons";

        private static string _iconPath;

        public static string IconPath
        {
            get
            {
                if (_iconPath != null) return _iconPath;

                if (AssetDatabase.IsValidFolder(PathIcon))
                {
                    _iconPath = PathIcon;
                    return _iconPath;
                }

                Debug.LogError($"Icon root could not be found {PathIcon}");

                return "";
            }
        }
    }
}