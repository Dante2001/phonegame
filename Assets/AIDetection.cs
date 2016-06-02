using UnityEngine;
using System.Collections;

public class AIDetection : MonoBehaviour {

    public MonsterRespawn respawner;

	void OnTriggerEnter(Collider col)
    {
        if (col.name == "player" && respawner.isRespawned)
        {
            this.GetComponentInParent<AIFollow>().follow = true;
            this.gameObject.SetActive(false);
        }
    }
}
