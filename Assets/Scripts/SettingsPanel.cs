using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public TMP_InputField[] Keys;
    public TMP_InputField Fire;
    // Start is called before the first frame update
    void Awake()
    {
        initialize();
    }
    public void Apply() {
        string fireString = Fire.text==""?"Space":Fire.text.ToUpper();
        KeyCode FireKeyCode;
        try {
            FireKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), fireString);
        } catch (ArgumentException){
            FireKeyCode = KeyCode.Space;
        }
        PlayerPrefs.SetInt("Fire",(int)FireKeyCode);
        for (int i = 0; i < Keys.Length; i++) {
            string keyString = Keys[i].text.ToUpper();
            keyString = keyString == "" ? "Z":keyString;
            KeyCode keyCode;
            try {
                keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyString);
            } catch (ArgumentException) {
                keyCode = KeyCode.Z;
            }
            PlayerPrefs.SetInt("Key"+(i+1).ToString(), (int)keyCode);
        }
        AudioManager.instance.Play("button");
    }

    public void ResetKeys() {
        Fire.text = "";
        for (int i = 0; i < Keys.Length; i++) {
            Keys[i].text = "";
        }
        PlayerPrefs.SetInt("Fire", (int)KeyCode.Space);
        for (int i = 0; i < Keys.Length; i++) {
            PlayerPrefs.SetInt("Key" + (i + 1).ToString(), (int)KeyCode.Z);
        }
        PlayerPrefs.SetInt("Key2", (int)KeyCode.X);
        AudioManager.instance.Play("button");
    }

    private void initialize() {
        Fire.text = ((KeyCode)PlayerPrefs.GetInt("Fire")).ToString();
        for (int i = 0; i < Keys.Length; i++) {
            Keys[i].text = ((KeyCode)PlayerPrefs.GetInt("Key"+(i+1).ToString())).ToString();
        }
    }
}
