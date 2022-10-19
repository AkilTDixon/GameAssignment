using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] float SpawnInterval = 5f;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] int SpawnNumber = 10;

    private Transform[] ChildObjects;
    private int numOfChildren;
    private float lastSpawned;
    // Start is called before the first frame update
    void Start()
    {
        ChildObjects = GetComponentsInChildren<Transform>();
        numOfChildren = ChildObjects.Length;
        lastSpawned = 0f;
    }

    // Update is called once per frame
    void Update()
    {   
        if ((Time.time > lastSpawned + SpawnInterval || lastSpawned == 0f) && SpawnNumber > 0)
        {
            Transform spawnPoint = ChildObjects[Random.Range(0, numOfChildren - 1)];

            GameObject obj = (GameObject)Instantiate(EnemyPrefab, spawnPoint.position, EnemyPrefab.transform.rotation);
            SpawnNumber--;
            lastSpawned = Time.time;
        }
    }
}
