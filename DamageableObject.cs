using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : MonoBehaviour
{
    [SerializeField, Tooltip("This is the starting HP for the object.")]
    private float startingHealth;

    [SerializeField, Tooltip("This is the object's current max health.")]
    private float maxHealth;

    [SerializeField, Tooltip("This is the object's current health.")]
    private float currentHealth;

    [SerializeField, Tooltip("This is the death animation.")]
    private GameObject deathParticles;

    [SerializeField, Tooltip("This is the heal animation.")]
    private GameObject healAura;

    [Header("Events")]
    [SerializeField, Tooltip("Raised every time the object is healed.")]
    private UnityEvent onHeal;

    [SerializeField, Tooltip("Raised every time the object is damaged.")]
    private UnityEvent onDamage;

    [SerializeField, Tooltip("Raised once when the object's health reaches 0 or bellow.")]
    private UnityEvent onDie;

    [SerializeField, Tooltip("Raised every time when the object's max health changes.")]
    private UnityEvent changeMaxHP;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    // first mode raises the current HP to the new Max HP level
    // second mode increases or decreases the current HP on the basis of the delta for the new max/old max HP
    public void IncreaseMaxHealth(float newMax, bool newMode)
    {
        if(newMode)
        {
            maxHealth = newMax;
            currentHealth = maxHealth;
        }
        else
        {
            float additionalHealth = newMax - maxHealth;
            maxHealth = newMax;
            currentHealth += additionalHealth;
        }

        SendMessage("OnChangeMaxHP", SendMessageOptions.DontRequireReceiver);
        changeMaxHP.Invoke();
    }

    // reduces the health of the current damageable object, sending messages for damage each time, and death when required
    public void reduceHealth(float damageDone)
    {        
        damageDone = Mathf.Max(damageDone, 0f);
        currentHealth = Mathf.Clamp(currentHealth - damageDone, 0f, maxHealth);
        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        onDamage.Invoke();
        if (currentHealth == 0f)
        {
            makeKill();
        }

    }

    // adds health to the current damageable object
    public bool gainHealth(float healingDone)
    {
        
            healingDone = Mathf.Max(healingDone, 0f);
            currentHealth = Mathf.Clamp(currentHealth + healingDone, currentHealth, maxHealth);
            SendMessage("OnHeal", SendMessageOptions.DontRequireReceiver);
            onHeal.Invoke();
            return true;
          
    }

    public float getCurrentHP()
    {
        return currentHealth;
    }

    public float getMaxHP()
    {
        return maxHealth;
    }

    public void makeKill()
    {
        currentHealth = 0f;
        SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
        onDie.Invoke();        
    }

    public void spawnDeathTotem()
    {
        GameObject thisDeathSpray = Instantiate(deathParticles, gameObject.transform, false);
        Destroy(thisDeathSpray, 1.5f);
    }

    public void spawnHealAura()
    {
        GameObject thisDeathSpray = Instantiate(healAura, gameObject.transform, false);
        Destroy(thisDeathSpray, 1.5f);
    }
}
