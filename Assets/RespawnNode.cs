using UnityEngine;
using System.Collections;

public class RespawnNode : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if (col.name == "player")
        {
            col.gameObject.GetComponent<PlayerController>().SetRespawn(this.transform);
        }
    }
}
