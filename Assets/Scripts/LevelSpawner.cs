using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public int spawnDistance;
    public Transform player;
    public Transform lastLevelPart;
    Vector2 levelEndPos;
    // Start is called before the first frame update
    void Start()
    {
        levelEndPos = lastLevelPart.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelEndPos.x - player.position.x < spawnDistance * 8) {
            levelEndPos += Vector2.right * 8;
            LevelPooler.instance.SpawnLevel(levelEndPos, Quaternion.identity);
        }
    }
}
