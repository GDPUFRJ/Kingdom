using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatedSpriteBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite portugueseSprite;
    [SerializeField] private Sprite englishSprite;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChangeText(TranslationManager.GameLanguage);
    }

    private void ChangeText(Language language)
    {
        switch (language)
        {
            case Language.Portuguese:
                spriteRenderer.sprite = portugueseSprite;
                break;
            case Language.English:
                spriteRenderer.sprite = englishSprite;
                break;
        }
    }
}
