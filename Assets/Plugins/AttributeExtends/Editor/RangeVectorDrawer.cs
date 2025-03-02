using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeVectorAttribute))]
public sealed class RangeVectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // First get the attribute since it contains the range for the slider
        var range = attribute as RangeVectorAttribute;
        EditorGUI.BeginProperty(position, label, property);
        switch (property.propertyType)
        {
            
            case SerializedPropertyType.Vector3:
                OnVector3(range, position, property, label);
                break;
            case SerializedPropertyType.Vector2:
                OnVector2(range, position, property, label);
                break;
        }
        EditorGUI.EndProperty();
    }

    private void OnVector2(RangeVectorAttribute range, Rect position, SerializedProperty property, GUIContent label)
    {
        Vector2 value = property.vector2Value;
        value = EditorGUI.Vector2Field(position, label, value);
        value = ClampValue(range, value);

        property.vector2Value = value;
    }

    private void OnVector3(RangeVectorAttribute range, Rect position, SerializedProperty property, GUIContent label)
    {
        Vector3 value = property.vector3Value;
        value = EditorGUI.Vector3Field(position, label, value);
        value = ClampValue(range, value);
        property.vector3Value = value;
    }

    private Vector3 ClampValue(RangeVectorAttribute range, Vector3 value)
    {
        value.x = Mathf.Clamp(value.x, range.min.x, range.max.x);
        value.y = Mathf.Clamp(value.y, range.min.y, range.max.y);
        value.z = Mathf.Clamp(value.z, range.min.z, range.max.z);
        return value;
    }
}
