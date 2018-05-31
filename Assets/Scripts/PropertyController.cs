using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyController : MonoBehaviour {

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
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<Property>().Castelo;
                break;
            case Tipo.Mina:
                gameObject.tag = "mina";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<Property>().Mina;
                break;
            case Tipo.Vila:
                gameObject.tag = "vila";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<Property>().Vila;
                break;
            case Tipo.Fazenda:
                gameObject.tag = "fazenda";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<Property>().Fazenda;
                break;
            case Tipo.Floresta:
                gameObject.tag = "floresta";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<Property>().Floresta;
                break;
            default:
                gameObject.tag = "castelo";
                GetComponent<SpriteRenderer>().sprite = GetComponentInParent<Property>().Castelo;
                break;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
