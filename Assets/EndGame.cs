using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGame : MonoBehaviour {

    public GameObject lastFirewall;
    public GameObject endScreen;
    public GameObject effect;
    private bool hasEnded = false;
    private bool hasFX = false;

    void Update()
    {
        if (lastFirewall == null && !hasFX)
        {
            hasFX = true;
            effect.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "player" && lastFirewall == null && !hasEnded)
        {
            hasEnded = true;
            StartCoroutine(BackToMainMenu());
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.name == "player" && lastFirewall == null && !hasEnded)
        {
            hasEnded = true;
            StartCoroutine(BackToMainMenu());
        }
    }

    IEnumerator BackToMainMenu()
    {
        Camera.main.GetComponent<CameraFollow>().follow = this.gameObject;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().playMenuTheme();
        endScreen.SetActive(true);
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(monster.transform.root.gameObject);
        }
        Destroy(GameObject.Find("player"));
        Destroy(GameObject.Find("pai"));
        float timer = 6f;
        while(timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(0);
    }

}
