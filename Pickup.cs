using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField, Tooltip("This is the delay before its destroyed.")]
    private float timeToDestroy = 10.0f;

    protected virtual void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    
}
