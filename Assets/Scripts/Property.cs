using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Property : MonoBehaviour, IPointerClickHandler, IComparer{
    [Header("Basic Informations")]
    public string customTitle = " ";
    public Tipo type;
    public bool dominated = false;
    public int level = 1;

    [Header("Production Information")]
    [Range(-100, 100)] public int gold = 0;
    [Range(-100, 100)] public int buildingMaterial = 0;
    [Range(-100, 100)] public int food = 0;

    [Header("Neighborhood")]
    [HideInInspector]public List<Property> Neighbors = new List<Property>();
    

    private void Start()
    {
        TimerPanel.OnDayEnd += OnDayEnd;

        // DestroyNeighborLines();
        //BuildNeighborLines();
    }


    private void OnDayEnd()
    {
        //Debug.Log("A propriedade " + customTitle + " Passou para o dia seguinte");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ative aqui o metodo para mostrar propriedades
        Debug.Log("Tocou!");
    }

    private void OnApplicationQuit()
    {
        TimerPanel.OnDayEnd -= OnDayEnd;
    }

    public int Compare(object x, object y)
    {
        Property a = x as Property;
        Property b = y as Property;

        if (a.gameObject.Equals(b.gameObject))
            return 0;
        else
            return 1;
    }
}
public enum Tipo
{
    Castelo, Mina, Vila, Fazenda, Floresta
}