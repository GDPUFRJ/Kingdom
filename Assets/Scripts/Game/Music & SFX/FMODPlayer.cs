using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : MonoBehaviour
{
    [FMODUnity.EventRef] public string fmodEvent;
    FMOD.Studio.EventInstance eventInstance;
    
    [SerializeField] private bool playOnAwake = true;

    private void Awake()
    {
        if (playOnAwake)
        {
            CreateFmodInstance();
            Play();
        }
    }

    public void CreateFmodInstance()
    {
        // criando instância do evento de áudio
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    }

    // função para executar o evento
    public void Play()
    {
        // se tiver algum evento
        if (fmodEvent != null)
        {
            if (!eventInstance.isValid()) CreateFmodInstance();

            // dispara o som
            eventInstance.start();
        }
    }

    // função para finalizar música em loop
    public static void StopAllSounds()
    {
        FMOD.Studio.Bus playerBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetParameterByName(string parameterName, float value)
    {
        eventInstance.setParameterByName(parameterName, value);
    }

    public float GetParameterByName(string parameterName)
    {
        float value;
        eventInstance.getParameterByName(parameterName, out value);
        return value;
    }
}
