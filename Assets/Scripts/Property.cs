using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Property : MonoBehaviour, IPointerClickHandler{

    public enum Tipo
    {
        Castelo, Mina, Vila, Fazenda, Floresta
    }

    [SerializeField] private Tipo tipo;
    public bool dominada = false;

    [Range(-100, 100)] public int riqueza = 0;
    [Range(-100, 100)] public int construcao = 0;
    [Range(-100, 100)] public int alimento = 0;

    private void OnValidate()
    {
        switch (tipo)
        {
            case Tipo.Castelo:
                gameObject.tag = "castelo";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<PropertyManager>().Castelo;
                break;
            case Tipo.Mina:
                gameObject.tag = "mina";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<PropertyManager>().Mina;
                break;
            case Tipo.Vila:
                gameObject.tag = "vila";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<PropertyManager>().Vila;
                break;
            case Tipo.Fazenda:
                gameObject.tag = "fazenda";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<PropertyManager>().Fazenda;
                break;
            case Tipo.Floresta:
                gameObject.tag = "floresta";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<PropertyManager>().Floresta;
                break;
            default:
                gameObject.tag = "castelo";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<PropertyManager>().Castelo;
                break;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        //ative aqui o metodo para mostrar propriedades
        Debug.Log("Tocou!");
    }
}
