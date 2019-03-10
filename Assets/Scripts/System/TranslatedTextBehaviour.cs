using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatedTextBehaviour : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string portugueseText;
    [TextArea]
    [SerializeField] private string englishText;
    [Tooltip("Marque essa caixa se o texto estiver no Menu Principal.")]
    [SerializeField] private bool mainMenu = false;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();

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
                text.text = portugueseText;
                break;
            case Language.English:
                text.text = englishText;
                break;
        }
    }
}
