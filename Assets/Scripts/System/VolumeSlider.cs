using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private enum kind { SFX, GameplayMusic, MenuMusic}
    [SerializeField] private kind control;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        switch (control)
        {
            case kind.SFX:
                slider.value = PlayerPrefs.GetFloat("volumeSFX", 1f);
                break;
            case kind.GameplayMusic:
                slider.value = PlayerPrefs.GetFloat("volumeGameplayMusic", 1f);
                break;
            case kind.MenuMusic:
                slider.value = PlayerPrefs.GetFloat("volumeMenuMusic", 1f);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        switch (control)
        {
            case kind.SFX:
                VolumeManager.Instance.volumeSFX = slider.value;
                break;
            case kind.GameplayMusic:
                VolumeManager.Instance.volumeMusic = slider.value;
                break;
            case kind.MenuMusic:
                VolumeManager.Instance.volumeUiMusic = slider.value;
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        switch (control)
        {
            case kind.SFX:
                PlayerPrefs.SetFloat("volumeSFX", slider.value);
                break;
            case kind.GameplayMusic:
                PlayerPrefs.SetFloat("volumeGameplayMusic", slider.value);
                break;
            case kind.MenuMusic:
                PlayerPrefs.SetFloat("volumeMenuMusic", slider.value);
                break;
            default:
                break;
        }
    }

    public void OnButtonClick()
    {
        if (slider.value > 0)
        {
            OnDisable();
            slider.value = 0;
        }
        else
        {
            switch (control)
            {
                case kind.SFX:
                    slider.value = PlayerPrefs.GetFloat("volumeSFX", 1f);
                    break;
                case kind.GameplayMusic:
                    slider.value = PlayerPrefs.GetFloat("volumeGameplayMusic", 1f);
                    break;
                case kind.MenuMusic:
                    slider.value = PlayerPrefs.GetFloat("volumeMenuMusic", 1f);
                    break;
                default:
                    break;
            }
            if (slider.value == 0) slider.value = 1;
        }
    }
}
