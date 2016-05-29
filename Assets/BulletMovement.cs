using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	
    public void FireAtObjectAtSpeed(Transform t, float speed)
    {
        Vector3 vel = (t.position - this.transform.position).normalized;
        vel *= speed;
        vel.y = 0f;
        this.GetComponent<Rigidbody>().velocity = vel;
    }
}
