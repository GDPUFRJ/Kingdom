using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : Singleton<VolumeManager>
{
    protected VolumeManager() { }

    [Range(0, 1)] public float volumeMusic = 1;
    [Range(0, 1)] public float volumeSFX = 1;
    [Range(0, 1)] public float volumeUiMusic = 1;

    void Update()
    {
        // chamada do controle de volume
        VolumeMUSIC(volumeMusic);
        VolumeSFX(volumeSFX);
        VolumeUI_MUSIC(volumeUiMusic);
    }

    // função para controlar volume da BGM (volume min=0 max=1)
    void VolumeMUSIC(float volume)
    {
        string vcaPath = "vca:/music";
        FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA(vcaPath);
        vca.setVolume(volume);
    }

    // função para controlar volume da UI (volume min=0 max=1)
    void VolumeSFX(float volume)
    {
        string vcaPath = "vca:/sfx";
        FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA(vcaPath);
        vca.setVolume(volume);
    }

    // função para controlar volume do SFX no jogo (volume min=0 max=1)
    void VolumeUI_MUSIC(float volume)
    {
        string vcaPath = "vca:/ui music";
        FMOD.Studio.VCA vca = FMODUnity.RuntimeManager.GetVCA(vcaPath);
        vca.setVolume(volume);
    }
}
