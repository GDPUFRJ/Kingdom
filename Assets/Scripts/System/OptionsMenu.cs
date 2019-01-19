using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    public void GoBack()
    {
        mainMenu.GoBackToMenuFromOptions();
    }
}
