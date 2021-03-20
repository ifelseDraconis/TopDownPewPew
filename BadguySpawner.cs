using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadguySpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Baddie to spawn.")]
    private GameObject thisBaddie;

    [SerializeField, Tooltip("Baddie container.")]
    private GameObject thisBaddieActual;

    [SerializeField, Tooltip("Baddie spawn cooldown.")]
    private float respawnCooldown;

    [SerializeField, Tooltip("Baddie monitor time.")]
    private float cooldownTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // checks to see if the badguy exists
        if (thisBaddieActual == null)
        {
            // recreates and resets the timer if the badguy doesn't exist and the cooldown is up
            if (cooldownTime >= respawnCooldown)
            {
                createBaddie();
                cooldownTime = 0;
            }
            else
            {
                cooldownTime += Time.deltaTime;
            }
        }
    }

    // creates the bad guy when called
    public void createBaddie()
    {
        thisBaddieActual = Instantiate(thisBaddie, transform, false);
    }
}
