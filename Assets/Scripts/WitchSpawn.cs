using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSpawn : MonoBehaviour
{

    /*
     Witch should have a random chance to spawn
     Witch can only spawn a maximum of two times per level
     */

    [SerializeField] int MaxSpawnOccurence = 2;
    [SerializeField] float SpawnChance = 0.01f;
    [SerializeField] GameObject EnemyPrefab;
    private Transform[] ChildObjects;
    private int numOfChildren;
    private float lastChecked;
    private float SpawnChanceBase;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(((int)System.DateTime.Now.Ticks));
        ChildObjects = GetComponentsInChildren<Transform>();
        numOfChildren = ChildObjects.Length;
        lastChecked = 0f;
        SpawnChanceBase = SpawnChance;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastChecked+1f)
        {
            float randNum = Random.value;
            if (randNum >= (1f - SpawnChance))
            {
               
                Transform spawnPoint = ChildObjects[Random.Range(0, numOfChildren - 1)];

                GameObject obj = (GameObject)Instantiate(EnemyPrefab, spawnPoint.position, EnemyPrefab.transform.rotation);

                //MaxSpawnOccurence -= 1;
                SpawnChance = SpawnChanceBase;
            }
            else
            {
                SpawnChance += 0.005f;
            }
            lastChecked = Time.time;
        }
    }
}
