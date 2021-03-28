using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    [SerializeField, Tooltip("This is the impact graphic spawned on a hit.")]
    private GameObject hitSprite;

    [SerializeField, Tooltip("This is the collider for the bullet.")]
    private Collider hitCapsle;

    private float activationDelay = 0.1f;
    private float currentActivation = 0f;

    private void Awake()
    {
        hitCapsle.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
        
        
    }

    // Update is called once per frame
    void Update()
    {

        // inputs that the game listens for even while paused

        if (GameManager.Paused == true)
        {
            return;
        }

        // code that the game only looks for when the game is unpaused

        if (currentActivation < activationDelay)
        {
            currentActivation += Time.deltaTime;
        }
        else if (hitCapsle.enabled == false)
        {
            hitCapsle.enabled = true;
        }
        transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * 60.0f;
    }

    // this is some flashy nonsense that creates an impact graphic where the bullet hits
    public void goodHit(Collision collision)
    {        
        ContactPoint contactpoint = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactpoint.normal);
        Vector3 position = contactpoint.point;
        GameObject thisHit = Instantiate(hitSprite, position, rotation);
        hitCapsle.enabled = false;
        Destroy(gameObject);
        Destroy(thisHit, 0.25f);
    }
}
