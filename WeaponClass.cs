using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponClass : Weapon
{
    public GameObject thisWeaponContainer;

    [SerializeField, Tooltip("This is the movement animation.")]
    private Weapon.WeaponAnimationType thisWeaponAnim;

    [SerializeField, Tooltip("This is the movement animation.")]
    private WeaponAnimationType animationType;

    [SerializeField, Tooltip("This offsets the weapon the character holds.")]
    private Vector3 weaponOffset;

    public TextMeshProUGUI WeaponDigitalDisplayTxt;

    [SerializeField, Tooltip("This is the starting spot for this rifle's shots.")]
    private Transform PewPewBox;

    [SerializeField, Tooltip("This is the starting ammo for this rifle.")]
    private int Ammo;
        
    [SerializeField, Tooltip("Rate of Fire.")]
    private float fireDelta = 1.0f;

    [SerializeField, Tooltip("This is the bullet used for this rifle.")]
    private GameObject thisBullet;

    [SerializeField, Tooltip("This is the sound effect of the weapon firing.")]
    private AudioClip weaponShot;

    [SerializeField, Tooltip("This is the sound effect of the weapon empty.")]
    private AudioClip weaponEmpty;

    private GameObject newBullet;
    private float myTime;
    private float nextFire;

    private Collider thisPickupBox;
    private BobAndRotate thisAppleBob;
    private bool isEquipped;

    [SerializeField, Tooltip("This is the pickup turn on delay.")]
    private float pickupDelay;

    private WeaponClass thisWeapon;

    public override void Start()
    {
        thisWeapon = this;
        thisPickupBox = GetComponent<BoxCollider>();
        thisAppleBob = GetComponent<BobAndRotate>();
        base.Start();
    }

    // Update is called once per frame
    // this outputs the current ammo to the update tracker, because this portion of the canvas is attached to
    // the weapon itself, if the weapon is made invisible then so is this ammo display
    public override void Update()
    {
        if (isEquipped)
        {
            transform.position = thisWeaponContainer.transform.position + weaponOffset;
            transform.rotation = thisWeaponContainer.transform.rotation;
            if (WeaponDigitalDisplayTxt != null)
            {
                WeaponDigitalDisplayTxt.text = Ammo.ToString();
            }            
        }
        

        myTime += Time.deltaTime;

        

    }

    public void tryFiring()
    {
        if (myTime > nextFire && isEquipped && (Ammo > 0))
        {
            nextFire = myTime + fireDelta;
            // calls the fire script which 
            Fire();
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
        else if ( myTime > nextFire && isEquipped && (Ammo <= 0))
        {
            AudioManager.PlaySoundAt(weaponEmpty, PewPewBox.transform, 0.55f);
            nextFire = myTime + fireDelta;
            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }

    void Fire()
    {
        Debug.Log("Firing!!!!");

        AudioManager.PlaySoundAt(weaponShot, PewPewBox.transform, 0.8f);
        newBullet = Instantiate(thisBullet, PewPewBox.position, PewPewBox.rotation) as GameObject;
        // script to animate this rifle's bullets

        Ammo = Ammo - 1;
        WeaponDigitalDisplayTxt.text = Ammo.ToString();
    }

    public void addAmmo(bool smallOrBig)
    {
        if(smallOrBig)
        {
            Ammo += 30;
        }
        else
        {
            Ammo += 300;
        }
    }

    public void disableCollider()
    {
        thisPickupBox.enabled = false;        
    }

    private void enableCollider()
    {
        thisPickupBox.enabled = true;
    }

    private void enableBobAndRotate()
    {
        thisAppleBob.enabled = true;
    }

    private void disableBobAndRotate()
    {
        thisAppleBob.enabled = false;
    }

    public void reEnablePickup()
    {
        isEquipped = false;
        StartCoroutine("StageEnable", pickupDelay);
        enableCollider();
        enableBobAndRotate();
    }

    private void doEquip()
    {
        isEquipped = true;
        disableBobAndRotate();
        disableCollider();
    }


    // this makes use of the WaitForSeconds so that when the player discards the weapon they don't immediately pick it back up.
    IEnumerator StageEnable(float countDown)
    {
        yield return new WaitForSeconds(countDown);
    }

    // this lets the player pick up weapons
    private void OnCollisionEnter(Collision collision)
    {
        string thisHitMe = collision.collider.gameObject.tag;
        if (thisHitMe == "Player")
        {
            collision.collider.gameObject.transform.parent.gameObject.GetComponent<Player>().Equip(thisWeapon);
            thisWeaponContainer = collision.collider.gameObject.transform.parent.gameObject.GetComponent<Player>().equipHere();
            doEquip();
        }
    }
}
