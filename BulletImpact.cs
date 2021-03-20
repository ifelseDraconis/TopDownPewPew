using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField, Tooltip("The sound the material makes when hit.")]
    private AudioClip weGotDinked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        string whatHitUs = collision.collider.gameObject.tag;

        if (whatHitUs == "Bullet")
        {
            AudioManager.PlaySoundAt(weGotDinked, gameObject.transform, 0.25f);

            if (collision.collider.gameObject.GetComponent<bulletMove>() != null)
            {
                bulletMove thisBullet = collision.collider.gameObject.GetComponent<bulletMove>();
                thisBullet.goodHit(collision);
            }
        }
    }
}
