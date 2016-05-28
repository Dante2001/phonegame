using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    private Rigidbody rigidbody;
    private Vector3 velocity;
	// Use this for initialization
	void Start () {
        rigidbody = this.GetComponent<Rigidbody>();
        velocity = new Vector3(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {

        velocity = Vector3.zero;
        if (Input.GetButton("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
                velocity.z = 5f;
            else if (Input.GetAxis("Vertical") < 0)
                velocity.z = -5f;
        }

        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
                velocity.x = 5f;
            else if (Input.GetAxis("Horizontal") < 0)
                velocity.x = -5f;
        }

        rigidbody.velocity = velocity;
	}
}
