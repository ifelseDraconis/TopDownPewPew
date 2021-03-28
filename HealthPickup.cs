using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField, Tooltip("The source pickup object.")]
    private GameObject thisContainer;

    [SerializeField, Tooltip("This controls the amount of health gained on pickup.")]
    private float healthHealValue;

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    // if one stands to notice, the health pickups can affect both the player and the hostiles
    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("It is here.");
        bool didGainHealth = false;
        if (collision.gameObject.gameObject.GetComponent<DamageableObject>() != null)
        {
            Debug.Log("Touch me.");
            DamageableObject thisKnucklehead = collision.gameObject.GetComponent<DamageableObject>();
            didGainHealth = thisKnucklehead.gainHealth(healthHealValue);
            if (didGainHealth)
            {
                Destroy(thisContainer);
            }
        }

        if (collision.gameObject.transform.parent.gameObject.GetComponent<DamageableObject>() != null)
        {
            Debug.Log("Touch them.");
            DamageableObject thisKnucklehead = collision.gameObject.transform.parent.gameObject.GetComponent<DamageableObject>();
            didGainHealth = thisKnucklehead.gainHealth(healthHealValue);
            if (didGainHealth)
            {
                Destroy(thisContainer);
            }
        }
    }
}
