using UnityEngine;
using System.Collections;

public class PhoneHolder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void wasAttacked(GameObject goEnemy) {
		GameManager.hasPhone = false;
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

/*
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag.Equals ("Monster")) {
			attackingUnit = col.gameObject;
		}
	}
*/
	// Update is called once per frame
	void Update () {
	
	}
}
