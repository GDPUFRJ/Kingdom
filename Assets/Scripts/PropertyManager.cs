using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PropertyManager : Singleton<PropertyManager> {

    protected PropertyManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    [Header("Sprites")]
    public Sprite Castelo;
    public Sprite Mina;
    public Sprite Vila;
    public Sprite Fazenda;
    public Sprite Floresta;

    [Header("Colors")]
    public Color DominatedLine;
    public Color NotDominatedLine;
    public Color NotDominatedProperty;

    [Header("Others")]
    public GameObject LineManager;
    public GameObject NeighborLine;


    public List<Property> Propriedades = new List<Property>();

    //private string filepath;

    private void Awake()
    {
        //carregar coisas
        //filepath = Application.persistentDataPath + "/properties.sav";
    }

    private void Start()
    {
        Propriedades = new List<Property>(GetComponentsInChildren<Property>());
        LineManager.SetActive(true); //ensure that it will only trace lines after fill up properties list
    }

    private void OnApplicationQuit()
    {
        //salvar as coisas
        //FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
        //StreamWriter sw 
        //Propriedades = new List<Property>(GetComponentsInChildren<Property>());
        //foreach(Property prop in Propriedades)
        //{
            //ToDo
        //}
    }

    


}
