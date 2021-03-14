using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAndRotate : MonoBehaviour
{
    private Transform thisTransform;
    private bool goUp;
    private float moveMe;

    [SerializeField, Tooltip("This controls the rotate speed.")]
    private float rotateSpeed;

    [SerializeField, Tooltip("This controls the bob speed.")]
    private float bobSpeed;

    [SerializeField, Tooltip("This controls the bob distance.")]
    private float bobDistance;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveMe += 180.0f * Time.deltaTime;

        Quaternion target = Quaternion.Euler(0.0f, moveMe, 0.0f);


        if (thisTransform.position.y <= 0.5f)
        {
            goUp = true;

        }
        else if (thisTransform.position.y >= (1.0f + bobDistance))
        {
            goUp = false;
        }

        if (goUp)
        {
            thisTransform.Translate(Vector3.up * Time.deltaTime * bobSpeed, Space.World);
        }
        else
        {
            thisTransform.Translate(-Vector3.up * Time.deltaTime * bobSpeed, Space.World);
        }


        thisTransform.rotation = target;
    }
}
