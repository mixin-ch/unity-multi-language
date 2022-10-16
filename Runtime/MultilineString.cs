using System;
using UnityEditor;
using UnityEngine;

namespace Mixin.Language
{
    /// <summary>
    /// This class contains a normal Text property. <br></br>
    /// But the inspector shows a custom drawer which has 100f height and 100% width.
    /// </summary>
    [Serializable]
    public class MultilineString
    {
        public string Text;
    }



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

