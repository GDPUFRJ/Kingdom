using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(KHappeningManager))]
public class KHappeningManagerEditor : Editor {


    KHappeningManager KHappeningManagerScript;
    SerializedObject GetTarget;

    List<KHappening> ToRemove = new List<KHappening>();

    private void OnEnable()
    {
        KHappeningManagerScript = (KHappeningManager)target;
        GetTarget = new SerializedObject(KHappeningManagerScript);
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        RemovePendent();

        EditorGUILayout.LabelField(KHappeningManagerScript.KHappenings.Count + " Possible Events", EditorStyles.boldLabel);

        foreach (KHappening khpp in KHappeningManagerScript.KHappenings)
        {
            khpp.showInInspector = EditorGUILayout.Foldout(khpp.showInInspector, "Event " + khpp.Name);
            if (khpp.showInInspector)
            {
                EditorGUILayout.BeginHorizontal();

                khpp.Name = EditorGUILayout.TextField(new GUIContent("Name"), khpp.Name);

                if (GUILayout.Button("Delete", GUILayout.MaxWidth(50)) &&
                    EditorUtility.DisplayDialog("Confirm Deletion",
                                                "Are you sure to delete " + khpp.Name + " event? This action cannot be undone.",
                                                "Yes", "No")
                )
                    ToRemove.Add(khpp);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginVertical();
                khpp.Question = EditorGUILayout.TextField(new GUIContent("Question"), khpp.Question);
                khpp.chance = (KHappening.Chance)EditorGUILayout.EnumPopup("Chance", khpp.chance);

                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel("Description");

                EditorStyles.textArea.wordWrap = true;

                khpp.Description = EditorGUILayout.TextArea(khpp.Description);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.LabelField("//TODO IMPLEMENTAR LISTA DE OPCOES COM OS NOMES DOS EVENTOS DISPONIVEIS", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Event", GUILayout.MinHeight(30)))
        {
            KHappeningManagerScript.KHappenings.Add(CreateInstance<KHappening>());
        }
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(KHappeningManagerScript);
            EditorSceneManager.MarkSceneDirty(KHappeningManagerScript.gameObject.scene);
        }
        GetTarget.ApplyModifiedProperties();
    }

    private bool RemovePendent()
    {
        if (ToRemove.Count > 0)
        {
            foreach (KHappening khpp in ToRemove)
            {
                KHappeningManagerScript.KHappenings.Remove(khpp);
            }
            ToRemove.Clear();

            return true;
        }
        return false;
    }

}
