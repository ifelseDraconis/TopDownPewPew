using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject agent;

    public GameObject agentContainer;

    [SerializeField, Tooltip("This is the sound of the foot steps.")]
    private AudioClip thisStepSound;    

    [Header("Weapons")]
    protected Weapon weapon;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    private void Step()
    {
        AudioManager.PlaySoundAt(thisStepSound, gameObject.transform, 0.2f);
    }

    public bool turnTowards(Vector3 thisTarget)
    {
        // this gets the direction that the player is in
        Vector3 direction = thisTarget - transform.position;

        // this converts it to degrees since Radians don't compute too well in Quaternions

        float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // this last part alters the rotation to match the y rotation
        transform.rotation = Quaternion.Euler(0, rotation, 0);


        return true;
    }
}
