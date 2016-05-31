using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private CharacterState currentState;
    private CharacterDetails playerDetails;
	public GameObject goPhone;
    private GameObject instancePhone;

	// Use this for initialization
	void Start () {
        playerDetails = new CharacterDetails(this.GetComponent<Rigidbody>(), this.GetComponentInChildren<AttackHitboxLogic>(),
            this.GetComponentInChildren<RangedAttackLogic>(), this.GetComponentInChildren<StungunLogic>(), 
            this.GetComponentInChildren<Hitpoints>(), this.GetComponentInChildren<BatteryCharge>());
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
            }
        }       

        currentState = currentState.CheckAlive();
        currentState = currentState.UpdateState();
        playerDetails.UpdateDetails();
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
}
