using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSFX : MonoBehaviour
{
    public void PlaySwordSound()
    {
        FMODPlayer.Instance.Play("swords crossing");
    }

    public void PlayWinSound()
    {
        FMODPlayer.Instance.Play("battle win");
    }

    public void PlayLoseSound()
    {
        FMODPlayer.Instance.Play("battle lose");
    }
}
