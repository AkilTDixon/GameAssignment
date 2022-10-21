using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Phase1Transition : MonoBehaviour
{
    public List<GameObject> GeometryToRemove;
    [SerializeField] private GameObject BossSpawnPoints;
    [SerializeField] private GameObject PlayerEntity;
    private Enemy hpHolder;
    private float TransitionTime;
    

    void Start()
    {
        TransitionTime = 0;
        hpHolder = GetComponent<Enemy>();
    }
    void Update()
    {
        if (hpHolder.HealthPoints <= 4250)
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

        Camera.main.transform.position = new Vector3(-67.32f, 3.18f, -48.15f);


        //Destroy(gameObject);
    }
}
