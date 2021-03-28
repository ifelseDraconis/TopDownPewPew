using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField, Tooltip("AmmoSize")]
    private bool smallOrBig;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this only makes a double redundant call to insure that the object that runs into it has the necessary scripts attached before taking action
    // Ammo will only come in two sizes to prevent over complication
    private void OnCollisionEnter(Collision collision)
    {
        bool didPickup = false;
        if (collision.collider.gameObject.transform.parent.gameObject.GetComponent<HumanoidPawn>() != null)
        {
            didPickup = collision.collider.gameObject.transform.parent.gameObject.GetComponent<HumanoidPawn>().letsAddAmmo(smallOrBig);   
            
        }
        else if (collision.collider.gameObject.GetComponent<HumanoidPawn>() != null)
        {
            didPickup = collision.collider.gameObject.GetComponent<HumanoidPawn>().letsAddAmmo(smallOrBig);
        }
        if (didPickup)
        {
            Destroy(gameObject);
        }
    }

}
