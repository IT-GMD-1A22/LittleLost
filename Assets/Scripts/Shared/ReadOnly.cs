using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Attribute that allows to set ReadOnly in inspector.
 *
 * Useful for values that are used only as visual inspection
 * Source: https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
 * JH
 */

public class ReadOnlyAttribute : PropertyAttribute
{
 
}
#if (UNITY_EDITOR) 
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
        GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
 
    public override void OnGUI(Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif