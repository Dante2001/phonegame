using UnityEngine;
using System.Collections;

public class ChargePlateLogic : MonoBehaviour {

    private PlayerController playerController;
    private bool onPlate = false;

    void Start()
    {
        playerController = GameObject.Find("player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (onPlate)
            playerController.UseChargePlate(onPlate);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "pai")
            onPlate = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "pai")
        {
            onPlate = false;
            playerController.UseChargePlate(onPlate);
        }
    }
}
