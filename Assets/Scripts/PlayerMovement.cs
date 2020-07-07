using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed { get; private set; }
    public float smoothTime;
    public float stepSize;
    PlayerInput playerInput;
    Animator animator;
    float refSpeed;
    bool startAudio;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.onKeyPress += walk;
        speed = 0;
        startAudio = false;
    }

    void walk() {
        speed += stepSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>1 && !startAudio) {
            AudioManager.instance.Play("ready");
            startAudio = true;
        }
        animator.SetFloat("runningSpeed",speed*0.4f);
        animator.SetFloat("speed", speed);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        UIManager.instance.updatePlayerSpeed(speed);
        speed = Mathf.SmoothDamp(speed, 0, ref refSpeed, smoothTime);
    }
}
