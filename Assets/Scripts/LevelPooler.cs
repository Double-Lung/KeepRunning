using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPooler : MonoBehaviour {
    public static LevelPooler instance;
    public GameObject prefab;
    public int size;
    Queue<GameObject> levelPool;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        levelPool = new Queue<GameObject>();

        for (int i = 0; i < size; i++) {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            levelPool.Enqueue(obj);
        }
    }

    public GameObject SpawnLevel(Vector3 position, Quaternion rotation) {
        GameObject obj = levelPool.Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        levelPool.Enqueue(obj);
        return obj;
    }
}
