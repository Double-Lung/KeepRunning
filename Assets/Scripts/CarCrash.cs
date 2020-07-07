using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrash : MonoBehaviour
{
    CarController carController;
    public PlayerHealth playerHealth;
    private void Awake() {
        carController = GetComponent<CarController>();
        carController.closeRangeAction += Smash;
    }

    void Smash() {
        if (playerHealth.transform.position.x - transform.position.x < 3) {
            playerHealth.TakeFullDamage();
            carController.closeRangeAction -= Smash;
        }
    }
}
