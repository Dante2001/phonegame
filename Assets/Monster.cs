using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	bool nearPlayer = false;
	bool stunned = false;

	public void GetHit(GameObject sword) {
		//lose hp, then be destroyed
		Destroy (this.gameObject);
	}

	public void GetStunned() {
		CancelInvoke ();
		stunned = true;
		Invoke ("RecoverFromStun", 1f);
		GetComponent<Animator> ().SetBool ("Attacking", false);

	}

	void RecoverFromStun() {
		stunned = false;
	}

	public void attackFinished() {
		GetComponent<Animator> ().SetBool ("Attacking", false);
		if (nearPlayer) {
			GameObject.Find ("player").GetComponent<PlayerController> ().HitByMonster (this.gameObject);
		}
	}

	void OnTriggerExit(Collider col) {
		Debug.Log ("EXIT");
		nearPlayer = false;
	}
	void OnTriggerEnter(Collider col) {
			Debug.Log ("ENTER");
			nearPlayer = true;
		if (col.gameObject.tag.Equals ("Weapon")) {
			GetHit (col.gameObject);
		}
		if (!stunned) {
			if (col.gameObject.tag.Equals ("Player")) {
				GetComponent<Animator> ().SetBool ("Attacking", true);
			}
		}

	}


	void OnTriggerStay(Collider col) {
		if (!stunned) {
			if (col.gameObject.tag.Equals ("Player")) {
				GetComponent<Animator> ().SetBool ("Attacking", true);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
