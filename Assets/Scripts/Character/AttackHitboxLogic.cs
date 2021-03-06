﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackHitboxLogic : MonoBehaviour {

    private List<GameObject> hitObjects;
    private bool active = false;

	// Use this for initialization
	void Start () {
        hitObjects = new List<GameObject>();
        //this.GetComponent<MeshRenderer>().enabled = false;
	}

    public void Activate(int x, int z)
    {
        //this.GetComponent<MeshRenderer>().enabled = true;
        active = true;
        Vector3 position;
        position.y = this.transform.localPosition.y;
        hitObjects.Clear();
        if (GameManager.playerFacing == 0)
        {
            position.z = 1;
            position.x = 0;
            this.transform.localPosition = position;
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (GameManager.playerFacing == 2)
        {
            position.z = -1;
            position.x = 0;
            this.transform.localPosition = position;
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (GameManager.playerFacing == 1)
        {
            position.z = 0;
            position.x = 1;
            this.transform.localPosition = position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (GameManager.playerFacing == 3)
        {
            position.z = 0;
            position.x = -1;
            this.transform.localPosition = position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Deactivate()
    {
        //this.GetComponent<MeshRenderer>().enabled = false;
        active = false;
    }

    void OnTriggerEnter(Collider col)
    {
		if (!hitObjects.Contains(col.gameObject) && active && col.gameObject.tag.Equals("Monster"))
        {
            hitObjects.Add(col.gameObject);
            col.transform.root.GetComponentInChildren<Monster>().GetHit();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!hitObjects.Contains(col.gameObject) && active && col.gameObject.tag.Equals("Monster"))
        {
            hitObjects.Add(col.gameObject);
            col.transform.root.GetComponentInChildren<Monster>().GetHit();
        }
    }

}
