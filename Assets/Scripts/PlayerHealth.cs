using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    private PlayerMovement playerMovement;
    private Animator animator;
    private PlayerDistance playerDistance;
    public event Action OnDie;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerDistance = GetComponent<PlayerDistance>();
    }

    private void Start() {
        health = maxHealth;
        UIManager.instance.initializeHealth(maxHealth);
    }

    public void TakeDamage(int amount) {
        health-= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        UIManager.instance.updateHealth(health);
        if (health < 1) {
            die();
        }
    }
    void die() {
        playerDistance.UpdateScore();
        OnDie?.Invoke();
        StartCoroutine(dieRoutine());
        // play music/cutscene 
    }
    IEnumerator dieRoutine() {
        playerMovement.enabled = false;
        animator.SetTrigger("die");
        AudioManager.instance.Play("wrud");
        AudioManager.instance.FadeOut("bgm",3);
        AudioManager.instance.FadeOut("bgm2", 3);
        yield return new WaitForSeconds(2);
        UIManager.instance.animator.SetTrigger("start");
        LevelManager.instance.LoadNextLevel();
    }
}
