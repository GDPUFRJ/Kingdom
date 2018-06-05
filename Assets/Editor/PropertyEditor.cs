
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


[CustomEditor(typeof(Property))]
[CanEditMultipleObjects]
public class PropertyEditor : Editor {

    enum displayFieldType { DisplayAsAutomaticFields, DisplayAsCustomizableGUIFields }
    displayFieldType DisplayFieldType;

    Property propertyScript;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;


    private void OnEnable()
    {
        propertyScript = (Property)target;

        GetTarget = new SerializedObject(propertyScript);
        ThisList = GetTarget.FindProperty("Neighbors"); // Find the List in our script and create a refrence of it


    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        propertyScript = (Property)target;

        GetTarget = new SerializedObject(propertyScript);
        ThisList = GetTarget.FindProperty("Neighbors"); // Find the List in our script and create a refrence of it
        //Update our list

        GetTarget.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Neighbors");
        EditorGUILayout.Space();

        int i = 0;
        foreach (Property p in propertyScript.Neighbors)
        {
            propertyScript.Neighbors[i] = (Property)EditorGUILayout.ObjectField("Neighbor " + i, p, typeof(Property), true);
            using (var v = new EditorGUILayout.VerticalScope("Button"))
            {
                if (GUI.Button(v.rect, GUIContent.none))
                {
                    //do what button should do
                    p.Neighbors.Remove(propertyScript);
                    propertyScript.Neighbors.RemoveAt(i);
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
                GUILayout.Label("Remove");
            }
            i++;
        }

        EditorGUILayout.Space();

        using (var v = new EditorGUILayout.VerticalScope("Button"))
        {
            if (GUI.Button(v.rect, GUIContent.none))
            {
                //do what button should do
                propertyScript.Neighbors.Add(null);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            GUILayout.Label("Add Property Slot");
        }

        EditorGUILayout.Space();

        using (var v = new EditorGUILayout.VerticalScope("Button"))
        {
            if (GUI.Button(v.rect, GUIContent.none))
            {
                //do what button should do
                propertyScript.Neighbors.Clear();
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            GUILayout.Label("Clear");
        }

        GetTarget.ApplyModifiedProperties();

    }
}
