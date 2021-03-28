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

    public float getOuchPoints()
    {
        return ouchPoints;
    }

    // This script will be what deals damage to players and enemies when needed
    // this is nice and seperate from the bullet script so I can put it on environmental hazards too
    private void OnCollisionEnter(Collision collision)
    {
        bool didDamage = false;
        if (collision == null)
        {
            return;
        }
        else
        {
            didDamage = true;
        }

        
        string thisHitMe = collision.collider.gameObject.tag;
        if (thisHitMe == "Player")
        {
            collision.collider.gameObject.transform.parent.gameObject.GetComponent<DamageableObject>().reduceHealth(ouchPoints);            
        }

        if (thisHitMe == "Baddie")
        {
            collision.collider.gameObject.GetComponent<DamageableObject>().reduceHealth(ouchPoints);            
        }

        if (didDamage)
        {
            Destroy(gameObject);
        }
    }
}
