using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss1 : MonoBehaviour
{
    [SerializeField] private GameObject BossObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "VintageRifle")
        {

            other.gameObject.GetComponent<MoveCharacter>().Crosshair.GetComponent<Shoot>().Boss1Phase1 = true;
            BossObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
