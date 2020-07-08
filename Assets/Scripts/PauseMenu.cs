using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Title() {
        StartCoroutine(TitleRoutine());
    }

    public void Restart() {
        StartCoroutine(RestartRoutine());
    }

    IEnumerator TitleRoutine() {
        LoadLevelPrep();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    IEnumerator RestartRoutine() {
        LoadLevelPrep();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadLevelPrep() {
        AudioManager.instance.Play("button");
        AudioManager.instance.FadeOut("bgm", 1);
        UIManager.instance.animator.SetTrigger("start");
        LevelManager.instance.animator.SetTrigger("start");
    }
}
