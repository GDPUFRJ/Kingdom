using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("volumeMusic", 1f);
    }

    private void Update()
    {
        VolumeManager.Instance.volumeMusic = slider.value;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("volumeMusic", slider.value);
    }

    public void OnButtonClick()
    {
        if (slider.value > 0)
        {
            OnDisable();
            slider.value = 0;
        }
        else
            slider.value = PlayerPrefs.GetFloat("volumeMusic", 1f);
    }
}
