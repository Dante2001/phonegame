using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawnDespawner : MonoBehaviour {

    public GameObject cubeToSpawn;
    private List<GameObject> spawnedCubes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool SpawnCube()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 10.0f;
        if (Physics.Raycast(ray, out hit, hitdist))
        {
            if (hit.collider.name.StartsWith("Cube Plate"))
            {
                hit.collider.GetComponent<CubeTrigger>().SpawnCube();
                return true;
            }
            else if (!hit.collider.name.StartsWith("SpawnedCube"))
            {
                spawnedCubes.Add((GameObject)Instantiate(cubeToSpawn, hit.point + Vector3.up, Quaternion.identity));
                return true;
            }
        }
        return false;
    }

    public bool DespawnCube()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 10.0f;
        if (Physics.Raycast(ray, out hit, hitdist))
        {
            if (hit.collider.name.StartsWith("SpawnedCube"))
            {
                hit.collider.GetComponent<CubeTrigger>().DespawnCube();
                return true;
            }
        }
        return false;
    }

}
