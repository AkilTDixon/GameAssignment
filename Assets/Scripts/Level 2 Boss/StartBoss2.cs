using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss2 : MonoBehaviour
{
    public GameObject BossObject;
    public GameObject BossSkeletonSpawn;
    public GameObject BossBarrelSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player1" || other.gameObject.name == "Player2" || other.gameObject.name == "Player3" || other.gameObject.name == "Player4")
        {

            BossObject.SetActive(true);
            BossSkeletonSpawn.GetComponent<Spawn>().enabled = true;
            BossBarrelSpawn.GetComponent<BarrelSpawn>().enabled = true;


            Destroy(gameObject);
        }
    }

}
