using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private CharacterState currentState;
    private CharacterDetails playerDetails;
    private CharacterDetails aiDetails;
    public GameObject goPhone;
    private GameObject instancePhone;
    private bool atPuzzleTerminal = false;
    private FirewallManager currentFirewall;

	// Use this for initialization
	void Start () {
        playerDetails = new CharacterDetails(this.GetComponent<NavMeshAgent>(), this.GetComponentInChildren<AttackHitboxLogic>(),
            this.GetComponentInChildren<RangedAttackLogic>(), this.GetComponentInChildren<StungunLogic>(), 
            this.GetComponent<Hitpoints>(), this.GetComponent<BatteryCharge>(), null);
        
        aiDetails = new CharacterDetails(GameObject.Find("pai").GetComponent<NavMeshAgent>(),
            null, null, null, null, this.GetComponent<BatteryCharge>(), 
            GameObject.Find("pai").GetComponent<CubeSpawnDespawner>());

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
