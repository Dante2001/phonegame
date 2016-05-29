using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private CharacterState currentState;
    private CharacterDetails playerDetails;

	// Use this for initialization
	void Start () {
        playerDetails = new CharacterDetails(this.GetComponent<Rigidbody>(), this.GetComponentInChildren<AttackHitboxLogic>(),
            this.GetComponentInChildren<RangedAttackLogic>());
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
        if (Input.GetButton("Roll"))
        {
            currentState.Roll(x, z);
        }
        if (Input.GetButton("Fire1"))
        {
            currentState.Attack();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            currentState.Shoot();
        }
        if (Input.GetButton("Sprint"))
        {
            currentState.Sprint();
        }

        currentState = currentState.UpdateState();
        playerDetails.UpdateDetails();
	}

    public void HitByMonster(GameObject attacker)
    {
        currentState.FlyBack(attacker);
        //Debug.Log("hitbymonster");
    }
}
