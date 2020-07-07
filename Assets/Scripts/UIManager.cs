using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public HealthBar healthbar;
    public SpeedDisplay speedDisplay;
    public DistanceDisplay distanceDisplay;
    public Animator animator;

    public static UIManager instance;
    private void Awake() {
        if (instance!=null&& this != instance) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        animator = GetComponent<Animator>();
    }

    public void updateHealth(int health) {
        healthbar.SetHealth(health);
    }

    public void initializeHealth(int health) {
        healthbar.SetMaxHealth(health);
    }

    public void updateCarSpeed(float speed) {
        speedDisplay.carSpeed.SetText(speed.ToString("F1"));
    }

    public void updatePlayerSpeed(float speed) {
        speedDisplay.playerSpeed.SetText(speed.ToString("F1"));
    }

    public void updateDistance(float distance) {
        distanceDisplay.distance.SetText(distance.ToString("F1"));
    }
}
