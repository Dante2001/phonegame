using UnityEngine;
using System.Collections;

public class PhoneHolder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag.Equals ("Monster")) {
			float force = 300;
			float xForce;
			float zForce;
			if (col.transform.position.x > this.transform.position.x) {
				xForce = -force;
			} else {
				xForce = force;
			}
			if (col.transform.position.z > this.transform.position.z) {
				zForce = -force;
			} else {
				zForce = force;
			}

			GetComponent<Rigidbody> ().AddForce (xForce, 0, zForce);
			//GetComponent<Rigidbody> ().AddForceAtPosition(new Vector3(300, 0, 300),col.gameObject.transform.position);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
