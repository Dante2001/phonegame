using UnityEngine;
using System.Collections;

public class PlayerLookAtMouse : MonoBehaviour {

    private  Plane playerPlane;
    private Ray ray;
    private Animator animator;
    private int facing = 0; //0 up, 1 right, 2 down, 3 left

	// Use this for initialization
	void Start () {
        animator = this.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        playerPlane = new Plane(Vector3.up, transform.position);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast (ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Vector3 direction = (targetPoint - transform.position).normalized;
            direction.y = 0;
            GameManager.directionFromPlayerToMouse = direction;
        }

        
	}
}
