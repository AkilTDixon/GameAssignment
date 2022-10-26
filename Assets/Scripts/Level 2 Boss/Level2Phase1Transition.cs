using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Phase1Transition : MonoBehaviour
{
    public GameObject BossBarrelSpawnPoints;
    public GameObject BossSkeletonSpawn;
    public GameObject Boss2Phase2;
    public float HealthWindowPercent = 0.25f;
    private Enemy hpHolder;
    private float TransitionTime;
    private float MaxHP;
    // Start is called before the first frame update

    void Start()
    {
        //TransitionTime = 0;
        hpHolder = GetComponent<Enemy>();
        MaxHP = hpHolder.HealthPoints;
    }
    // Update is called once per frame
    void Update()
    {
        if (hpHolder.HealthPoints <= MaxHP * HealthWindowPercent)
        {

            //if (Time.time > TransitionTime + 0.25f)
            //{
                BossBarrelSpawnPoints.GetComponent<BarrelSpawn>().enabled = false;
                BossSkeletonSpawn.GetComponent<Spawn>().enabled = false;
                Boss2Phase2.SetActive(true);
                Destroy(gameObject);
            //}
        }
    }

}
