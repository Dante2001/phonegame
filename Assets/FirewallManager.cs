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
        GameObject.Find("SoundManager").GetComponent<SoundManager>().playMainTheme();
        pai.GetComponentInChildren<SpriteRenderer>().enabled = true;
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(new Vector3(paiStart.position.x, pai.transform.position.y, paiStart.position.z), out closestHit, 500, 1))
            pai.transform.position = closestHit.position;
        Camera.main.GetComponent<CameraFollow>().follow = pai;
    }

    public void FirewallLose()
    {
        //reset everything
        for (int i = 0; i < cubePlates.Count; i++)
        {
            cubePlates[i].RestartFirewall();
        }
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(new Vector3(paiStart.position.x, pai.transform.position.y, paiStart.position.z), out closestHit, 500, 1))
            pai.transform.position = closestHit.position;
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
            
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(enemySpawnPosition[i].transform.position, out closestHit, 500, 1))
            {
                GameObject newEnemy = (GameObject)Instantiate(enemiesToSpawn[i], closestHit.position, Quaternion.identity);
                //newEnemy.GetComponentInChildren<NavMeshAgent>().gameObject.transform.position = closestHit.position;
                newEnemy.GetComponentInChildren<NavMeshAgent>().enabled = true;
                newEnemy.GetComponentInChildren<AIFollow>().enabled = true;
                newEnemy.GetComponentInChildren<AIFollow>().victim = GameObject.Find("player");
                newEnemy.GetComponentInChildren<AIFollow>().follow = false;
                newEnemy.GetComponentInChildren<MonsterRespawn>().spawnPosition = closestHit.position;
            }
        }
    }

    public void FirewallCompleted()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().playCombatTheme();
        Destroy(entrance);
        Destroy(exit);
        pai.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Camera.main.GetComponent<CameraFollow>().follow = GameObject.Find("player");
        SpawnEnemies();
        this.gameObject.name = "CompletedPuzzleTerminal";
        this.GetComponent<FirewallManager>().enabled = false;
    }
}
