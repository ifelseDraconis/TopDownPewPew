using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HumanoidPawn
{
    public Health health { get; private set; }
        
    // Start is called before the first frame update
    public override void Start()
    {
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
        Destroy(gameObject, 0.5f);
    }
}
