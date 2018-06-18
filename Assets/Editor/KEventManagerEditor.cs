using System.Collections;
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
            kevt.showInInspector = EditorGUILayout.Foldout(kevt.showInInspector, "Event " + kevt.Name);
            if (kevt.showInInspector)
            {
                EditorGUILayout.BeginHorizontal();

                kevt.Name = EditorGUILayout.TextField(new GUIContent("Name"), kevt.Name);

                if (GUILayout.Button("Delete", GUILayout.MaxWidth(50)) &&
                    EditorUtility.DisplayDialog("Confirm Deletion",
                                                "Are you sure to delete " + kevt.Name + " event? This action cannot be undone.",
                                                "Yes", "No")
                )
                    ToRemove.Add(kevt);


                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginVertical();

                kevt.Duration = EditorGUILayout.IntSlider(new GUIContent("Duration Days"), kevt.Duration, 0, +30);

                kevt.intensity = (KEvent.Intensity)EditorGUILayout.EnumPopup("Intensity", kevt.intensity);
                kevt.chance = (KEvent.Chance)EditorGUILayout.EnumPopup("Chance", kevt.chance);
                kevt.mode = (KEvent.Mode)EditorGUILayout.EnumPopup("Mode", kevt.mode);
                kevt.battle = (KEvent.Battle)EditorGUILayout.EnumPopup("Battle", kevt.battle);


                switch (kevt.intensity)
                {
                    case KEvent.Intensity.light:
                        if (kevt.mode == KEvent.Mode.UsePercentage)
                        {
                            kevt.PercentGoldLight = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.PercentGoldLight, -100, +100);
                            kevt.PercentFoodLight = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.PercentFoodLight, -100, +100);
                            kevt.PercentBuildingLight = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.PercentBuildingLight, -100, +100);
                            kevt.PercentPeopleLight = EditorGUILayout.IntSlider(new GUIContent("Population Growth"), kevt.PercentPeopleLight, -100, +100);
                        }
                        else
                        {
                            kevt.AbsoluteGoldLight = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.AbsoluteGoldLight, -1000, +1000);
                            kevt.AbsoluteFoodLight = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.AbsoluteFoodLight, -1000, +1000);
                            kevt.AbsoluteBuildingLight = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.AbsoluteBuildingLight, -1000, +1000);
                        }
                        break;
                    case KEvent.Intensity.medium:
                        if (kevt.mode == KEvent.Mode.UsePercentage)
                        {
                            kevt.PercentGoldMedium = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.PercentGoldMedium, -100, +100);
                            kevt.PercentFoodMedium = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.PercentFoodMedium, -100, +100);
                            kevt.PercentBuildingMedium = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.PercentBuildingMedium, -100, +100);
                            kevt.PercentPeopleMedium = EditorGUILayout.IntSlider(new GUIContent("Population Growth"), kevt.PercentPeopleMedium, -100, +100);
                        }
                        else
                        {
                            kevt.AbsoluteGoldMedium = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.AbsoluteGoldMedium, -1000, +1000);
                            kevt.AbsoluteFoodMedium = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.AbsoluteFoodMedium, -1000, +1000);
                            kevt.AbsoluteBuildingMedium = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.AbsoluteBuildingMedium, -1000, +1000);
                        }
                        break;
                    case KEvent.Intensity.heavy:
                        if (kevt.mode == KEvent.Mode.UsePercentage)
                        {
                            kevt.PercentGoldHeavy = EditorGUILayout.IntSlider(new GUIContent("Gold"), kevt.PercentGoldHeavy, -100, +100);
                            kevt.PercentFoodHeavy = EditorGUILayout.IntSlider(new GUIContent("Food"), kevt.PercentFoodHeavy, -100, +100);
                            kevt.PercentBuildingHeavy = EditorGUILayout.IntSlider(new GUIContent("Building"), kevt.PercentBuildingHeavy, -100, +100);
                            kevt.PercentPeopleHeavy = EditorGUILayout.IntSlider(new GUIContent("Population Growth"), kevt.PercentPeopleHeavy, -100, +100);
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

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PrefixLabel("Description");

                EditorStyles.textArea.wordWrap = true;
                
                
                kevt.Description = EditorGUILayout.TextArea(kevt.Description);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

            //if(GUILayout.Button("Add Property Slot")) {propertyScript.Neighbors.Add(null);}

            if (GUILayout.Button("Add Event", GUILayout.MinHeight(30)))
            {
                KEventManagerScript.KEvents.Add(CreateInstance<KEvent>());
            }
        EditorGUILayout.EndHorizontal();

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
