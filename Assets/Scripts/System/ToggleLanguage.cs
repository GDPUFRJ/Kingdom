using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLanguage : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image checkmark;
    [SerializeField] private Sprite portuguese;
    [SerializeField] private Sprite english;

    private void Start()
    {
        if (TranslationManager.GameLanguage == Language.Portuguese)
        {
            background.sprite = english;
            checkmark.sprite = portuguese;
        }
        else
        {
            background.sprite = portuguese;
            checkmark.sprite = english;
        }
    }
}
