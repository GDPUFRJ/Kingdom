using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusicManager : MonoBehaviour
{
    private FMODPlayer fmodPlayer;

    private void Awake()
    {
        // Initializes parameter Game by a random value
        fmodPlayer = GetComponent<FMODPlayer>();
        fmodPlayer.CreateFmodInstance();
        var possibleValues = new int[] { 1, 2, 3, 4 };
        var value = possibleValues[Random.Range(0, possibleValues.Length)];
        fmodPlayer.SetParameterByName("Game", value);
        fmodPlayer.Play();
    }
}
