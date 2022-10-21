using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss1 : MonoBehaviour
{
    [SerializeField] private GameObject BossObject;
    private List<GameObject> AllPlayers;
    void Start()
    {
        AllPlayers = new List<GameObject>();
        AllPlayers.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player1);
        
        AllPlayers.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player2);
        
        AllPlayers.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player3);
        
        AllPlayers.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player4);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player1" || other.gameObject.name == "Player2" || other.gameObject.name == "Player3" || other.gameObject.name == "Player4")
        {
            for (int i = 0; i < AllPlayers.Count; i++)
                AllPlayers[i].gameObject.GetComponent<MoveCharacter>().Crosshair.GetComponent<Shoot>().Boss1Phase1 = true;
            
            BossObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
