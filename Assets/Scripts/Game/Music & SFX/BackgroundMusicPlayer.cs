using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [FMODUnity.EventRef] public string fmodEvent;
    FMOD.Studio.EventInstance eventInstance;
    //FMOD.Studio.ParameterInstance parametro;
    
    [SerializeField] private bool playOnAwake = true;

    private void Awake()
    {
        // criando instância do evento de áudio
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        //MudarParametro(e, "Game", 0);

        if (playOnAwake)
            Play();
    }

    // função para executar o evento
    void Play()
    {
        // se tiver algum evento
        if (fmodEvent != null)
        {
            // dispara o som
            eventInstance.start();
        }
    }

    // função para mudar parametro de um evento
    //void MudarParametro(FMOD.Studio.EventInstance e, string nome, float valorParametro)
    //{
    //    e.getParameter(nome, out parametro);
    //    parametro.setValue(valorParametro);
    //}

    // função para finalizar música em loop
    void PararMusica()
    {
        FMOD.Studio.Bus playerBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //public void OnGamePaused()
    //{
    //    MudarParametro(eventInstance, "PauseGame", 1);
    //}

    //public void OnGameUnpaused()
    //{
    //    MudarParametro(eventInstance, "PauseGame", 0);
    //}

    //public void OnGameStarted()
    //{
    //    MudarParametro(eventInstance, "Game", 1);
    //}

    //public void OnGameOver()
    //{
    //    PararMusica();
    //}

    //public void OnGameRestarted()
    //{
    //    Play();
    //    MudarParametro(eventInstance, "Game", 1);
    //}
}
