using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    private float startX;
    private float distance;
    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        distance = 0;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > 0 && !started) {
            AudioManager.instance.Play("bgm");
            started = true;
        }
        distance = transform.position.x - startX;
        UIManager.instance.updateDistance(distance);
    }

    public void UpdateScore() {
        for (int i = 0; i < 10; i++) {
            string scoreString = PlayerPrefs.GetString("Distance" + (i + 1).ToString());
            if (scoreString == "NULL") {
                PlayerPrefs.SetString("Distance" + (i + 1).ToString(),distance.ToString("F2"));
                PlayerPrefs.SetString("Player" + (i + 1).ToString(),PlayerPrefs.GetString("currentPlayer"));
                return;
            }
            float score = float.Parse(scoreString);
            if (distance > score) {
                ShiftRanking(i);
                PlayerPrefs.SetString("Distance" + (i + 1).ToString(), distance.ToString("F2"));
                PlayerPrefs.SetString("Player" + (i + 1).ToString(), PlayerPrefs.GetString("currentPlayer"));
                return;
            }
        }
    }

    void ShiftRanking(int startIndex) {
        if (startIndex < 9) {
            for (int i = 9; i > startIndex; i--) {
                PlayerPrefs.SetString("Distance" + (i + 1).ToString(), PlayerPrefs.GetString("Distance"+i));
                PlayerPrefs.SetString("Player" + (i + 1).ToString(), PlayerPrefs.GetString("Player"+i));
            }
        }
    }
}
