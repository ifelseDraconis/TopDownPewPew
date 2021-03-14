using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidPawn : Pawn
{
    // the Animator
    public Animator thisAnimator;

    [Header("Data")]
    public float speed = 1f;

    public WeaponClass equippedWeapon;

    [SerializeField, Tooltip("")]
    private GameObject thisWeaponContainer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    
    // this equips a weapon, removing the currently equipped weapon if it exists
    public void Equip(WeaponClass weapon)
    {
        //if (equippedWeapon != null)
        //{
        //    Unequip();
        //}
        equippedWeapon = weapon;
    }

    public GameObject equipHere()
    {        
        return thisWeaponContainer;
    }

    // this destroys the gun when it is called
    public void Unequip()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.reEnablePickup();
            equippedWeapon = null;
        }
    }


    // this induces the inverseKinematics to make the agent reach for the gun
    protected virtual void OnAnimatorIK()
    {
        if (equippedWeapon == null)
        {
            
            //Debug.Log("Can't reach the code.");
            return;
        }

        if (equippedWeapon.rightHandPoint != null)
        {
            thisAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            thisAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            thisAnimator.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.position);            
            thisAnimator.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.rotation);
        }
        else
        {
            thisAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            thisAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }

        if (equippedWeapon.leftHandPoint != null)
        {
            thisAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            thisAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
            thisAnimator.SetIKPosition(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.position);            
            thisAnimator.SetIKRotation(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.rotation);
            
        }
        else
        {
            thisAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            thisAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
        }
    }

    // this will only return true to destory the game object if the player can pick up ammo
    public bool letsAddAmmo(bool smallOrBig)
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.addAmmo(smallOrBig);
            return true;
        }
        return false;
    }
}
