using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject follow;
    public float leashStartFollow;
    public float catchUpSpeed;

    private bool isFollowing = false;

    private Vector2 camXZPosition;
    private Vector2 playerXZPosition;
    private Vector2 positionDifference;
    private Vector3 finalPosition;
    private Vector2 vel = Vector2.zero;

    private Camera cam;
    private Coroutine zoomCoroutine;

	// Use this for initialization
	void Start () {
        cam = this.GetComponent<Camera>();
        camXZPosition = new Vector2();
        playerXZPosition = new Vector2();
        positionDifference = new Vector2();
        finalPosition = new Vector3();
        finalPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.F))
        //    ChangeZoom(12, 1, 1);
        //if (Input.GetKeyDown(KeyCode.G))
        //    DefaultZoom(1);
        //if (Input.GetKeyDown(KeyCode.T))
        //    Jitter(0.5f, 0.12f);

        GetPositions();
        if (isFollowing)
            CheckForLeash();
        else if (GetPositionDifference() >= leashStartFollow)
            isFollowing = true;
        finalPosition.x = 0f;
        finalPosition.z = 0f;
        finalPosition.x += camXZPosition.x;
        finalPosition.z += camXZPosition.y;
        this.transform.position = finalPosition;
	}

    void GetPositions()
    {
        camXZPosition.x = this.transform.position.x;
        camXZPosition.y = this.transform.position.z;
        playerXZPosition.x = follow.transform.position.x;
        playerXZPosition.y = follow.transform.position.z;
    }

    float GetPositionDifference()
    {
        positionDifference = camXZPosition - playerXZPosition;
        return positionDifference.magnitude;
    }

    void CheckForLeash()
    {
        float posDiff = GetPositionDifference();
        if (posDiff == 0)
            isFollowing = false;
        else
            SmoothToPlayer();       
    }

    void SmoothToPlayer()
    {
        camXZPosition = Vector2.SmoothDamp(camXZPosition, playerXZPosition, ref vel, catchUpSpeed);
    }

    public void Jitter(float duration, float impact)
    {
        StartCoroutine(ShakeCamera(duration, impact));
    }

    IEnumerator ShakeCamera(float duration, float impact)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            //float x = Random.insideUnitCircle.x * impact;
            //float y = Random.insideUnitCircle.y * impact;
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= impact * damper;
            y *= impact * damper;
            this.transform.position = new Vector3(camXZPosition.x + x, finalPosition.y, camXZPosition.y + y);
            yield return new WaitForEndOfFrame();
        }
    }

    public void ChangeZoom(float size, float speed, float bounce = 0f)
    {
        if (zoomCoroutine != null)
         StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(ZoomTo(size, speed, bounce));
    }
    
    public void DefaultZoom(float speed)
    {
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(ZoomToDefault(7f, speed));
    }

    IEnumerator ZoomToDefault(float size, float speed)
    {
        float zoomVel = 0f;
        while (cam.orthographicSize >= size + 0.05f)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, size, ref zoomVel, speed);
            yield return new WaitForEndOfFrame();
        }
        cam.orthographicSize = size;
    }
    IEnumerator ZoomTo(float size, float speed, float bounce)
    {
        float zoomVel = 0f;
        while (cam.orthographicSize <= size + bounce - 0.12f)
        {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, size + bounce, ref zoomVel, speed);
            yield return new WaitForEndOfFrame();
        }
        if (bounce != 0)
        {
            while (cam.orthographicSize >= size + 0.08f)
            {
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, size, ref zoomVel, speed);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
