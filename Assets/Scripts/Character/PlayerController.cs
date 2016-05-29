using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private CharacterState currentState;
    private CharacterDetails playerDetails;
	public GameObject goPhone;

	public void hitByMonster(GameObject goEnemy) {
		if (GameManager.hasPhone) {
			GameManager.hasPhone = false;
			Instantiate (goPhone, this.gameObject.transform.position, transform.rotation);
		}

		float force = 1200;
		float xForce;
		float zForce;
		if (goEnemy.transform.position.x > this.transform.position.x) {
			xForce = -force;
		} else {
			xForce = force;
		}
		if (goEnemy.transform.position.z > this.transform.position.z) {
			zForce = -force;
		} else {
			zForce = force;
		}

		GetComponent<Rigidbody> ().AddForce (xForce, force/2, zForce);
		//GetComponent<Rigidbody> ().AddForceAtPosition(new Vector3(300, 0, 300),col.gameObject.transform.position);

	}

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
}
