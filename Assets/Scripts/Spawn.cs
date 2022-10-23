using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float SpawnInterval = 5f;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject PlayerEntityInfo;
    public bool OverrideMode = false;
    public int HealthPoints = 0;
    public float ActiveTime = 0;
    public float EntrySpeed = 0;

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


        if ((Time.time > lastSpawned + SpawnInterval || lastSpawned == 0f) && SpawnNumber > 0)
        {
            for (int i = 0; i < EntitiesPerInterval; i++)
            {
                Transform spawnPoint = ChildObjects[Random.Range(0, numOfChildren - 1)];

                GameObject obj = Instantiate(EnemyPrefab, spawnPoint.position, EnemyPrefab.transform.rotation);
                if (OverrideMode)
                {
                    Enemy en = obj.GetComponent<Enemy>();
                    en.HealthPoints = HealthPoints;
                    en.ActiveTime = ActiveTime;
                    en.EntrySpeed = EntrySpeed;
                }
                
                SpawnNumber--;
                lastSpawned = Time.time;
            }
        }
        if (SpawnNumber <= 0)
        {
            GetComponent<StageTransition>().enabled = true;
        }
    }
}
