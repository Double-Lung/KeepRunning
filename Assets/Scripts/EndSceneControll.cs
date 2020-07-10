using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneControll : MonoBehaviour
{
    public float playbackDelay = 1;
    public VideoController videoController;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }
    private void Update() {
        if (!videoController.started && Time.time > startTime + playbackDelay) {
            videoController.StartPlayback();
            return;
        }
        if (videoController.started && Input.GetMouseButtonDown(0)) {
            videoController.SkipVideo();
        }
    }
}
