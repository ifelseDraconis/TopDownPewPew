using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ragdollController : MonoBehaviour
{
    [SerializeField, Tooltip("This is the animator for the character the ragdoll is attached to.")]
    private Animator thisAnimator;

    [SerializeField, Tooltip("This is the GameObject for the hips.")]
    private GameObject thisHipSet;

    [SerializeField, Tooltip("This is the ragdoll rigidbodies.")]
    private Rigidbody[] theseRagdollRigidbodies;

    [SerializeField, Tooltip("This is the ragdoll colliders.")]
    private Collider[] theseRagdollColliders;

    [SerializeField, Tooltip("This is the main Rigidbody for the character the ragdoll is attached to.")]
    private Rigidbody mainBodyRigidbody;

    [SerializeField, Tooltip("This is the main collider for the gameobject this script is attached to.")]
    private Collider mainBodyCollider;

    [SerializeField, Tooltip("This is a test for something being AI controlled and thus a mesh agent item.")]
    private bool isAnAI;

    [SerializeField, Tooltip("This is the MeshAgent item for an AI agent.")]
    private NavMeshAgent thisNavMeshController;

    // Start is called before the first frame update
    void Start()
    {
        theseRagdollColliders = thisHipSet.GetComponentsInChildren<Collider>();
        theseRagdollRigidbodies = thisHipSet.GetComponentsInChildren<Rigidbody>();
        deActivateRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateRagdoll()
    {
        /* ToDo coding something that makes all the ragdoll rigidbodies NOT Kinematic
     * Turn ON all of the ragdoll colliders
     * turn OFF the main collider on the object
     Make the main rigidBody kinematic
    Turn OFF the animator*/
        mainBodyCollider.enabled = false;

        mainBodyRigidbody.isKinematic = true;

        thisAnimator.enabled = false;

        if (isAnAI == true && thisNavMeshController != null)
        {
            thisNavMeshController.enabled = false;
        }

        foreach(Rigidbody thisRigidpart in theseRagdollRigidbodies)
        {
            thisRigidpart.isKinematic = false;
        }

        foreach(Collider thisColliderpart in theseRagdollColliders)
        {
            thisColliderpart.enabled = true;
        }
    }

    public void deActivateRagdoll()
    {
        /* Make all of the ragdoll rigidbodies Kinematic 
        * Turn OFF all of the ragdoll colliders
        * Turn ON the main collider on our object
        * Make the main rigidBody NOT Kinematic
        * Turn ON the animator*/

        mainBodyCollider.enabled = true;

        mainBodyRigidbody.isKinematic = false;

        thisAnimator.enabled = true;

        if (isAnAI == true && thisNavMeshController != null)
        {
            thisNavMeshController.enabled = true;
        }

        foreach (Rigidbody thisRigidpart in theseRagdollRigidbodies)
        {
            thisRigidpart.isKinematic = true;
        }

        foreach (Collider thisColliderpart in theseRagdollColliders)
        {
            thisColliderpart.enabled = false;
        }
    }
}
