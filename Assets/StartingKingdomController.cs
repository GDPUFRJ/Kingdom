using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingKingdomController : Singleton<StartingKingdomController>
{
    [SerializeField] private Transform camera;

    private Kingdom kingdom;
    public Kingdom PlayerKingdom { get { return kingdom; } set { kingdom = value; } }

    protected StartingKingdomController() { }

    private void Awake()
    {
        if (SaveSystem.newGame)
        {
            DefinePlayerKingdom();
        }
    }

    private void Start()
    {
        SetUpCameraPosition();
    }

    private void DefinePlayerKingdom()
    {
        List<Kingdom> kingdoms = new List<Kingdom> { Kingdom.Blue, Kingdom.Green, Kingdom.Orange, Kingdom.Purple, Kingdom.Red };
        Kingdom randomKingdom = kingdoms[Random.Range(0, kingdoms.Count)];

        foreach (Kingdom k in kingdoms)
        {
            List<Property> kingdomProperties = GetKingdomProperties(k);

            foreach(Property property in kingdomProperties)
            {
                if (k == randomKingdom)
                {
                    property.SetDominated(true, false);
                }
                else
                {
                    property.SetDominated(false, false);
                }
            }
        }

        kingdom = randomKingdom;
    }

    private void SetUpCameraPosition()
    {
        List<Property> playerProperties = GetKingdomProperties(PlayerKingdom);
        Vector2 meanPosition = new Vector2(0, 0);

        foreach (Property property in playerProperties)
        {
            meanPosition += (Vector2)property.transform.position;
        }

        meanPosition /= playerProperties.Count;
        camera.position = new Vector3(meanPosition.x, meanPosition.y, camera.position.z);
    }

    private List<Property> GetKingdomProperties(Kingdom kingdom)
    {
        List<Property> properties = PropertyManager.Instance.Propriedades;
        List<Property> kingdomProperties = new List<Property>();

        foreach (Property property in properties)
        {
            if (property.kingdom == kingdom)
            {
                kingdomProperties.Add(property);
            }
        }

        return kingdomProperties;
    }
}
