using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HumanoidPawn
{
    public Health health { get; private set; }

    [SerializeField, Tooltip("This is the sound effect of the weapon empty.")]
    private PlayerUI playerUI;


    // Start is called before the first frame update
    public override void Start()
    {
        if (playerUI == null)
        {
            GameObject thisUIContainer = GameObject.FindGameObjectWithTag("CameraOverlay");
            playerUI = thisUIContainer.GetComponent<PlayerUI>();
        }
        base.Start();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
        base.Update();        
    }

    public void pickMeUp(WeaponClass weapon)
    {
        base.Equip(weapon);
    }

    public void makeMeDead()
    {
        Destroy(base.agentContainer, 6.5f);
        Destroy(gameObject, 6.5f);
        //gameObject.GetComponent<Player>().enabled = false;
    }
}
