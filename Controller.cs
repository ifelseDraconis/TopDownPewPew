using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Animator")]
    public Animator thisAnimate;

    [Header("Pawn")]
    public Pawn pawn;


    public float moveSpeed = 1.0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // fetches the animator
        if (gameObject.GetComponent<Animator>() != null)
        {
            thisAnimate = gameObject.GetComponent<Animator>();
        }
        else if (gameObject.transform.parent.gameObject.GetComponent<Animator>() != null)
        {
            thisAnimate = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
