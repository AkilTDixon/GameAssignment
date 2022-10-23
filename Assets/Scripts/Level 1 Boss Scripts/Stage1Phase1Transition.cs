using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Phase1Transition : MonoBehaviour
{
    public List<GameObject> GeometryToRemove;
    [SerializeField] private GameObject BossSpawnPoints;
    [SerializeField] private GameObject PlayerEntity;
    public float HealthWindowPercent = 0.85f;
    private Enemy hpHolder;
    private float TransitionTime;
    private float MaxHP;

    /*
     
     HealthWindowPercent = (hpHolder.HealthPoints - ((hpHolder.HealthPoints - (hpHolder.HealthPoints * HealthWindowPercent)) * 2))
     /
     hpHolder.HealthPoints
     
    -68.269, 2.431, -44.088
     */
    void Start()
    {
        TransitionTime = 0;
        hpHolder = GetComponent<Enemy>();
        MaxHP = hpHolder.HealthPoints;
    }
    void Update()
    {
        if (hpHolder.HealthPoints <= MaxHP * HealthWindowPercent)
        {
            if (TransitionTime == 0)
                Transition();

            if (Time.time > TransitionTime + 0.25f)
            {
                PlayerEntity.GetComponent<MoveCharacter>().Crosshair.GetComponent<Shoot>().Boss1Phase1 = false;
                Destroy(gameObject);
            }

        }
    }
    void Transition()
    {
        TransitionTime = Time.time;
        for (int i = 0; i < GeometryToRemove.Count; i++)
            Destroy(GeometryToRemove[i].gameObject);


        BossSpawnPoints.GetComponent<Boss1Phase2>().enabled = true;

        Camera.main.transform.position = new Vector3(-68.269f, 2.431f, -44.088f);


        //Destroy(gameObject);
    }
}
