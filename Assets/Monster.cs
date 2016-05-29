using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	bool nearPlayer = false;

	public void attackFinished() {
		GetComponent<Animator> ().SetBool ("Attacking", false);
		if (nearPlayer) {
			GameObject.Find ("player").GetComponent<PlayerController> ().hitByMonster (this.gameObject);
		}
	}

	void OnTriggerExit(Collider col) {
		nearPlayer = false;
	}
	void OnTriggerEnter(Collider col) {
		nearPlayer = true;
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
