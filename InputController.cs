using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Controller
{
    [SerializeField, Tooltip("This is the player class on the pawn")]
    private HumanoidPawn thisPlayer;
    
    private Vector3 goesYander;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        
    }

    // Update is called once per frame
    public override void Update()
    {  

        // converts the input vector into a localized movement modifier
        goesYander = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Vector3 animatorVect = transform.InverseTransformDirection(goesYander);

        if (Input.GetButton("Fire1") == true)
           {
            if (thisPlayer.equippedWeapon != null)
            {
                thisPlayer.equippedWeapon.tryFiring();
            }
            
           }
        // declares the float result to the animation controller
        base.thisAnimate.SetFloat("Forward", animatorVect.z);
        base.thisAnimate.SetFloat("Right", animatorVect.x);
        base.thisAnimate.SetFloat("MoveSpeed", base.moveSpeed);
        base.Update();
    }
}
