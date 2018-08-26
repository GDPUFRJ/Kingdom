using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapController : Singleton<WorldMapController>, IPointerDownHandler, IPointerUpHandler {

    protected WorldMapController() { }

    public bool Selecionado; //{ get; private set;}

    public void OnPointerDown(PointerEventData eventData)
    {
        Selecionado = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Selecionado = false;
    }
}
