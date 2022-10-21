using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float SpawnInterval = 5f;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject PlayerEntityInfo;
    public bool VariantMode = false;
    public int SpawnNumber = 10;
    public int EntitiesPerInterval = 1;

    private Transform[] ChildObjects;
    private int numOfChildren;
    private float lastSpawned;
    public float VariantStart = 0;
    public float vActiveTime = 10f;

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
/*        if (VariantMode)
        {
            if (VariantStart == 0)
                VariantStart = Time.time;

            if (Time.time > VariantStart + vActiveTime)
            {
                VariantStart = 0;        
            }
                
        }*/

        if ((Time.time > lastSpawned + SpawnInterval || lastSpawned == 0f) && SpawnNumber > 0)
        {
            for (int i = 0; i < EntitiesPerInterval; i++)
            {
                Transform spawnPoint = ChildObjects[Random.Range(0, numOfChildren - 1)];

                Instantiate(EnemyPrefab, spawnPoint.position, EnemyPrefab.transform.rotation);
                SpawnNumber--;
                lastSpawned = Time.time;
            }
        }
        if (SpawnNumber <= 0)
        {
/*            if (VariantMode)
            {
                VariantStart = 0;        
            }*/
            GetComponent<StageTransition>().enabled = true;
        }
    }
}
