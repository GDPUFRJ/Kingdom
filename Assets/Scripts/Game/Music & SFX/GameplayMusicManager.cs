using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusicManager : MonoBehaviour
{
    private void Awake()
    {
        var possibleValues = new int[] { 1, 2, 3, 4 };
        var value = possibleValues[Random.Range(0, possibleValues.Length)];
        var player = FMODPlayer.Instance;
        player.SetParameterByName("gameplayBG", "Game", value);
        player.Play("gameplayBG");
    }
}
