using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource engineSource;
    public AudioSource gunSource;
    public AudioClip stopSound;
    public bool isMoving;
    public bool canFire;
    bool wasMoving;

    private void Awake() {
        isMoving = false;
        canFire = false;
        wasMoving = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving && !engineSource.isPlaying) {
            engineSource.Play();
        } else if (!isMoving && wasMoving) {
            engineSource.Stop();
            engineSource.PlayOneShot(stopSound);
        }
        if (canFire && !gunSource.isPlaying) {
            gunSource.Play();
        } else if(!canFire){
            gunSource.Stop();
        }
        wasMoving = isMoving;
    }
}
