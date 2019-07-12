using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private enum state { menu, gameplay }
    [SerializeField] private state scene;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;

    public void GoBack()
    {
        switch (scene)
        {
            case state.menu:
                if (mainMenu != null) mainMenu.GoBackToMenuFromOptions();
                break;
            case state.gameplay:
                if (pauseMenu != null) pauseMenu.GoBackToPauseMenuFromOptions();
                break;
            default:
                break;
        }
    }
}
