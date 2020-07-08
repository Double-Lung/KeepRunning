using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainbuttons;
    public GameObject start;
    public GameObject settings;
    public GameObject score;
    private CanvasGroup mainbuttonGroup;
    // Start is called before the first frame update
    private void Awake() {
        mainbuttonGroup = mainbuttons.GetComponent<CanvasGroup>();
    }

    public void StartGameFake() {
        start.SetActive(true);
        AudioManager.instance.Play("button");
        mainbuttonGroup.blocksRaycasts = false;
    }

    public void ScoreBoard() {
        score.SetActive(true);
        AudioManager.instance.Play("button");
        mainbuttonGroup.blocksRaycasts = false;
    }

    public void Options() {
        settings.SetActive(true);
        AudioManager.instance.Play("button");
        mainbuttonGroup.blocksRaycasts = false;
    }

    public void QuitGame() {
        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine() {
        AudioManager.instance.Play("button");
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void Back() {
        start.SetActive(false);
        settings.SetActive(false);
        score.SetActive(false);
        AudioManager.instance.Play("button");
        mainbuttonGroup.blocksRaycasts = true;
    }
}
