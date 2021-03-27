using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : DamageableObject
{
    [SerializeField, Tooltip("The sound we make when we get hit.")]
    private AudioClip ouchThatHurt;

    public float Percent { get; private set; }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        float currentHealth = getCurrentHP();
        float maxHealth = getMaxHP();
        if (currentHealth <= 0)
        {
            
            base.makeKill();
        }
        Percent = currentHealth / maxHealth;
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string whatHitUs = collision.collider.gameObject.tag;

        if (whatHitUs == "Bullet")
        {
            AudioManager.PlaySoundAt(ouchThatHurt, gameObject.transform, 0.45f);

            if (collision.collider.gameObject.GetComponent<bulletMove>() != null)
            {
                bulletMove thisBullet = collision.collider.gameObject.GetComponent<bulletMove>();
                thisBullet.goodHit(collision);
            }
        }
        
    }
}
