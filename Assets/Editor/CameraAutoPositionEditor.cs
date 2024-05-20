using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraAutoPosition))]
public class CameraAutoPositionEditor : Editor
{
    private CameraAutoPosition cameraAutoPosition;

    private void OnEnable()
    {
        cameraAutoPosition = target as CameraAutoPosition;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        if (GUILayout.Button("Position Camera"))
        {
            cameraAutoPosition.PositionCamera();
        }
    }
}
