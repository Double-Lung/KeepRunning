using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action onKeyPress;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.F) | Input.GetKeyDown(KeyCode.H)| Input.GetKeyDown(KeyCode.K) | Input.GetKeyDown(KeyCode.A)| Input.GetKeyDown(KeyCode.D)| Input.GetKeyDown(KeyCode.J)| Input.GetKeyDown(KeyCode.K)| Input.GetKeyDown(KeyCode.Z) | Input.GetKeyDown(KeyCode.X)) {
            onKeyPress();
        }
    }
}
