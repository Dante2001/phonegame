using UnityEngine;
using System.Collections;

public class AIDetection : MonoBehaviour {

    public AIFollow aifollow;

	void OnTriggerEnter(Collider col)
    {
        if (col.name == "player")
        {
            aifollow.follow = true;
            this.gameObject.SetActive(false);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.name == "player")
        {
            aifollow.follow = true;
            this.gameObject.SetActive(false);
        }
    }
}
