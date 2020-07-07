using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    float startX;
    float distance;
    bool started;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        distance = 0;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > 0 && !started) {
            AudioManager.instance.Play("bgm");
            started = true;
        }
        distance = transform.position.x - startX;
        UIManager.instance.updateDistance(distance);
    }
}
