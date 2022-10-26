using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTransition : MonoBehaviour
{
    [SerializeField] GameObject BarrelSpawnPoints;
    [SerializeField] GameObject WitchSpawnPoints;
    [SerializeField] GameObject GeometryToRemove;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GeometryToRemove.gameObject);
        Camera.main.transform.Find("HUD").Find("ArrowHolder").gameObject.SetActive(true);
        GetComponent<Spawn>().enabled = false;
        WitchSpawnPoints.gameObject.GetComponent<WitchSpawn>().enabled = false;
        if (BarrelSpawnPoints != null)
            BarrelSpawnPoints.gameObject.GetComponent<BarrelSpawn>().enabled = false;
    }

}
