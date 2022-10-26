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
    public float SpawnFrequency = 1f;
    public bool SpecifyPosition = false;
    public bool SpecifyX = false;
    public bool SpecifyY = false;
    public bool SpecifyZ = false;
    public float xPos = 0f;
    public float yPos = 0f;
    public float zPos = 0f;

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
        if (Time.time > lastChecked + SpawnFrequency)
        {
            float randNum = Random.value;
            if (randNum >= (1f - SpawnChance))
            {

                Transform spawnPoint = ChildObjects[Random.Range(1, numOfChildren - 1)];
                Vector3 sPoint = spawnPoint.position;
                if (SpecifyPosition)
                {
                    if (SpecifyX)
                        sPoint.x = xPos;
                    if (SpecifyY)
                        sPoint.y = yPos;
                    if (SpecifyZ)
                        sPoint.z = zPos;

                }
                GameObject obj = (GameObject)Instantiate(BarrelPrefab, sPoint, BarrelPrefab.transform.rotation);
                
            
            }

            lastChecked = Time.time;
        }
    }
}
