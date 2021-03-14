using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : DamageableObject
{

    public float Percent { get; private set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        float currentHealth = getCurrentHP();
        float maxHealth = getMaxHP();
        Percent = currentHealth / maxHealth;
        base.Update();
    }
}
