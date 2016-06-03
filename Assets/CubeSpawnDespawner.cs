using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawnDespawner : MonoBehaviour {

    public GameObject cubeToSpawn;
    private List<GameObject> spawnedCubes = new List<GameObject>();

    public bool SpawnCube(float maxSpawnDistance)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 10.0f;
        if (Physics.Raycast(ray, out hit, hitdist))
        {
            if ((hit.point - this.transform.position).magnitude <= maxSpawnDistance)
            {
                if (hit.collider.name.StartsWith("CubePlate"))
                {
                    hit.collider.GetComponent<CubeTrigger>().SpawnCube();
                    return true;
                }
                else if (!hit.collider.name.StartsWith("SpawnedCube"))
                {
                    GameObject spawnedCube = (GameObject)Instantiate(cubeToSpawn, new Vector3(hit.point.x, this.transform.position.y, hit.point.z), Quaternion.Euler(90,0,0));
                    spawnedCubes.Add(spawnedCube);
                    return true;
                }
            }
        }
        return false;
    }

    public bool DespawnCube(float maxDespawnDistance)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 10.0f;
        if (Physics.Raycast(ray, out hit, hitdist))
        {
            if ((hit.point - this.transform.position).magnitude <= maxDespawnDistance)
            {
                if (hit.collider.name.StartsWith("SpawnedCube"))
                {
                    GameObject parentOfCube = hit.collider.GetComponent<CubeDetails>().parent;
                    if (parentOfCube != null)
                       parentOfCube.GetComponent<CubeTrigger>().DespawnCube();
                    else
                        Destroy(hit.collider.gameObject);
                    return true;
                }
            }
        }
        return false;
    }

    public void RestartPuzzle()
    {
        for (int i = 0; i < spawnedCubes.Count; i++ )
        {
            Destroy(spawnedCubes[i]);
        }
        spawnedCubes.Clear();
    }
}
