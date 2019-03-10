using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(KHappeningManager))]
public class KHappeningManagerEditor : Editor
{
    KHappeningManager KHappeningManagerScript;
    SerializedObject GetTarget;

    List<KHappening> ToRemove = new List<KHappening>();
    List<KAnswer> AnswerToRemove = new List<KAnswer>();

    private void OnEnable()
    {
        KHappeningManagerScript = (KHappeningManager)target;
        GetTarget = new SerializedObject(KHappeningManagerScript);
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        GetTarget.Update();

        RemovePendent();

        EditorGUILayout.LabelField("Probabilities:", EditorStyles.boldLabel);

        KHappeningManagerScript.HappenningChance = EditorGUILayout.FloatField("Chance de Acontecimento", KHappeningManagerScript.HappenningChance);
        KHappeningManagerScript.ChanceMuitoComum = EditorGUILayout.FloatField("Chance Muito Comum", KHappeningManagerScript.ChanceMuitoComum);
        KHappeningManagerScript.ChanceComum = EditorGUILayout.FloatField("Chance Comum", KHappeningManagerScript.ChanceComum);
        KHappeningManagerScript.ChanceNormal = EditorGUILayout.FloatField("Chance Normal", KHappeningManagerScript.ChanceNormal);
        KHappeningManagerScript.ChanceRaro = EditorGUILayout.FloatField("Chance Raro", KHappeningManagerScript.ChanceRaro);
        KHappeningManagerScript.ChanceMuitoRaro = EditorGUILayout.FloatField("Chance Muito Raro", KHappeningManagerScript.ChanceMuitoRaro);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(KHappeningManagerScript.KHappenings.Count + " Possible Happenings", EditorStyles.boldLabel);

        foreach (KHappening khpp in KHappeningManagerScript.KHappenings)
        {
            khpp.showInInspector = EditorGUILayout.Foldout(khpp.showInInspector, "Happening " + khpp.PortugueseName);
            if (khpp.showInInspector)
            {
                khpp.PortugueseName = EditorGUILayout.TextField(new GUIContent("Portuguese Name"), khpp.PortugueseName);
                khpp.EnglishName = EditorGUILayout.TextField(new GUIContent("English Name"), khpp.EnglishName);

                if (GUILayout.Button("Delete", GUILayout.MaxWidth(50)) &&
                    EditorUtility.DisplayDialog("Confirm Deletion",
                    "Are you sure to delete " + khpp.PortugueseName + " event? This action cannot be undone.",
                    "Yes", "No"))
                    ToRemove.Add(khpp);

                EditorGUI.indentLevel++;
                khpp.showDescription = EditorGUILayout.Foldout(khpp.showDescription, "Description");
                if (khpp.showDescription)
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.PrefixLabel("Portuguese");
                    EditorStyles.textArea.wordWrap = true;
                    khpp.PortugueseDescription = EditorGUILayout.TextArea(khpp.PortugueseDescription);

                    EditorGUILayout.PrefixLabel("English");
                    EditorStyles.textArea.wordWrap = true;
                    khpp.EnglishDescription = EditorGUILayout.TextArea(khpp.EnglishDescription);
                }
                EditorGUI.indentLevel--;

                khpp.PortugueseQuestion = EditorGUILayout.TextField(new GUIContent("Portuguese Question"), khpp.PortugueseQuestion);
                khpp.EnglishQuestion = EditorGUILayout.TextField(new GUIContent("English Question"), khpp.EnglishQuestion);
                khpp.chance = (Chance)EditorGUILayout.EnumPopup("Chance", khpp.chance);

                EditorGUI.indentLevel++;

                khpp.showAnswers = EditorGUILayout.Foldout(khpp.showAnswers, "Answers List");
                if (khpp.showAnswers)
                {
                    EditorGUI.indentLevel++;
                    foreach (KAnswer kans in khpp.Answers)
                    {

                        kans.showInInspector = EditorGUILayout.Foldout(kans.showInInspector, "Answer: '" + kans.portugueseAnswer + "'");
                        if (kans.showInInspector)
                        {
                            EditorGUILayout.BeginHorizontal();
                            kans.portugueseAnswer = EditorGUILayout.TextField(new GUIContent("Portuguese Text"), kans.portugueseAnswer);
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            kans.englishAnswer = EditorGUILayout.TextField(new GUIContent("English Text"), kans.englishAnswer);
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            if (GUILayout.Button("Delete", GUILayout.MaxWidth(50)) &&
                                                    EditorUtility.DisplayDialog("Confirm Deletion",
                                                    "Are you sure to delete Answer " + kans.portugueseAnswer + "from " + khpp.PortugueseName + " event? This action cannot be undone.",
                                                    "Yes", "No") )
                                AnswerToRemove.Add(kans);

                            EditorGUILayout.EndHorizontal();

                            //----------------
                            List<string> availableEvents = new List<string>();
                            foreach(KEvent kevt in KEventManager.Instance.KEvents)
                            {
                                availableEvents.Add(kevt.InternalName);
                            }

                            // Set the choice index to the previously selected index
                            int _choiceIndex = Array.IndexOf(KEventManager.Instance.KEvents.ToArray(), kans.answerEvent);

                            // If the choice is not in the array then the _choiceIndex will be -1 so set back to 0
                            if (_choiceIndex < 0)
                                _choiceIndex = 0;

                            _choiceIndex = EditorGUILayout.Popup(new GUIContent("Event"),_choiceIndex, availableEvents.ToArray());
                            kans.answerEvent = KEventManager.Instance.KEvents[_choiceIndex];

                            kans.intensity = (Intensity)EditorGUILayout.EnumPopup("Intensity", kans.intensity);
                        }
                    }

                    AnswerRemovePendent(khpp);

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();

                    GUILayout.Button("", new GUIStyle());

                    if (GUILayout.Button("Add Answer", GUILayout.MinHeight(20), GUILayout.MaxWidth(100)))
                    {
                        khpp.Answers.Add(CreateInstance<KAnswer>());
                    }

                    GUILayout.Button("", new GUIStyle());

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUI.indentLevel--;

                }
                EditorGUI.indentLevel--;


            }
        }



        // EditorGUILayout.LabelField("//TODO IMPLEMENTAR LISTA DE OPCOES COM OS NOMES DOS EVENTOS DISPONIVEIS", EditorStyles.boldLabel);



        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Button("", new GUIStyle());

        if (GUILayout.Button("Add Happening", GUILayout.MinHeight(30)))
        {
            KHappeningManagerScript.KHappenings.Add(CreateInstance<KHappening>());
        }

        GUILayout.Button("", new GUIStyle());

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

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

    private bool AnswerRemovePendent(KHappening khpp)
    {
        if (AnswerToRemove.Count > 0)
        {
            foreach (KAnswer kans in AnswerToRemove)
            {
                khpp.Answers.Remove(kans);
            }
            AnswerToRemove.Clear();

            return true;
        }
        return false;
    }

}
