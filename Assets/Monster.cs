using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public void attackFinished() {
		GetComponent<Animator> ().SetBool ("Attacking", false);
		GameObject.Find ("player").GetComponent<PhoneHolder>().wasAttacked(this.gameObject);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag.Equals ("Player")) {
			GetComponent<Animator> ().SetBool ("Attacking", true);
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
