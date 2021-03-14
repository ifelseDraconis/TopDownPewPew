using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public enum WeaponAnimationType
    {
        None = 0,
        Rifle = 1,
        Pistol = 2
    }

    [Header("IK Points")]
    public Transform rightHandPoint;
    public Transform leftHandPoint;

    [Header("Firing Events")]
    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackStop;
    public UnityEvent OnAltAttackStart;
    public UnityEvent OnAltAttackStop;

    public Transform rightHandTarget { get; set; }
    public Transform leftHandTarget { get; set; }

    [SerializeField, Tooltip("This is the firearm prefab.")]
    public GameObject thisFireArm;    

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void StartAttack()
    {
        // this is the start of the attack
    }

    public virtual void StopAttack()
    {
        // this is the end of the attack
    }

    
}
