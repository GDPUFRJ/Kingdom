﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatedSpriteBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite portugueseSprite;
    [SerializeField] private Sprite englishSprite;

    [Tooltip("Marque essa caixa se a sprite estiver no Menu Principal.")]
    [SerializeField] private bool mainMenu = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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
                spriteRenderer.sprite = portugueseSprite;
                break;
            case Language.English:
                spriteRenderer.sprite = englishSprite;
                break;
        }
    }
}
