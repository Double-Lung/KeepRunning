using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShoot : MonoBehaviour {
    CarController carController;
    public float fireRate;
    public float nextFireTime;
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    private void Awake() {
        carController = GetComponent<CarController>();
        carController.closeRangeAction += shoot;
        nextFireTime = 0;
    }

    void shoot() {
        if (Time.time > nextFireTime) {
            nextFireTime = Time.time + fireRate;
            playerHealth.TakeDamage();
            if (playerHealth.health < 1) {
                carController.closeRangeAction -= shoot;
            }
        }
    }
}
