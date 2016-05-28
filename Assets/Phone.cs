using UnityEngine;
using System.Collections;

public class Phone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag.Equals ("")) {
			GameManager.hasPhone = true;
		}
		Destroy (this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
