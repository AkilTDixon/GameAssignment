using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSpawn : MonoBehaviour
{

    /*
     Witch should have a random chance to spawn
     Witch can only spawn a maximum of two times per level
     */

    public float SpawnChance = 0.01f;
    public float SpawnChanceIncrement = 0.005f;
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject PlayerEntityInfo;
    public bool VariantMode = false;
    private Transform[] ChildObjects;
    private int numOfChildren;
    private float lastChecked;
    private float SpawnChanceBase;
    public float VariantStart = 0;
    public float vActiveTime = 10;
    public bool OverrideMode = false;
    public int HealthPoints = 0;
    public float ActiveTime = 0;
    public float EntrySpeed = 0;


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
               
                Transform spawnPoint = ChildObjects[Random.Range(1, numOfChildren - 1)];

                GameObject obj = (GameObject)Instantiate(EnemyPrefab, spawnPoint.position, EnemyPrefab.transform.rotation);
                if (OverrideMode)
                {
                    Enemy en = obj.GetComponent<Enemy>();
                    en.HealthPoints = HealthPoints;
                    en.ActiveTime = ActiveTime;
                    en.EntrySpeed = EntrySpeed;
                }
                SpawnChance = SpawnChanceBase;
            }
            else
            {
                SpawnChance += SpawnChanceIncrement;
            }
            lastChecked = Time.time;
        }
    }
}
