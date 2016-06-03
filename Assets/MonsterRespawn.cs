using UnityEngine;
using System.Collections;

public class MonsterRespawn : MonoBehaviour {

    public Vector3 spawnPosition;
    public bool isRespawned = true;
    public GameObject detector;
    public Monster monster;

    public void Respawn()
    {
        monster.Respawn();
        this.GetComponent<AIFollow>().follow = false;
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(spawnPosition, out closestHit, 500, 1))
        {
            this.GetComponentInChildren<NavMeshAgent>().gameObject.transform.position = closestHit.position;
        }
        detector.SetActive(true);

    }
}
