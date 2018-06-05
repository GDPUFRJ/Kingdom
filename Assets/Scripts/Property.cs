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

    private void OnValidate()
    {
        if (PropertyManager.Instance == null)
            return;

        if (dominated)
            GetComponent<SpriteRenderer>().color = Color.white;
        else
            GetComponent<SpriteRenderer>().color = PropertyManager.Instance.NotDominatedProperty;
        
        foreach (Property p in Neighbors)
        {
            if (p.Neighbors.Contains(this) == false)
                p.Neighbors.Add(this);
        }
        
        switch (type)
        {
            case Tipo.Castelo:
                gameObject.tag = "castelo";
                GetComponent<SpriteRenderer>().sprite = PropertyManager.Instance.Castelo;
                break;
            case Tipo.Mina:
                gameObject.tag = "mina";
                GetComponent<SpriteRenderer>().sprite = PropertyManager.Instance.Mina;
                break;
            case Tipo.Vila:
                gameObject.tag = "vila";
                GetComponent<SpriteRenderer>().sprite = PropertyManager.Instance.Vila;
                break;
            case Tipo.Fazenda:
                gameObject.tag = "fazenda";
                GetComponent<SpriteRenderer>().sprite = PropertyManager.Instance.Fazenda;
                break;
            case Tipo.Floresta:
                gameObject.tag = "floresta";
                GetComponent<SpriteRenderer>().sprite = PropertyManager.Instance.Floresta;
                break;
            default:
                gameObject.tag = "castelo";
                GetComponent<SpriteRenderer>().sprite = PropertyManager.Instance.Castelo;
                break;
        }
    }

    private void OnDayEnd()
    {
        //Debug.Log("A propriedade " + customTitle + " Passou para o dia seguinte");
    }



    public void UpdateNeighborLines()
    {
        DestroyNeighborLines();
        BuildNeighborLines();
    }

    public void BuildNeighborLines()
    {
        GameObject NeighborLine = PropertyManager.Instance.NeighborLine;
        Vector3 NeighborPosition;
        foreach (Property p in Neighbors)
        {
            NeighborPosition = p.transform.position;
            NeighborLine.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
            NeighborLine.GetComponent<LineRenderer>().SetPosition(1, NeighborPosition);

            if (p.dominated)
            {
                NeighborLine.GetComponent<LineRenderer>().startColor = Color.white;
                NeighborLine.GetComponent<LineRenderer>().endColor = Color.white;
            }
            else
            {
                NeighborLine.GetComponent<LineRenderer>().startColor = Color.gray;
                NeighborLine.GetComponent<LineRenderer>().endColor = Color.gray;
            }

            Instantiate(NeighborLine, this.transform);
        }
    }

    public void DestroyNeighborLines()
    {
        
        foreach (Transform child in transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
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