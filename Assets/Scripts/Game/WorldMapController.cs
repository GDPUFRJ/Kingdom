using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapController : Singleton<WorldMapController>, IPointerDownHandler, IPointerUpHandler {

    protected WorldMapController() { }

    public bool Selecionado; //{ get; private set;}

    public void Start()
    {
        //GameManager.Instance.CanvasBattle.GetComponent<Renderer>().bounds.size = GetComponent<SpriteRenderer>().bounds.size;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Selecionado = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Selecionado = false;
    }
}
