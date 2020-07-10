using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public HealthBar healthbar;
    public ManaBar manabar;
    public SpeedDisplay speedDisplay;
    public DistanceDisplay distanceDisplay;
    public Animator animator;
    public GameObject PausePanel;
    private bool paused;

    public static UIManager instance;
    private void Awake() {
        if (instance!=null&& this != instance) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        animator = GetComponent<Animator>();
    }

    private void Start() {
        LevelManager.instance.animator.SetTrigger("playback");
    }

    public void updateHealth(int health) {
        healthbar.SetHealth(health);
    }

    public void initializeHealth(int health) {
        healthbar.SetMaxHealth(health);
    }

    public void initializeMana(float mana) {
        manabar.SetMaxMana(mana);
    }

    public void updateMana(float mana) {
        manabar.SetMana(mana);
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

    public void ShowPause() {
        if (!paused) {
            PausePanel.SetActive(true);
            paused = true;
        }
    }
}
