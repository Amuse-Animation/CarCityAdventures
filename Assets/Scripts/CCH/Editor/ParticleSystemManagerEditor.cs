#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ParticleSystemManager))]
public class ParticleSystemManagerEditor : Editor
{
    SerializedProperty _particleStartColor;
    SerializedProperty _particleAngleArc;

    public void OnEnable()
    {
        _particleStartColor = serializedObject.FindProperty("_particleStartColor");
        _particleAngleArc = serializedObject.FindProperty("_particleAngleArc");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ParticleSystemManager _partSystemManager = (ParticleSystemManager)target;

        EditorGUILayout.PropertyField(_particleAngleArc);

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
