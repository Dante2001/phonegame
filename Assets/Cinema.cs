using UnityEngine;
using System.Collections;

public class Cinema : MonoBehaviour {

    public AudioClip one;
    public AudioClip two;
    public AudioClip three;

	public void finishedShowing() {
		Application.LoadLevel ("mattScene");
	}

    public void playVO(int i)
    {
        if (i == 1)
        {
            this.GetComponent<AudioSource>().clip = one;
            this.GetComponent<AudioSource>().Play();
        }
        else if (i == 2)
        {
            this.GetComponent<AudioSource>().clip = two;
            this.GetComponent<AudioSource>().Play();
        }
        else
        {
            this.GetComponent<AudioSource>().clip = three;
            this.GetComponent<AudioSource>().Play();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
