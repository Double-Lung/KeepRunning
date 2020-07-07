using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] footSteps;
    AudioSource audioSource;
    int soundCount;
    // Start is called before the first frame update
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        soundCount = footSteps.Length;
    }

    public void playFootStep() {
        int index = Random.Range(0, soundCount);
        audioSource.clip = footSteps[index];
        audioSource.Play();
    }
}
