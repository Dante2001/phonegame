using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersonalSoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource otherSource;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    public void PlayEfxRandom(List<AudioClip> clips)
    {
        int randomIndex = Random.Range(0, clips.Count);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void PlayOtherRandom(List<AudioClip> clips)
    {
        int randomIndex = Random.Range(0, clips.Count);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        otherSource.pitch = randomPitch;
        otherSource.clip = clips[randomIndex];
        otherSource.Play();
    }

    public void StopSFX()
    {
        efxSource.Stop();
    }

    public void StopOther()
    {
        efxSource.Stop();
    }

}
