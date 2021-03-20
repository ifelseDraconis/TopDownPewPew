using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    [Header("Targets")]
    [SerializeField, Tooltip("This provides the Player target.")]
    private GameObject PlayerTarget;

    [SerializeField, Tooltip("This is the weapon that the Agent is after.")]
    private GameObject WeaponTarget;

    [SerializeField, Tooltip("This is the target pickup.")]
    private GameObject PickupTarget;

    private bool needsToGetAmmo = false;
    private bool stateCurrent = false;

    [Header("Navigator Mesh Agent")]
    [SerializeField, Tooltip("This is the NavMeshAgent for the controller.")]
    private NavMeshAgent thisPawnDirector;

    [Header("AI Conduct")]
    [SerializeField, Tooltip("This is the amount of time between AI update checks.")]
    private float timeBeforeAICheck;

    [SerializeField, Tooltip("This is the amount of time the AI retains a behavior that hasn't born a result yet.")]
    private float timeBeforeAILeavesAction;

    [SerializeField, Tooltip("This is the amount of distance the AI retains before firing at the player.")]
    private float distanceFromPlayerDuringShooting; 

    private float currentTimer = 0f;
    private float secondaryTimer = 0f;

    [SerializeField, Tooltip("This is the NavMeshAgent for the controller.")]
    private thisAIState currentAIState;

    [Header("Humanoid Pawn Connection")]
    [SerializeField, Tooltip("This is the Humanoid Pawn for this agent.")]
    private HumanoidPawn thisPawnBody;

    [Header("The hitpoints and hit box")]
    [SerializeField, Tooltip("This covers the character actor for the agent.")]
    private NotTheFace hurtableBody;

    private Vector3 desiredVelocity;


    public enum thisAIState
    {
        Nothing = 0,
        LookForGun = 1,
        LookForHealth = 2,
        LookForAmmo = 3,
        LookForPlayer = 4,
        ShootAtPlayer = 5
    }

    public void Awake()
    {
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        currentTimer += Time.deltaTime;
        secondaryTimer += Time.deltaTime;

        // breaks the current AI state if it has gone on too long
        if (secondaryTimer >= timeBeforeAILeavesAction)
        {
            currentAIState = thisAIState.Nothing;
            secondaryTimer = 0f;
        }

        if (currentTimer >= timeBeforeAICheck)
        {
            updateAIBehavior();
            currentTimer = 0f;
        }

        // updates the current state of the AI
        if (stateCurrent == false) 
        {
            switch (currentAIState)
            {
                case thisAIState.Nothing:
                    // does nothing, exists this condition by seeking update through AI Behavior logics
                    break;

                case thisAIState.LookForGun:
                    // sends the agent after a firearm
                    if (WeaponTarget != null)
                    {
                        reachForGun();
                    }
                    else
                    {
                        currentAIState = thisAIState.Nothing;
                    }
                    break;

                case thisAIState.LookForAmmo:
                    if (PickupTarget != null)
                    {
                        reachForAmmo();
                    }
                    else
                    {
                        currentAIState = thisAIState.Nothing;
                    }
                    break;

                case thisAIState.LookForHealth:
                    // sends the agent after health
                    if (PickupTarget != null)
                    {
                        reachForHealth();
                    }
                    else
                    {
                        currentAIState = thisAIState.Nothing;
                    }
                    break;

                case thisAIState.LookForPlayer:
                    // sends the agent after the player
                    if (PlayerTarget != null)
                    {
                        chasePlayer();
                    }
                    else
                    {
                        currentAIState = thisAIState.Nothing;
                    }                    
                    break;

                case thisAIState.ShootAtPlayer:
                    // tells the agent to shoot at the player
                    // add a raycast check so the agent doesn't shoot through walls
                    if (PlayerTarget != null)
                    {
                        shootAtPlayer();
                    }
                    else
                    {
                        currentAIState = thisAIState.Nothing;
                    }
                    break;

                default:
                    Debug.Log("Are we failing out?");
                    currentAIState = thisAIState.Nothing;
                    break;
            }
            stateCurrent = true;

        }
        // the switch determines what the AI Controller is having the agent do

        makeMovement();
        Vector3 input = thisPawnDirector.desiredVelocity;
        input = transform.InverseTransformDirection(input);
        thisAnimate.SetFloat("Horizontal", input.x);
        thisAnimate.SetFloat("Vertical", input.y);
        

        thisPawnDirector.autoRepath = true;
        
        if (currentAIState == thisAIState.ShootAtPlayer)
        {
            shootAtPlayer();
        }

        base.Update();
    }

    // fires off every so often to change the AI behavior
    private void updateAIBehavior()
    {
        bool getHealth = false;
        bool getFirearm = false;
        bool findWeapon = false;

        getHealth = needsHealth();
        getFirearm = needsAFirearm();

        // highest priority is given to finding health if health is low
        if (getHealth == false)
        {
            // second highest is given to finding a weapon / ammo
            if (getFirearm == true)
            {
                findWeapon = foundWeapon();
                if (findWeapon)
                {
                    currentAIState = thisAIState.LookForGun;
                }
            }
            else
            {
                // third highest is given to finding the player
                if (thisPawnBody.equippedWeapon != null)
                {
                    needsToGetAmmo = needsAmmo();
                    if (PlayerTarget == null)
                    {
                        foundPlayer();
                    }
                    if (needsToGetAmmo == false)
                    {
                        if (distanceFromPlayerDuringShooting <= Vector3.Distance(transform.position, PlayerTarget.transform.position))
                        {
                            // shoot at the player
                            currentAIState = thisAIState.ShootAtPlayer;
                        }
                        else
                        {
                            // get the player in range
                            currentAIState = thisAIState.LookForPlayer;
                        }
                    }
                    else
                    {
                        // sends the agent after ammo
                        currentAIState = thisAIState.LookForAmmo;
                    }
                }
                
            }
        }
        else
        {
            currentAIState = thisAIState.LookForHealth;
        }
        stateCurrent = false;
    }

    // determines if the agent needs to seek health
    private bool needsHealth()
    {
        if (hurtableBody == null)
        {
            return false;
        }
        if (hurtableBody.getCurrentHP() < (hurtableBody.getMaxHP()/2))
        {
            if(foundHealth())
            {
                return true;
            }
            return false;
        }
        return false;
    }

    // determines if the agent needs to seek a firearm
    private bool needsAFirearm()
    {
        if (thisPawnBody.equippedWeapon == null)
        {
            return true;
        }
        if (thisPawnBody.equippedWeapon.checkAmmo() <= 0)
        {
            if (needsAmmo())
            {
                return false;
            }
        }
        return false;
    }

    // checks the amount of ammo in the gun, this is staged so it only calls if a firearm is already equipped
    private bool needsAmmo()
    {
        if(thisPawnBody.equippedWeapon.checkAmmo() <= 40)
        {
            needsToGetAmmo = true;
            if (foundAmmo())
            {
                return false;
            }
            return true;
        }
        needsToGetAmmo = false;
        return false;
    }

    // sends the agent to find health
    private bool foundHealth()
    {
        GameObject thisNewHealth = null;
        GameObject[] thesePossibleHealths = null;

        Transform thisLocation = gameObject.transform;

        thesePossibleHealths = GameObject.FindGameObjectsWithTag("Ammo");

        foreach (GameObject HealthSpot in thesePossibleHealths)
        {
            if (thisNewHealth == null)
            {
                thisNewHealth = HealthSpot;
            }
            else
            {
                float distA = 0f;
                float distB = 0f;
                distA = Vector3.Distance(thisLocation.position, thisNewHealth.transform.position);
                distB = Vector3.Distance(thisLocation.position, HealthSpot.transform.position);
                if (distA > distB)
                {
                    thisNewHealth = HealthSpot;
                }
            }
        }
        if (thisNewHealth != null)
        {
            PickupTarget = thisNewHealth;
            return true;
        }
        return false;
    }

    // this searches through all objects with the ammo tag to find the closest one before directing the AI after it
    private bool foundAmmo()
    {
        GameObject thisNewAmmo = null;
        GameObject[] thesePossibleAmmos = null;

        Transform thisLocation = gameObject.transform;

        thesePossibleAmmos = GameObject.FindGameObjectsWithTag("Ammo");

        foreach (GameObject AmmoSpot in thesePossibleAmmos)
        {
            if (thisNewAmmo == null)
            {
                thisNewAmmo = AmmoSpot;
            }
            else
            {
                float distA = 0f;
                float distB = 0f;
                distA = Vector3.Distance(thisLocation.position, thisNewAmmo.transform.position);
                distB = Vector3.Distance(thisLocation.position, AmmoSpot.transform.position);
                if (distA > distB)
                {
                    thisNewAmmo = AmmoSpot;
                }
            }
        }
        if (thisNewAmmo != null)
        {
            PickupTarget = thisNewAmmo;
            return true;
        }
        return false;
    }

    // returns the location of the nearest weapon for the agent
    private bool foundWeapon()
    {
        GameObject thisNewWeapon = null;
        GameObject[] thesePossibleWeaponss = null;

        Transform thisLocation = gameObject.transform;

        thesePossibleWeaponss = GameObject.FindGameObjectsWithTag("Weapon");

        foreach (GameObject WeaponSpot in thesePossibleWeaponss)
        {
            if (thisNewWeapon == null)
            {
                thisNewWeapon = WeaponSpot;
            }
            else
            {
                float distA = 0f;
                float distB = 0f;
                distA = Vector3.Distance(thisLocation.position, thisNewWeapon.transform.position);
                distB = Vector3.Distance(thisLocation.position, WeaponSpot.transform.position);
                if (distA > distB)
                {
                    thisNewWeapon = WeaponSpot;
                }
            }
        }
        if (thisNewWeapon != null)
        {
            // where is the gun Johnny?
            Debug.Log("There is a gun here x:" + thisNewWeapon.transform.position.x + ", y:" + thisNewWeapon.transform.position.y + ", z:" + thisNewWeapon.transform.position.z);
            WeaponTarget = thisNewWeapon;
            return true;
        }
        else
        {
            Debug.Log("Nobody home boss.");
        }
        return false;
    }

    // looks for all objects with the player tag, and then goes after the nearest one if there is one
    private bool foundPlayer()
    {
        GameObject thisNewPlayer = null;
        GameObject[] thesePossiblePlayers;

        Transform thisLocation = gameObject.transform;

        thesePossiblePlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject PlayerSpot in thesePossiblePlayers)
        {
            if (thisNewPlayer == null)
            {
                thisNewPlayer = PlayerSpot;
            }
            else
            {
                float distA = 0f;
                float distB = 0f;
                distA = Vector3.Distance(thisLocation.position, thisNewPlayer.transform.position);
                distB = Vector3.Distance(thisLocation.position, PlayerSpot.transform.position);
                if (distA > distB)
                {
                    thisNewPlayer = PlayerSpot;
                }
            }
        }
        if (thisNewPlayer != null)
        {
            PlayerTarget = thisNewPlayer;
            return true;
        }
        return false;
    }

    // sends the agent after a gun
    private void reachForGun()
    {
        thisPawnDirector.stoppingDistance = 0f;
        thisPawnDirector.SetDestination(WeaponTarget.transform.position);
    }

    // sends the agent after health
    private void reachForHealth()
    {
        thisPawnDirector.stoppingDistance = 0f;
        thisPawnDirector.SetDestination(PickupTarget.transform.position);
    }

    private void reachForAmmo()
    {
        thisPawnDirector.stoppingDistance = 0f;
        thisPawnDirector.SetDestination(PickupTarget.transform.position);
    }

    // sends the agent after the player
    private void chasePlayer()
    {
        // this gives a bit of variety to the stopping distance when called so that the agents aren't making semi circles around the player
        thisPawnDirector.stoppingDistance = Mathf.Clamp(distanceFromPlayerDuringShooting + Random.Range(-12.0f, 6.0f), 0f, distanceFromPlayerDuringShooting + 6f);
        thisPawnDirector.SetDestination(PlayerTarget.transform.position);
    }

    private void makeMovement()
    {
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, thisPawnDirector.desiredVelocity, thisPawnDirector.acceleration * Time.deltaTime);
    }

    // tells the agent to shoot at the player
    private void shootAtPlayer()
    {
        bool aimedAtPlayer = false;
        bool distanceBetweenPlayer = false;

        if (PlayerTarget != null)
        {
            //ToDo code to point and shoot at the player
            float currentDistance = Vector3.Distance(gameObject.transform.position, PlayerTarget.transform.position);

            distanceBetweenPlayer = (currentDistance >= thisPawnDirector.stoppingDistance);

            thisPawnDirector.isStopped = true;
            // command pawn to turn and look at the player
            aimedAtPlayer = base.pawn.turnTowards(PlayerTarget.transform.position);

            if (true)
            {
                thisPawnBody.tryFiring();               
            }

            thisPawnDirector.isStopped = false;
        }        
    }

    public void pickedUpWeapon()
    {
        needsToGetAmmo = false;
        currentAIState = thisAIState.Nothing;
        updateAIBehavior();
    }

    public void pickedUpHealth()
    {
        currentAIState = thisAIState.Nothing;
        updateAIBehavior();
    }

    private void OnAnimatorMove()
    {
        thisPawnDirector.velocity = thisAnimate.velocity;
        desiredVelocity = thisPawnDirector.velocity;
    }

    IEnumerator BurstFire(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
    }
}
