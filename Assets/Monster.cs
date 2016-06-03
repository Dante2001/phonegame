using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : MonoBehaviour {

	bool nearPlayer = false;
	bool stunned = false;
    public int hitpoints = 3;
    public float speed = 3.5f;
    public float acceleration = 100f;
    public MonsterSpinLogic spinLogic;
    public AIFollow aifollow;
    public NavMeshAgent navAgent;
    public PersonalSoundManager sound;
    public List<AudioClip> attackSfx;
    public List<AudioClip> hitSfx;

	public void GetHit() {
        Debug.Log("gothit");
        sound.PlayEfxRandom(hitSfx);
		//lose hp, then be destroyed
        hitpoints -= 1;
        if (hitpoints <= 0)
    		Destroy (this.transform.parent.gameObject);
	}

	public void GetStunned() {
		CancelInvoke ();
		stunned = true;
        Debug.Log("STUN");
		Invoke ("RecoverFromStun", 3f);
        aifollow.follow = false;
		GetComponent<Animator> ().SetBool ("Attacking", false);
        GetComponent<Animator>().SetBool("SpinAttack", false);
        sound.PlayEfxRandom(hitSfx);
	}

	void RecoverFromStun() {
		stunned = false;
        aifollow.follow = true;
	}

	public void attackFinished() {
        Debug.Log("Attacked");
        if (!stunned)
            aifollow.follow = true;
		GetComponent<Animator> ().SetBool ("Attacking", false);        
		if (nearPlayer && !stunned) {
			GameObject.Find ("player").GetComponent<PlayerController> ().HitByMonster (this.gameObject);
		}
	}

    public void spinFinished()
    {
        if (!stunned)
            aifollow.follow = true;
        GetComponent<Animator>().SetBool("SpinAttack", false);
        if (spinLogic.IsNear() && !stunned)
        {
            GameObject.Find("player").GetComponent<PlayerController>().HitByMonster(this.gameObject);
        }
    }

	void OnTriggerExit(Collider col) {
		Debug.Log ("EXIT");
		nearPlayer = false;
	}
	void OnTriggerEnter(Collider col) {
			Debug.Log ("ENTER");
			nearPlayer = true;
		if (!stunned) {
			if (col.gameObject.tag.Equals ("Player")) {
				GetComponent<Animator> ().SetBool ("Attacking", true);
                sound.PlayEfxRandom(attackSfx);
                aifollow.follow = false;
			}
		}
	}

    public bool AttemptSpinAttack()
    {
        if (!stunned)
        {
            CancelInvoke();
            GetComponent<Animator>().SetBool("SpinAttack", true);
            return true;           
        }
        return false;
    }

    void SetHitBox()
    {
        float rotation = aifollow.GetRotation();
        if ((rotation >= 315f && rotation <= 0f) || rotation < 45f)
        {
            this.GetComponent<CapsuleCollider>().center = new Vector3(1f, 0f, 1f);
        }
        else if (rotation >= 45f && rotation < 135f)
            this.GetComponent<CapsuleCollider>().center = new Vector3(1f, 0f, 1f);
        else if (rotation >= 135f && rotation < 225f)
            this.GetComponent<CapsuleCollider>().center = new Vector3(-1f, 0f, 1f);
        else // rotation >= 225f && rotation < 315f
            this.GetComponent<CapsuleCollider>().center = new Vector3(-1f, 0f, 1f);  
    }

	void OnTriggerStay(Collider col) {
		if (!stunned) {
			if (col.gameObject.tag.Equals ("Player")) {
				GetComponent<Animator> ().SetBool ("Attacking", true);
                sound.PlayEfxRandom(attackSfx);
                aifollow.follow = false;
			}
		}
	}

    public void Respawn()
    {
        CancelInvoke();
        aifollow.follow = false;
    }

	// Use this for initialization
	void Start () {
        sound = this.GetComponent<PersonalSoundManager>();
        navAgent.speed = speed;
        navAgent.acceleration = acceleration;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetBool("IsFollowing", aifollow.follow);
        SetHitBox();
	}
}
