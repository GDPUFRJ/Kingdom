using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TranslationManager : MonoBehaviour
{
    private static Language gameLanguage = Language.Portuguese;
    public static Language GameLanguage { get { return gameLanguage; } }

    public delegate void LanguageChanged();
    public static event LanguageChanged OnLanguageChanged;

    public static void ChangeLanguage()
    {
        gameLanguage = gameLanguage == Language.Portuguese ? Language.English : Language.Portuguese;
        OnLanguageChanged();
    }

    public static void UnsubscribeDelegates()
    {
        OnLanguageChanged = null;
    }

    private void OnApplicationQuit()
    {
        UnsubscribeDelegates();
    }
}

public enum Language { English, Portuguese }
