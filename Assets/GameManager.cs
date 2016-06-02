using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static bool hasPhone = true;
    public static int playerFacing = 0; //0 up, 1 right, 2 down, 3 left
    public static Vector3 directionFromPlayerToMouse;
    public static bool isAI = false; // if you are controlling the AI
    public static int aiFacing = 0;
}
