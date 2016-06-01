using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

	void Start() {
		Invoke("DestroySelf",2f);
	}

	void DestroySelf() {
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag.Equals ("Monster")) {
			try {
			//stun monster
			col.gameObject.GetComponent<Monster>().GetStunned();
			Destroy(this.gameObject);
			}
			catch (System.Exception ex) {

			}
		}
	}

    public void FireAtObjectAtSpeed(Vector3 direction, float speed)
    {
        Vector3 vel = direction;
        vel *= speed;
        vel.y = 0f;
        this.GetComponent<Rigidbody>().velocity = vel;
    }
}
