using UnityEngine;
using System.Collections;

public class RangedAttackLogic : MonoBehaviour {

    public GameObject bullet;
    public Transform target;

    public void FireBullet(GameObject t, float speed)
    {
        GameObject newBullet = (GameObject)Instantiate(bullet, this.transform.position, Quaternion.identity);
        newBullet.GetComponent<BulletMovement>().FireAtObjectAtSpeed(target.transform, speed);
    }
	
}
