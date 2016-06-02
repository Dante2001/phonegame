using UnityEngine;
using System.Collections;

public class CubeTrigger : MonoBehaviour {

    public Vector3 cubePosOffset;
    public GameObject cubeToSpawn;
    private GameObject spawnedCube;
    private bool hasCube = false;
    public bool startsWithCube = false;

	// Use this for initialization
	void Start () {
        if (startsWithCube)
            SpawnCube();
	}
	
    public void SpawnCube()
    {
        hasCube = true;
        spawnedCube = (GameObject)Instantiate(cubeToSpawn, cubePosOffset + this.transform.position, Quaternion.identity);
        spawnedCube.GetComponent<CubeDetails>().parent = this.gameObject;
    }

    public void DespawnCube()
    {
        hasCube = false;
        Destroy(spawnedCube);
    }

    public void RestartFirewall()
    {
        if (hasCube)
            DespawnCube();
        if (startsWithCube)
            SpawnCube();
    }

    public bool isActive()
    {
        return hasCube;
    }
}
