using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineManager))]
public class LineManagerEditor : Editor {

    LineManager lineManagerScript;
    SerializedObject GetTarget;

    private void OnEnable()
    {
        lineManagerScript = (LineManager)target;
        GetTarget = new SerializedObject(lineManagerScript);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        /*
        using (var horizontalScope = new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Rebuild All Lines"))//, GUILayout.MaxWidth(60), GUILayout.MinHeight(45)))
            {
                lineManagerScript.BuildLines(PropertyManager.Instance.Propriedades);
            }
        }
        */
    }
}
