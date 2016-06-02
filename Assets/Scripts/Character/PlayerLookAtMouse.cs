using UnityEngine;
using System.Collections;

public class PlayerLookAtMouse : MonoBehaviour {

    private Plane playerPlane;
    private Ray ray;
    private Animator animator;
    public Transform player;
    public bool isAI;

	// Use this for initialization
	void Start () {
        animator = GameObject.Find("player").GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        playerPlane = new Plane(Vector3.up, player.position);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast (ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Vector3 direction = (targetPoint - player.position).normalized;
            direction.y = 0;
            GameManager.directionFromPlayerToMouse = direction;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (angle < 0)
                angle += 360;
            //Debug.Log(angle);
            int facing;
            if ((angle >= 315f && angle <= 0f) || angle < 45f)
            {
                animator.SetFloat("Direction", 0f);
                facing = 0;
            }
            else if (angle >= 45f && angle < 135f)
            {
                animator.SetFloat("Direction", 1f);
                facing = 1;
            }
            else if (angle >= 135f && angle < 225f)
            {
                animator.SetFloat("Direction", 0.5f);
                facing = 2;
            }
            else // rotation >= 225f && rotation < 315f
            {
                animator.SetFloat("Direction", 1.5f);
                facing = 3;
            }
            
            if (!isAI)
                GameManager.playerFacing = facing;
            else
                GameManager.aiFacing = facing;
        }

        
	}
}
