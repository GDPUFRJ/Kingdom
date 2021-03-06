﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(KEventManager))]
public class KEventManagerEditor : Editor
{
    KEventManager KEventManagerScript;
    SerializedObject GetTarget;

    List<KEvent> ToRemove = new List<KEvent>();

    private void OnEnable()
    {
        KEventManagerScript = (KEventManager)target;
        GetTarget = new SerializedObject(KEventManagerScript);
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        GetTarget.Update();

        RemovePendent();

        EditorGUILayout.LabelField(KEventManagerScript.KEvents.Count + " Possible Events", EditorStyles.boldLabel);

        foreach (KEvent kevt in KEventManagerScript.KEvents)
        {

            kevt.showInInspector = EditorGUILayout.Foldout(kevt.showInInspector, "Event " + kevt.InternalName);
            if (kevt.showInInspector)
            {

                    EditorGUILayout.BeginHorizontal();

                    kevt.InternalName = EditorGUILayout.TextField(new GUIContent("Internal Name"), kevt.InternalName);

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginVertical();

                    kevt.PortugueseExhibitionName = EditorGUILayout.TextField(new GUIContent("Portuguese Exhibition Name"), kevt.PortugueseExhibitionName);
                    kevt.EnglishExhibitionName = EditorGUILayout.TextField(new GUIContent("English Exhibition Name"), kevt.EnglishExhibitionName);

                    if (GUILayout.Button("Delete", GUILayout.MaxWidth(50)) &&
                        EditorUtility.DisplayDialog("Confirm Deletion",
                                                    "Are you sure to delete " + kevt.InternalName + " event? This action cannot be undone.",
                                                    "Yes", "No")
                    )
                        ToRemove.Add(kevt);


                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginVertical();

                    kevt.Duration = EditorGUILayout.IntSlider(new GUIContent("Duration Days"), kevt.Duration, 0, +30);  

                    kevt.ActiveIntensity = (Intensity)EditorGUILayout.EnumPopup("Intensity", kevt.ActiveIntensity);
                    kevt.chance = (Chance)EditorGUILayout.EnumPopup("Chance", kevt.chance);
                    kevt.mode = (Mode)EditorGUILayout.EnumPopup("Mode", kevt.mode);
                    kevt.battle = (Battle)EditorGUILayout.EnumPopup("Battle", kevt.battle);


                    switch (kevt.ActiveIntensity)
                    {
                        case Intensity.light:
                            if (kevt.mode == Mode.UsePercentage)
                            {
                                kevt.PercentGoldLight = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.PercentGoldLight, -100, +100);
                                kevt.PercentFoodLight = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.PercentFoodLight, -100, +100);
                                kevt.PercentBuildingLight = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.PercentBuildingLight, -100, +100);
                                kevt.PercentPeopleLight = EditorGUILayout.IntSlider(new GUIContent("Population Growth"), kevt.PercentPeopleLight, -100, +100);
                                kevt.PercentHappinessLight = EditorGUILayout.IntSlider(new GUIContent("Happiness"), kevt.PercentHappinessLight, -100, +100);
                            }
                            else
                            {
                                kevt.AbsoluteGoldLight = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.AbsoluteGoldLight, -1000, +1000);
                                kevt.AbsoluteFoodLight = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.AbsoluteFoodLight, -1000, +1000);
                                kevt.AbsoluteBuildingLight = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.AbsoluteBuildingLight, -1000, +1000);
                            }
                            break;
                        case Intensity.medium:
                            if (kevt.mode == Mode.UsePercentage)
                            {
                                kevt.PercentGoldMedium = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.PercentGoldMedium, -100, +100);
                                kevt.PercentFoodMedium = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.PercentFoodMedium, -100, +100);
                                kevt.PercentBuildingMedium = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.PercentBuildingMedium, -100, +100);
                                kevt.PercentPeopleMedium = EditorGUILayout.IntSlider(new GUIContent("Population Growth"), kevt.PercentPeopleMedium, -100, +100);
                                kevt.PercentHappinessMedium = EditorGUILayout.IntSlider(new GUIContent("Happiness"), kevt.PercentHappinessMedium, -100, +100);
                            }
                            else
                            {
                                kevt.AbsoluteGoldMedium = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.AbsoluteGoldMedium, -1000, +1000);
                                kevt.AbsoluteFoodMedium = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.AbsoluteFoodMedium, -1000, +1000);
                                kevt.AbsoluteBuildingMedium = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.AbsoluteBuildingMedium, -1000, +1000);
                            }
                            break;
                        case Intensity.heavy:
                            if (kevt.mode == Mode.UsePercentage)
                            {
                                kevt.PercentGoldHeavy = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.PercentGoldHeavy, -100, +100);
                                kevt.PercentFoodHeavy = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.PercentFoodHeavy, -100, +100);
                                kevt.PercentBuildingHeavy = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.PercentBuildingHeavy, -100, +100);
                                kevt.PercentPeopleHeavy = EditorGUILayout.IntSlider(new GUIContent("Population Growth"), kevt.PercentPeopleHeavy, -100, +100);
                                kevt.PercentHappinessHeavy = EditorGUILayout.IntSlider(new GUIContent("Happiness"), kevt.PercentHappinessHeavy, -100, +100);
                            }
                            else
                            {
                                kevt.AbsoluteGoldHeavy = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.AbsoluteGoldHeavy, -1000, +1000);
                                kevt.AbsoluteFoodHeavy = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.AbsoluteFoodHeavy, -1000, +1000);
                                kevt.AbsoluteBuildingHeavy = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.AbsoluteBuildingHeavy, -1000, +1000);
                            }
                            break;
                    }
                    EditorGUILayout.EndVertical();

                    //EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.PrefixLabel("Portuguese Description");
                    EditorStyles.textField.wordWrap = true;
                    EditorStyles.textArea.wordWrap = true;
                    kevt.PortugueseDescription = EditorGUILayout.TextArea(kevt.PortugueseDescription);
                    EditorGUILayout.PrefixLabel("English Description");
                    kevt.EnglishDescription = EditorGUILayout.TextArea(kevt.EnglishDescription);
                    //EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
            }
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Button("", new GUIStyle());

        if (GUILayout.Button("Add Event", GUILayout.MinHeight(30)))
        {
            KEventManagerScript.KEvents.Add(CreateInstance<KEvent>());
        }

        GUILayout.Button("", new GUIStyle());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(KEventManagerScript);
            EditorSceneManager.MarkSceneDirty(KEventManagerScript.gameObject.scene);
        }
        GetTarget.ApplyModifiedProperties();
    }

    private bool RemovePendent()
    {
        if (ToRemove.Count > 0)
        {
            foreach (KEvent kevt in ToRemove)
            {
                KEventManagerScript.KEvents.Remove(kevt);
            }
            ToRemove.Clear();

            return true;
        }
        return false;
    }


}
