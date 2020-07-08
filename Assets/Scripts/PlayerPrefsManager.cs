using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private void Awake() {
        if (PlayerPrefs.HasKey("NOTFIRSTTIME") && !Application.isEditor) {//
            return;
        }
        PlayerPrefs.SetInt("NOTFIRSTTIME", 1);
        initialization();
    }

    private void Start() {
        AudioManager.instance.Play("bgm");
    }

    void initialization() {
        PlayerPrefs.SetString("currentPlayer", "Orga");
        for (int i = 0; i < 10; i++) {
            PlayerPrefs.SetInt("Key" + (i + 1).ToString(), (int)KeyCode.Z);
        }
        PlayerPrefs.SetInt("Key2", (int)KeyCode.X);
        PlayerPrefs.SetInt("Fire", (int)KeyCode.Space);
        for (int i = 0; i < 10; i++) {
            PlayerPrefs.SetString("Player" + (i + 1).ToString(), "Empty");
            PlayerPrefs.SetString("Distance" + (i + 1).ToString(), "NULL");
        }
    }
}
