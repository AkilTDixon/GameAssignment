using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Transition : MonoBehaviour
{

    [SerializeField] GameObject GeometryToRemove;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GeometryToRemove.gameObject);
        Camera.main.transform.Find("HUD").Find("ArrowHolder").gameObject.SetActive(true);
        GetComponent<Spawn>().enabled = false;
        GameObject.Find("Stage1WitchSpawnPoints").GetComponent<WitchSpawn>().enabled = false;
    }

}
