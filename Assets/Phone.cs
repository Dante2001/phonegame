using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	bool grabbable = false;

	// Use this for initialization
	void Start () {
		float explosionForce = 500;
		GetComponent<Rigidbody> ().AddExplosionForce (explosionForce, transform.position, 1);
		this.tag = "Untagged";
		grabbable = false;
		Invoke ("makeGrabbable", 2);
	}

	void makeGrabbable() {
		this.tag = "Phone";
		grabbable = true;
	}

	void OnTriggerEnter(Collider col) {
		//GRAB THE PHONE

		if (col.gameObject.tag.Equals ("Player") && grabbable) {
			GameManager.hasPhone = true;
		
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
