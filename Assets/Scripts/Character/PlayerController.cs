using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    private CharacterState currentState;
    private CharacterDetails playerDetails;
    private CharacterDetails aiDetails;
    public GameObject goPhone;
    private GameObject instancePhone;
    private bool atPuzzleTerminal = false;
    private FirewallManager currentFirewall;

    public List<AudioClip> deathSFX;
    public List<AudioClip> attackSFX;
    public List<AudioClip> damagedSFX;
    public List<AudioClip> healSFX;
    public List<AudioClip> fireballSFX;
    public List<AudioClip> stunSFX;
    public List<AudioClip> puzzleTerminalSFX;
    public List<AudioClip> rollSFX;
    public List<AudioClip> chargingSFX;
    public List<AudioClip> spawnCubeSFX;
    public List<AudioClip> despawnCubeSFX;
    public List<AudioClip> winPuzzleSFX;
    public List<AudioClip> losePuzzleSFX;
    public List<AudioClip> otherAttack;
    public List<AudioClip> paiOther;
    public List<AudioClip> paiOther2;

    private Dictionary<string, List<AudioClip>> sfxDict;

    void Awake()
    {
        sfxDict = new Dictionary<string,List<AudioClip>>();
        sfxDict.Add("death", deathSFX);
        sfxDict.Add("attack", attackSFX);
        sfxDict.Add("damage", damagedSFX);
        sfxDict.Add("heal", healSFX);
        sfxDict.Add("fireball", fireballSFX);
        sfxDict.Add("stun", stunSFX);
        sfxDict.Add("puzzleTerminal", puzzleTerminalSFX);
        sfxDict.Add("roll", rollSFX);
        sfxDict.Add("charging", chargingSFX);
        sfxDict.Add("spawnCube", spawnCubeSFX);
        sfxDict.Add("despawnCube", despawnCubeSFX);
        sfxDict.Add("winPuzzle", winPuzzleSFX);
        sfxDict.Add("losePuzzle", losePuzzleSFX);
        sfxDict.Add("otherAttack", otherAttack);
        sfxDict.Add("paiOther", paiOther);
        sfxDict.Add("paiOther2", paiOther2);

    }

    // Use this for initialization
	void Start () {
        playerDetails = new CharacterDetails(this.GetComponent<NavMeshAgent>(), this.GetComponentInChildren<AttackHitboxLogic>(),
            this.GetComponentInChildren<RangedAttackLogic>(), this.GetComponentInChildren<StungunLogic>(), 
            this.GetComponent<Hitpoints>(), this.GetComponent<BatteryCharge>(), null, 
            this.GetComponent<PersonalSoundManager>(), sfxDict);
        
        aiDetails = new CharacterDetails(GameObject.Find("pai").GetComponent<NavMeshAgent>(),
            null, null, null, null, this.GetComponent<BatteryCharge>(),
            GameObject.Find("pai").GetComponent<CubeSpawnDespawner>(), 
            GameObject.Find("pai").GetComponent<PersonalSoundManager>(), sfxDict);

        playerDetails.animator = this.GetComponentInChildren<Animator>();
        aiDetails.animator = GameObject.Find("pai").GetComponentInChildren<Animator>();

        GameManager.playerDetails = playerDetails;
        GameManager.aiDetails = aiDetails;
        currentState = new DefaultState(playerDetails);
	}
	
	// Update is called once per frame
	void Update () {
        int x = 0;
        int z = 0;
	    if (Input.GetButton("Horizontal"))
        {
            x = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        }
        if (Input.GetButton("Vertical"))
        {
            z = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        }
        currentState.Move(x, z);
        if (!GameManager.isAI)
        {
            if (Input.GetButton("Roll"))
            {
                currentState.Roll(x, z);
            }
            if (Input.GetButton("Fire1"))
            {
                currentState.Attack();
            }

            if (GameManager.hasPhone)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    currentState.Shoot();
                }
                if (Input.GetButtonDown("Fire3"))
                {
                    currentState.Stungun();
                }
                if (Input.GetButtonDown("Heal"))
                {
                    currentState.Heal();
                }
                if (atPuzzleTerminal && Input.GetButtonDown("Interact"))
                {
                    currentState.Puzzle(currentFirewall);
                }
            }
        }
        else if (GameManager.isAI)
        {
            if (Input.GetButtonDown("SpawnCube"))
                currentState.SpawnCube();
            if (Input.GetButtonDown("DespawnCube"))
                currentState.DespawnCube();
        }
        if (!GameManager.isAI)
            currentState = currentState.CheckAlive();
        currentState = currentState.UpdateState();
        if (!GameManager.isAI)
            playerDetails.UpdateDetails();
        else
            aiDetails.UpdateDetails();
	}

    public void HitByMonster(GameObject attacker)
    {
		if (GameManager.hasPhone) {
			GameManager.hasPhone = false;
		    instancePhone = (GameObject)Instantiate (goPhone, this.gameObject.transform.position, transform.rotation);
		}
        else
        {
            playerDetails.LoseHP();
            if (!playerDetails.IsAlive())
                Respawn();
        }
        currentState.FlyBack(attacker);
        //Debug.Log("hitbymonster");
    }

    public void PickUpPhone()
    {
        if (instancePhone != null)
            Destroy(instancePhone);
    }

    public void SetRespawn(Transform respawn)
    {
        playerDetails.lastRespawn = respawn;
    }

    private void Respawn()
    {
        if (instancePhone != null)
            Destroy(instancePhone);
        GameManager.hasPhone = true;
    }

    public void UseChargePlate(bool onPlate)
    {
        if (GameManager.isAI)
            currentState.Charging(onPlate);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name.StartsWith("PuzzleTerminal"))
        {
            atPuzzleTerminal = true;
            currentFirewall = col.gameObject.GetComponent<FirewallManager>();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name.StartsWith("PuzzleTerminal"))
        {
            atPuzzleTerminal = false;
        }
    }
}
