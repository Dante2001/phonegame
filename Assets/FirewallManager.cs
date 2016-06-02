using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirewallManager : MonoBehaviour {

    public List<CubeTrigger> cubePlates;
    public Transform paiStart;
    public GameObject entrance;
    public GameObject exit;
    public GameObject pai;

    public void StartFirewall()
    {
        pai.GetComponent<Renderer>().enabled = true;
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

    public void FirewallCompleted()
    {
        Destroy(entrance);
        Destroy(exit);
        Camera.main.GetComponent<CameraFollow>().follow = GameObject.Find("player");
    }
}
