using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatedImageBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite portugueseSprite;
    [SerializeField] private Sprite englishSprite;

    [Tooltip("Marque essa caixa se a sprite estiver no Menu Principal.")]
    [SerializeField] private bool mainMenu = false;

    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();

        ChangeText(TranslationManager.GameLanguage);

        if (mainMenu)
        {
            TranslationManager.OnLanguageChanged += OnLanguageChanged;
        }
    }

    private void OnLanguageChanged()
    {
        ChangeText(TranslationManager.GameLanguage);
    }

    private void ChangeText(Language language)
    {
        switch (language)
        {
            case Language.Portuguese:
                imageComponent.sprite = portugueseSprite;
                break;
            case Language.English:
                imageComponent.sprite = englishSprite;
                break;
        }
    }
}
