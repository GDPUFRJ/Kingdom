using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : Singleton<FMODPlayer>
{
    protected FMODPlayer() { }

    [System.Serializable]
    public class Audio
    {
        public string name;
        [FMODUnity.EventRef] public string fmodEvent;
        public FMOD.Studio.EventInstance eventInstance;
    }

    [SerializeField] private List<Audio> audios;

    public void Play(string name)
    {
        var audio = FindAudioByName(name);

        if (audio.fmodEvent != null)
        {
            if (!audio.eventInstance.isValid()) CreateFmodInstance(audio);

            audio.eventInstance.start();
        }
    }

    private Audio FindAudioByName(string name)
    {
        foreach (var audio in audios)
        {
            if (audio.name == name)
            {
                return audio;
            }
        }

        Debug.LogError("FMODPlayer: Não consegui encontrar o áudio " + name + "! Verifique no objeto " + this.name + " se você adicionou esse áudio.");
        return null;
    }

    private void CreateFmodInstance(string name)
    {
        var audio = FindAudioByName(name);
        if (audio.fmodEvent != null)
            audio.eventInstance = FMODUnity.RuntimeManager.CreateInstance(audio.fmodEvent);
    }

    private void CreateFmodInstance(Audio audio)
    {
        if (audio.fmodEvent != null)
            audio.eventInstance = FMODUnity.RuntimeManager.CreateInstance(audio.fmodEvent);
    }

    public static void StopAllSounds()
    {
        FMOD.Studio.Bus playerBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetParameterByName(string audioName, string parameterName, float parameterValue)
    {
        var audio = FindAudioByName(audioName);
        if (!audio.eventInstance.isValid()) CreateFmodInstance(audio);
        audio.eventInstance.setParameterByName(parameterName, parameterValue);
    }

    public float GetParameterByName(string audioName, string parameterName)
    {
        var audio = FindAudioByName(audioName);
        float value;
        audio.eventInstance.getParameterByName(parameterName, out value);
        return value;
    }
}
