using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChase : MonoBehaviour
{
    public GameObject playerCharacter;
    private float moveRate;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerCharacter.transform.position;
        moveRate = 60.0f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        // changes the position of the player empty that the camera is a child of so that the camera follows
        Vector3 offsetPosition = new Vector3(0, 12, -5);
        offsetPosition += playerCharacter.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, offsetPosition, moveRate);
    }
}
