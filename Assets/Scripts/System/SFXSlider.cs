using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("volumeSFX", 1f);
    }

    private void Update()
    {
        VolumeManager.Instance.volumeSFX = slider.value;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("volumeSFX", slider.value);
    }

    public void OnButtonClick()
    {
        if (slider.value > 0)
        {
            OnDisable();
            slider.value = 0;
        }
        else
            slider.value = PlayerPrefs.GetFloat("volumeSFX", 1f);
    }
}
