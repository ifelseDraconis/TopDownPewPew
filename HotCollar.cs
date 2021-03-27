using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotCollar : MonoBehaviour
{
    public GameObject playerPawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // makes it so that the player empty follows the player pawn exactly
        transform.position = playerPawn.transform.position;
    }
}
