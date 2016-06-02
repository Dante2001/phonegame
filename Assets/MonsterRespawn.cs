using UnityEngine;
using System.Collections;

public class MonsterRespawn : MonoBehaviour {

    public Vector3 spawnPosition;
    public bool isRespawned = false;

    public void Respawn()
    {
        this.GetComponent<AIFollow>().follow = false;
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(spawnPosition, out closestHit, 500, 1))
        {
            this.GetComponentInChildren<NavMeshAgent>().gameObject.transform.position = closestHit.position;
        }
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
