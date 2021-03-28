using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChase : MonoBehaviour
{
    public GameObject playerCharacter;

    [SerializeField, Tooltip("This is the movement rate of the player.")]
    private float moveRate;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCharacter != null)
        {
            transform.position = playerCharacter.transform.position;
        }
        moveRate = 60.0f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
            
        
            // changes the position of the player empty that the camera is a child of so that the camera follows
        if (playerCharacter != null)
        {
            Vector3 offsetPosition = new Vector3(0, 12, -5);
            offsetPosition += playerCharacter.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, offsetPosition, moveRate);
        }
        else
        {
            GameObject playerFind = GameObject.FindGameObjectWithTag("PlayerContainer");
            if (playerFind != null)
            {
                playerCharacter = playerFind;
            }
        }
            
        
        
    }
}
