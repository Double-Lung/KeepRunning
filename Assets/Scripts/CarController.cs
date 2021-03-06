﻿using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform player;
    private PlayerInput playerInput;
    public PlayerMovement playerMovement;
    [Range(0,1)]
    public float speedBonus = 0.1f;
    public float speedBonus2 = 0.05f;
    public float speedDropChance;
    public float speedIncreaseChance;
    public float nextDropTime;
    public float speedChangeFreq = 1;
    [SerializeField]private float speed;
    private int states; //0:far, 1: medium, 2: close, 3: stop
    private float targetSpeed;
    private float maxSpeed;
    public float smashCountdown = 3;
    [SerializeField]  private float timeLeft;
    private float refSpeed;
    public float smoothTime;
    private int prevState;
    private Animator animator;
    public event Action closeRangeAction;
    private CarAudio carAudio;
    private PlayerFire playerFire;
    private float startTime;
    private bool getHit;
    private float debuffDuration = 2;
    private float debuffEndTime;

    void Awake()
    {
        animator = GetComponent<Animator>();
        carAudio = GetComponent<CarAudio>();
        playerInput = player.GetComponent<PlayerInput>();
        playerFire = player.GetComponent<PlayerFire>();
        states = 3;
        targetSpeed = 0;
        maxSpeed = 0;
        nextDropTime = 0;
        timeLeft = smashCountdown;
        startTime = Time.time;
        playerInput.onPause += OnPause;
        playerFire.onFire += ReduceSpeed;
        getHit = false;
    }

    void Update()
    {
        UpdateStates();
        animator.SetInteger("states", states);
        TargetSpeedModifier();
        AudioToggler();
        speed = Mathf.SmoothDamp(speed, targetSpeed, ref refSpeed, smoothTime);
        UIManager.instance.updateCarSpeed(speed);
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    void AudioToggler() {
        switch (states) {
            case 0:
                carAudio.isMoving = true;
                carAudio.canFire = false;
                if (prevState == 2) {
                    AudioManager.instance.CrossFade("bgm", "bgm2", 1, false);
                }
                break;
            case 1:
                carAudio.isMoving = true;
                carAudio.canFire = false;
                if (prevState == 2) {
                    AudioManager.instance.CrossFade("bgm", "bgm2", 1, false);
                }
                break;
            case 2:
                carAudio.isMoving = true;
                carAudio.canFire = true;
                if (prevState != 2) {
                    AudioManager.instance.CrossFade("bgm2","bgm",1,false);
                }
                break;
            case 3:
                carAudio.canFire = false;
                carAudio.isMoving = false;
                break;
            default:
                break;
        }
    }

    void ReduceSpeed() {
        getHit = true;
        debuffEndTime = Time.time + debuffDuration;
        targetSpeed = 5;
        speed *= 0.8f;
    }

    void TargetSpeedModifier() {
        if (getHit && Time.time < debuffEndTime) {
            return;
        }
        getHit = false;
        switch (states) {
            case 0:
                maxSpeed = Mathf.Max(maxSpeed, playerMovement.speed);
                targetSpeed = maxSpeed * (1 + speedBonus2);
                break;
            case 1:
                if (prevState == 3) {
                    targetSpeed = 10;
                } else if (prevState==0) {
                    targetSpeed = Mathf.Clamp(targetSpeed,0,speed*0.7f);
                }else {
                    MaintainSpeed();
                }
                break;
            case 2:
                if (prevState == 3) {
                    targetSpeed = 10;
                    break;
                }
                closeRangeAction?.Invoke();
                if (prevState != 2) {
                    timeLeft = smashCountdown;
                }
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0) {
                    targetSpeed *= 1.5f;
                    timeLeft = smashCountdown;
                }
                break;
            case 3:
                targetSpeed = 0;
                break;
            default:
                break;
        }
    }

    void UpdateStates() {
        prevState = states;
        float distance = player.position.x - transform.position.x;
        playerFire.accuracy = 1 - distance / 45;
        if (states == 3 && Time.time< startTime+2.5f) {
            return;
        }
        if (distance < 2) {
            states = 3;
            return;
        }
        if (distance >= 45) {
            states = 0;
            return;
        }
        if (distance < 15) {
            states = 2;
            return;
        }
        states = 1;
    }

    void MaintainSpeed() {
        if (Time.time > nextDropTime) {
            nextDropTime = Time.time + speedChangeFreq;
            if (speedDropChance > UnityEngine.Random.value) {
                targetSpeed *= (1 - speedBonus);
                return;
            }
            if (speedIncreaseChance > UnityEngine.Random.value) {
                targetSpeed *= (1 + speedBonus);
                return;
            }
        }
        targetSpeed = Mathf.Clamp(targetSpeed, 10, speed*1.2f);
    }

    void OnPause() {
        states = 3;
        speed = 0;
        startTime = Time.time + 31536000;
        playerInput.onPause -= OnPause;
    }
}
