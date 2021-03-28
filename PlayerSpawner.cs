using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // this is the spawned GameObject for the player object
    [SerializeField, Tooltip("Baddie to spawn.")]
    private GameObject thisPlayer;

    public GameObject createPlayer()
    {
       GameObject newPlayer = Instantiate(thisPlayer, transform, false);
        return newPlayer;
    }
}
