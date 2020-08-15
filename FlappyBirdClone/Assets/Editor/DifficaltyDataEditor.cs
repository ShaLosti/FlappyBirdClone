using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

[CustomEditor(typeof(DifficultData))]
public class DifficaltyDataEditor : Editor
{
    private DifficultData data;

    private void Awake()
    {
        data = (DifficultData)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("RemoveAll"))
        {
            data.ClearData();
        }
        if (GUILayout.Button("Remove"))
        {
            data.RemoveDifficulty();
        }
        if (GUILayout.Button("Add"))
        {
            data.AddDifficulty();
        }
        if (GUILayout.Button("<="))
        {
            data.GetPrev();
        }
        if (GUILayout.Button(">="))
        {
            data.GetNext();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
