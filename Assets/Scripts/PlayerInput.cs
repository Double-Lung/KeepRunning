using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action onKeyPress;
    public event Action onPause;
    public event Action onFire;
    private KeyCode[] keycodes;
    private KeyCode fire;

    private void Awake() {
        keycodes = new KeyCode[10];
        for (int i = 0; i < 10; i++) {
            keycodes[i] = (KeyCode)PlayerPrefs.GetInt("Key"+(i+1).ToString());
        }
        fire = (KeyCode)PlayerPrefs.GetInt("Fire");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            onPause?.Invoke();
            UIManager.instance.ShowPause();
        }
        if (Input.GetKeyDown(fire)) {
            onFire?.Invoke();
        }
        foreach (KeyCode k in keycodes) {
            if (Input.GetKeyDown(k)) {
                onKeyPress?.Invoke();
                return;
            }
        }
    }
}
