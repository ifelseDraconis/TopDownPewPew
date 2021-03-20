using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuchPickup : Pickup
{
    // making it a private serialized field on each object increases the ease of customization
    [SerializeField, Tooltip("This is how much damage the object does when touched.")]
    private float ouchPoints;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // This script will be what deals damage to players and enemies when needed
    // this is nice and seperate from the bullet script so I can put it on environmental hazards too
    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.collider.gameObject.GetComponent<DamageableObject>() != null)
        {
            DamageableObject thisKnucklehead = collision.collider.gameObject.GetComponent<DamageableObject>();
            thisKnucklehead.reduceHealth(ouchPoints);
        }

        if (collision.gameObject.transform.parent.gameObject.GetComponent<DamageableObject>())
        {
            Debug.Log("Touch them.");
            DamageableObject thisKnucklehead = collision.gameObject.transform.parent.gameObject.GetComponent<DamageableObject>();
            thisKnucklehead.reduceHealth(ouchPoints);
            
        }
    }
}
