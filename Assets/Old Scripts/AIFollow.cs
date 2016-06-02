using UnityEngine;
using System.Collections;

public class AIFollow : MonoBehaviour {

    private NavMeshAgent navAgent;
    public GameObject sprite;
    public GameObject victim;
    public bool follow;
    private Vector3 position;
    private Animator animator;
    private float rotation;
	// Use this for initialization
	void Start () {
        position = new Vector3();
        position = sprite.transform.position;
        navAgent = this.GetComponent<NavMeshAgent>();
        animator = sprite.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (victim != null && follow)
            navAgent.SetDestination(victim.transform.position);
        else if (!follow && victim != null)
            navAgent.SetDestination(this.transform.position);
        
        position.x = this.transform.position.x;
        position.z = this.transform.position.z;
        sprite.transform.position = position;
        UpdateSprite();
	}

    void UpdateSprite()
    {
        rotation = this.transform.rotation.eulerAngles.y;

        if ((rotation >= 315f && rotation <= 0f) || rotation < 45f)
            animator.SetFloat("Direction", 0f);
        else if (rotation >= 45f && rotation < 135f)
            animator.SetFloat("Direction", 1f);
        else if (rotation >= 135f && rotation < 225f)
            animator.SetFloat("Direction", 0.5f);
        else // rotation >= 225f && rotation < 315f
            animator.SetFloat("Direction", 1.5f);         
    }

    public float GetRotation()
    {
        return rotation;
    }
}
