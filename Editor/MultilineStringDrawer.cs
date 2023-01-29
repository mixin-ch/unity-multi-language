using UnityEditor;
using UnityEngine;

namespace Mixin.MultiLanguage
{
    /// <summary>
    /// The custom drawer of the MultilineString.
    /// </summary>
    [CustomPropertyDrawer(typeof(MultilineString))]
    public class MultilineStringDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 100f;
            SerializedProperty variableProp = property.FindPropertyRelative("Text");
            EditorGUI.PropertyField(position, variableProp, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 100f;
        }
    }
}

