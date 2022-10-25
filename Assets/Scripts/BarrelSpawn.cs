using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{

    public float SpawnChance = 0.2f;
    [SerializeField] GameObject BarrelPrefab;
    public Transform[] ChildObjects;
    private int numOfChildren;
    private float lastChecked;
    public int EntitiesPerInterval = 1;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(((int)System.DateTime.Now.Ticks));
        ChildObjects = GetComponentsInChildren<Transform>();
        numOfChildren = ChildObjects.Length;
        lastChecked = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastChecked + 1f)
        {
            float randNum = Random.value;
            if (randNum >= (1f - SpawnChance))
            {

                Transform spawnPoint = ChildObjects[Random.Range(1, numOfChildren - 1)];

                GameObject obj = (GameObject)Instantiate(BarrelPrefab, spawnPoint.position, BarrelPrefab.transform.rotation);

            }

            lastChecked = Time.time;
        }
    }
}
