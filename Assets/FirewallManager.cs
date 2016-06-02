using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirewallManager : MonoBehaviour {

    public List<CubeTrigger> cubePlates;
    public Transform paiStart;
    public GameObject entrance;
    public GameObject exit;
    public GameObject pai;

    public List<GameObject> enemiesToSpawn;
    public List<GameObject> enemySpawnPosition;

    public void StartFirewall()
    {
        pai.GetComponent<MeshRenderer>().enabled = true;
        pai.transform.position = new Vector3(paiStart.position.x, pai.transform.position.y, paiStart.position.z);
        Camera.main.GetComponent<CameraFollow>().follow = pai;
    }

    public void FirewallLose()
    {
        //reset everything
        for (int i = 0; i < cubePlates.Count; i++)
        {
            cubePlates[i].RestartFirewall();
        }
    }

    public bool CheckForCompletion()
    {
        bool result = true;
        for (int i = 0; i < cubePlates.Count; i++)
        {
            result = result && cubePlates[i].isActive();
        }
        return result;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            GameObject newEnemy = (GameObject)Instantiate(enemiesToSpawn[i], enemySpawnPosition[i].transform.position, Quaternion.identity);
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(enemySpawnPosition[i].transform.position, out closestHit, 500, 1))
            {
                newEnemy.GetComponentInChildren<NavMeshAgent>().gameObject.transform.position = closestHit.position;
                newEnemy.GetComponentInChildren<NavMeshAgent>().enabled = true;
                newEnemy.GetComponentInChildren<AIFollow>().enabled = true;
                newEnemy.GetComponentInChildren<AIFollow>().victim = GameObject.Find("player");
                newEnemy.GetComponentInChildren<AIFollow>().follow = true;
                newEnemy.GetComponentInChildren<MonsterRespawn>().spawnPosition = closestHit.position;
            }
        }
    }

    public void FirewallCompleted()
    {
        Destroy(entrance);
        Destroy(exit);
        pai.GetComponent<MeshRenderer>().enabled = false;
        Camera.main.GetComponent<CameraFollow>().follow = GameObject.Find("player");
        SpawnEnemies();
        this.gameObject.name = "CompletedPuzzleTerminal";
        this.GetComponent<FirewallManager>().enabled = false;
    }
}
