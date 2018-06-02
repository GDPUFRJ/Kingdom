using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PropertyManager : MonoBehaviour {

    public GameObject Propriedade;
    public Sprite Castelo, Mina, Vila, Fazenda, Floresta;

    public static List<Property> Propriedades;

    private string filepath;

    private void Awake()
    {
        //carregar coisas
        filepath = Application.persistentDataPath + "/properties.sav";
    }

    private void Start()
    {
        Propriedades = new List<Property>(GetComponentsInChildren<Property>());
        Debug.Log(Propriedades.Count);
    }

    private void OnApplicationQuit()
    {
        //salvar as coisas

        FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);

        //StreamWriter sw 

        Propriedades = new List<Property>(GetComponentsInChildren<Property>());

        foreach(Property prop in Propriedades)
        {

        }
    }

    


}
