using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanel : MonoBehaviour {

    public ScoreRow[] rows;
    // Start is called before the first frame update
    void Awake() {
        initialization();
    }

    void initialization() {
        foreach (ScoreRow s in rows) {
            s.nickname.text = PlayerPrefs.GetString("Player"+s.rank.ToString());
            s.distance.text = PlayerPrefs.GetString("Distance" + s.rank.ToString());
        }
    }

    [System.Serializable]
    public class ScoreRow{
        public int rank;
        public TextMeshProUGUI nickname;
        public TextMeshProUGUI distance;
    }
}
