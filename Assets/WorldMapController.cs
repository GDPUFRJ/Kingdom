using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public static bool Selecionado; //{ get; private set;}

    public void OnPointerDown(PointerEventData eventData)
    {
        Selecionado = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Selecionado = false;
    }
}
