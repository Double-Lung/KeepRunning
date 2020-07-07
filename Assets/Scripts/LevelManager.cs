using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator animator;
    public static LevelManager instance;
    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        animator.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }


}
