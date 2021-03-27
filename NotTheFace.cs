using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NotTheFace : DamageableObject
{
    [SerializeField, Tooltip("HitQue")]
    private AudioClip weGotDinked;

    [SerializeField, Tooltip("Item Drop Settings")]
    private WeightedObject[] theseDrops;

    [SerializeField, Tooltip("HealthBar container.")]
    private GameObject thisBarContainer;

    [SerializeField, Tooltip("HealthBar Game Object.")]
    private GameObject thisHealthBarUI;

    [SerializeField, Tooltip("HealthBar Slider Object.")]
    private Slider thisHealthBar;
    

    private float totalWeightForDrops;
    private bool itemDropped;

    [System.Serializable]
    public class WeightedObject
    {
        [SerializeField, Tooltip("the object selected by this choice.")]
        public GameObject thisItem;

        [SerializeField, Tooltip("the object selected by this choice.")]
        public float chanceOfDrop = 1.0f;

    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        SumTheWeights();
        base.Start();
        thisHealthBar.value = base.getCurrentHP() / base.getMaxHP();
    }

    // Update is called once per frame
    public override void Update()
    {
        thisHealthBar.value = base.getCurrentHP() / base.getMaxHP();
        base.Update();
    }

    // this plays a sound at the location the npc gets hit
    // it also spawns the hit particles at the normal of the hit
    private void OnCollisionEnter(Collision collision)
    {
        string whatHitUs = collision.collider.gameObject.tag;

        if (whatHitUs == "Bullet")
        {
            AudioManager.PlaySoundAt(weGotDinked, gameObject.transform, 0.65f);
            
            if (collision.collider.gameObject.GetComponent<bulletMove>() != null)
            {
                Debug.Log("Hit by a shot.");
                bulletMove thisBullet = collision.collider.gameObject.GetComponent<bulletMove>();
                OuchPickup ouchHit = collision.collider.gameObject.GetComponent<OuchPickup>();                
                thisBullet.goodHit(collision);
            }
        }

        // this lets us know if we picked up a weapon or ammo
        if (whatHitUs == "Ammo" || whatHitUs == "Weapon")
        {
            if(gameObject.GetComponent<AIController>() != null)
            {
                gameObject.GetComponent<AIController>().pickedUpWeapon();
            }
        }
    }

    public void goodByeCruelWorld()
    {

        Destroy(gameObject, 4.6f);
        Destroy(gameObject.transform.parent.gameObject, 4.6f);
    }

    public Transform getHealthBarContainer()
    {
        return thisBarContainer.transform;
    }

    // decides which item to drop
    private GameObject chooseItem()
    {
        GameObject itemSelected = null;
        float maxChoice = totalWeightForDrops;
        float randChoice = UnityEngine.Random.Range(0, maxChoice);
        float weightSum = 0;

        foreach(WeightedObject thisDropItem in theseDrops)
        {
            weightSum += thisDropItem.chanceOfDrop;
            if (randChoice <= weightSum)
            {
                itemSelected = thisDropItem.thisItem;
                break;
            }
        }
        
        return itemSelected;
    }

    // sums the total weight of all items in the drop list
    private void SumTheWeights()
    {
        foreach(WeightedObject dropItem in theseDrops)
        {
            totalWeightForDrops += dropItem.chanceOfDrop;
        }
    }

    // this actually triggers the drop
    public void DropAnItem()
    {
        GameObject thisDrop = chooseItem();
        if (thisDrop != null && itemDropped == false)
        {
            Instantiate(thisDrop, transform.position, Quaternion.identity);
            itemDropped = true;
        }
    }
}
