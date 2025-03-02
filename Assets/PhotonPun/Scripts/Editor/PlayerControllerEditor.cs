using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    SerializedProperty _rigidBody;
    SerializedProperty _maxGroundVelocity;
    SerializedProperty _minGroundVelocity;
    SerializedProperty _jumpForce;
    SerializedProperty _accelerationType;
    SerializedProperty _decelerationType;
    SerializedProperty _velocityMultiplier;
    SerializedProperty _decelerationMultiplier;
    SerializedProperty _rotateRoot;
    SerializedProperty _rotationMultiplier;
    SerializedProperty _canMoveWhileMidAir;
    SerializedProperty _feet;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("---Acceleration---", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_accelerationType);
        EditorGUILayout.PropertyField(_velocityMultiplier);
        EditorGUILayout.LabelField("---Deceleration---", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_decelerationType);
        switch (_decelerationType.enumValueIndex)
        {
            case 0:
                EditorGUILayout.PropertyField(_decelerationMultiplier);
                break;
        }
        EditorGUILayout.LabelField("---Rotation---", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_rotateRoot);
        EditorGUILayout.PropertyField(_rotationMultiplier);
        EditorGUILayout.LabelField("---Jump---", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_jumpForce);
        EditorGUILayout.LabelField("--Velocity Constraint--", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_minGroundVelocity);
        EditorGUILayout.PropertyField(_maxGroundVelocity);
        EditorGUILayout.LabelField("--Drag & Drop References--", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_rigidBody);
        EditorGUILayout.PropertyField(_feet);
        EditorGUILayout.LabelField("--Settings--", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_canMoveWhileMidAir, new GUIContent("Can move while in mid-air"));
        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        _feet =     serializedObject.FindProperty(nameof(_feet));
        _rigidBody =    serializedObject.FindProperty(nameof(_rigidBody));
        _jumpForce =    serializedObject.FindProperty(nameof(_jumpForce));
        _rotateRoot =   serializedObject.FindProperty(nameof(_rotateRoot));
        _maxGroundVelocity =  serializedObject.FindProperty(nameof(_maxGroundVelocity));
        _minGroundVelocity =  serializedObject.FindProperty(nameof(_minGroundVelocity));
        _accelerationType =     serializedObject.FindProperty(nameof(_accelerationType));
        _decelerationType =     serializedObject.FindProperty(nameof(_decelerationType));
        _velocityMultiplier =   serializedObject.FindProperty(nameof(_velocityMultiplier));
        _canMoveWhileMidAir =   serializedObject.FindProperty(nameof(_canMoveWhileMidAir));
        _rotationMultiplier =   serializedObject.FindProperty(nameof(_rotationMultiplier));
        _decelerationMultiplier =   serializedObject.FindProperty(nameof(_decelerationMultiplier));
    }
}
