using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject agent;

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
}
