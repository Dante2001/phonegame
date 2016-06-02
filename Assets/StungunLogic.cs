using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StungunLogic : MonoBehaviour {

    private List<GameObject> hitObjects;
    private bool active = false;

    // Use this for initialization
    void Start()
    {
        hitObjects = new List<GameObject>();
        //this.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Activate(int x, int z)
    {
        //this.GetComponent<MeshRenderer>().enabled = true;
        active = true;
        hitObjects.Clear();
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
            try
            {
                //stun monster
                col.transform.root.GetComponentInChildren<Monster>().GetStunned();
                hitObjects.Add(col.gameObject);
            }
            catch (System.Exception ex)
            {

            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!hitObjects.Contains(col.gameObject) && active && col.gameObject.tag.Equals("Monster"))
        {
            try
            {
                //stun monster
                col.transform.root.GetComponentInChildren<Monster>().GetStunned();
                hitObjects.Add(col.gameObject);
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
