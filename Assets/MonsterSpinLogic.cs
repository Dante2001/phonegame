using UnityEngine;
using System.Collections;

public class MonsterSpinLogic : MonoBehaviour {

    public Monster monsterLogic;
    private float chanceToSpin = 80f; //10 percent
    private float coolDown = 6f;
    private bool attacked = false;
    private float timer;
    private bool isNear;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (attacked && timer > 0)
            timer -= Time.deltaTime;
        else
            attacked = false;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "player")
            isNear = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "player")
            isNear = false;
    }

    public bool IsNear()
    {
        return isNear;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.name == "player")
        {
            isNear = true;
            if (!attacked && Random.Range(0, 100) > chanceToSpin)
            {
                if (monsterLogic.AttemptSpinAttack())
                {
                    attacked = true;
                    timer = coolDown;
                }
            }
        }
    }
}
