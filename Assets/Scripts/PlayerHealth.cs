using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    PlayerMovement playerMovement;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start() {
        health = maxHealth;
        UIManager.instance.initializeHealth(maxHealth);
    }

    public void TakeDamage() {
        health--;
        UIManager.instance.updateHealth(health);
        if (health < 1) {
            die();
        }
    }

    public void TakeFullDamage() {
        health = 0;
        UIManager.instance.updateHealth(health);
        die();
    }

    void die() {

        StartCoroutine(dieRoutine());
        // play music/cutscene 
    }
    IEnumerator dieRoutine() {
        playerMovement.enabled = false;
        animator.SetTrigger("die");
        AudioManager.instance.Play("wrud");
        AudioManager.instance.Stop("bgm");
        yield return new WaitForSeconds(2);
        UIManager.instance.animator.SetTrigger("start");
        LevelManager.instance.LoadNextLevel();
    }
}
