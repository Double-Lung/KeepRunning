using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanel : MonoBehaviour
{
    public TMP_InputField nickname;
    public Animator animator;
    // Start is called before the first frame update
    public void StartGame() {
        PlayerPrefs.SetString("currentPlayer", nickname.text==""?"Orga": nickname.text);
        StartCoroutine(StartGameRoutine());
    }


    IEnumerator StartGameRoutine() {
        AudioManager.instance.Play("button");
        animator.SetTrigger("start");
        AudioManager.instance.FadeOut("bgm",1);
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.Play("kuruma");
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
