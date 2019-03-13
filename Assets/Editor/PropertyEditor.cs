
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


[CustomEditor(typeof(Property))]
[CanEditMultipleObjects]
public class PropertyEditor : Editor
{

    Property propertyScript;
    SerializedObject GetTarget;

    List<Property> ToRemove = new List<Property>();

    private void OnEnable()
    {
        propertyScript = (Property)target;
        GetTarget = new SerializedObject(propertyScript);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        propertyScript = (Property)target;
        GetTarget = new SerializedObject(propertyScript);

        GetTarget.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(propertyScript.Neighbors.Count + " Neighbors", EditorStyles.boldLabel);
        if (PropertyManager.Instance != null)
        {

            CheckDuplicatesAndNulls();

            RemovePendent();

            //REMOVE Button
            int i = 0;
            foreach (Property p in propertyScript.Neighbors)
            {
                using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                {
                    propertyScript.Neighbors[i] = (Property)EditorGUILayout.ObjectField("Neighbor " + i, p, typeof(Property), true);
                    if (GUILayout.Button("Remove", GUILayout.MaxWidth(60)))
                        ToRemove.Add(p);
                }
                i++;
            }

            EditorGUILayout.Space();

            //CLEAR BUTTON AND DROP AREA
            using (var horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                DropAreaGUI();

                //if(GUILayout.Button("Add Property Slot")) {propertyScript.Neighbors.Add(null);}

                if (GUILayout.Button("Clear", GUILayout.MaxWidth(60), GUILayout.MinHeight(45)))
                {
                    foreach (Property p in propertyScript.Neighbors)
                    {
                        ToRemove.Add(p);
                    }
                }
            }

            RemovePendent();

            foreach (Property p in propertyScript.Neighbors)
            {
                if (p == null)
                    continue;

                if (p.Neighbors.Contains(propertyScript) == false)
                    p.Neighbors.Add(propertyScript);
            }

            //-----------------------------------------------------------------------------------------------------------

            propertyScript.UpdateSprite(propertyScript);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(propertyScript);
            EditorSceneManager.MarkSceneDirty(propertyScript.gameObject.scene);
        }
        GetTarget.ApplyModifiedProperties();
    }

    private void CheckDuplicatesAndNulls()
    {
        //Avoid duplicates in case of direct assignment in the list
        int i;
        for (i = 0; i < propertyScript.Neighbors.Count; i++)
        {
            for (int j = 0; j < propertyScript.Neighbors.Count; j++)
            {
                if (i == j)
                    continue;

                if (propertyScript.Neighbors[i] == null)
                    ToRemove.Add(propertyScript.Neighbors[i]);

                if (propertyScript.Neighbors[j] == null)
                    ToRemove.Add(propertyScript.Neighbors[j]);

                if (ToRemove.Contains(propertyScript.Neighbors[i]) || ToRemove.Contains(propertyScript.Neighbors[j]))
                    continue;

                if (propertyScript.Neighbors[i].Equals(propertyScript.Neighbors[j]))
                    ToRemove.Add(propertyScript.Neighbors[j]);
            }
        }
    }

    private bool RemovePendent()
    {
        if (ToRemove.Count > 0)
        {
            foreach (Property p in ToRemove)
            {
                //PropertyManager.Instance.lineManager.RemoveAnyLineConnecting(propertyScript, p);
                propertyScript.Neighbors.Remove(p);
                p.Neighbors.Remove(propertyScript);

            }
            ToRemove.Clear();

            return true;
        }
        return false;
    }

    public void DropAreaGUI()
    {
        Event evt = Event.current;
        Rect drop_area = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(drop_area, "Drop New Neighbors Here");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!drop_area.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object dragged_object in DragAndDrop.objectReferences)
                    {
                        // Do On Drag Stuff here
                        if (dragged_object is GameObject)
                        {
                            Property DraggedProperty = (dragged_object as GameObject).GetComponent<Property>();

                            if (DraggedProperty != null)
                            {
                                if (propertyScript.Neighbors.Contains(DraggedProperty))
                                    Debug.LogWarning("This property is already a Neighbor");
                                else
                                {
                                    Property dragged_Property = (dragged_object as GameObject).GetComponent<Property>();
                                    propertyScript.Neighbors.Add(dragged_Property);
                                    //PropertyManager.Instance.lineManager.AddLine(propertyScript, dragged_Property);
                                }
                            }
                            else
                                Debug.LogWarning("This Gameobject do not have a Property Component");
                        }
                        else
                            Debug.LogWarning("Not a gameobject");
                    }
                }
                break;
        }
    }


}
