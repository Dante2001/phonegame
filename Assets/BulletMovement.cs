using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	
    public void FireAtObjectAtSpeed(Vector3 direction, float speed)
    {
        Vector3 vel = direction;
        vel *= speed;
        vel.y = 0f;
        this.GetComponent<Rigidbody>().velocity = vel;
    }
}
