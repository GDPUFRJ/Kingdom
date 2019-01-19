using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    [SerializeField] MainMenu mainMenu;

    public void GoBack()
    {
        mainMenu.GoBackToMenuFromCredits();
    }
}
