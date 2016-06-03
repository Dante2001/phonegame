using UnityEngine;
using System.Collections;

public class CubeDetails : MonoBehaviour {

    public GameObject parent = null;
    private bool despawned = false;

    public void despawnMe()
    {
        if (!despawned)
        {
            this.GetComponent<Animator>().SetTrigger("toDespawn");
            this.GetComponent<BoxCollider>().enabled = false;
            despawned = true;
        }
    }

    public void destroyMe()
    {
        Destroy(this.gameObject);
    }
}
