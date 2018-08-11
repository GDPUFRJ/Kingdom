using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DuplicateCatcher : MonoBehaviour {
#if UNITY_EDITOR
        //catch duplication of this GameObject
        [SerializeField]
        int instanceID = 0;
        void Awake()
        {
            if (Application.isPlaying)
                return;

            if (instanceID == 0)
            {
                instanceID = GetInstanceID();
                return;
            }

            if (instanceID != GetInstanceID() && GetInstanceID() < 0)
            {
                //Debug.LogError("Detected Duplicate!");
                instanceID = GetInstanceID();
                GetComponent<Property>().Neighbors.Clear();
                PropertyManager.Instance.lineManager.RemoveAnyInvalidLine();
            }
        }
#endif
    
}
