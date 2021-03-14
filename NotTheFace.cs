using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotTheFace : DamageableObject
{
    [SerializeField, Tooltip("HitQue")]
    private AudioClip weGotDinked;

    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
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
                bulletMove thisBullet = collision.collider.gameObject.GetComponent<bulletMove>();
                thisBullet.goodHit(collision);
            }
        }
    }

    public void goodByeCruelWorld()
    {
        Destroy(gameObject, 0.5f);
        Destroy(gameObject.transform.parent.gameObject, 0.6f);
    }
}