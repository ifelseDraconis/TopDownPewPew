using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    [SerializeField, Tooltip("This is the impact graphic spawned on a hit.")]
    private GameObject hitSprite;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * 60.0f;
    }

    // this is some flashy nonsense that creates an impact graphic where the bullet hits
    public void goodHit(Collision collision)
    {
        ContactPoint contactpoint = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactpoint.normal);
        Vector3 position = contactpoint.point;
        GameObject thisHit = Instantiate(hitSprite, position, rotation);
        Destroy(gameObject);
        Destroy(thisHit, 0.25f);
    }
}
