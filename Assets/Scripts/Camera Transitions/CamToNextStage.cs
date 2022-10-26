using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamToNextStage : MonoBehaviour
{

    [SerializeField] GameObject SpawnPoints;
    [SerializeField] GameObject WitchSpawnPoints;
    [SerializeField] GameObject BarrelSpawnPoints;
    [SerializeField] Transform NextStageEntry;
    [SerializeField] Vector3 CamPos;
    [SerializeField] bool BossNext = false;
    [SerializeField] AudioSource Stage1BGM;
    [SerializeField] AudioSource Stage2BGM;
    [SerializeField] AudioSource BossBGM;
    private List<GameObject> AllPlayersAndReticles;
    private Vector3 modifiedZEntry;
    
    void Start()
    {
        
        
        AllPlayersAndReticles = new List<GameObject>();
        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player1);
        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player1Reticle);

        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player2);
        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player2Reticle);

        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player3);
        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player3Reticle);

        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player4);
        AllPlayersAndReticles.Add(GameObject.Find("PlayerEntityInfo").GetComponent<PlayerEntityInfo>().Player4Reticle);

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player1" || other.gameObject.name == "Player2" || other.gameObject.name == "Player3" || other.gameObject.name == "Player4")
        {
            for (int i = 0; i < AllPlayersAndReticles.Count; i++)
            {

                AllPlayersAndReticles[i].transform.position = NextStageEntry.position;

            }
            GameObject HUD = Camera.main.transform.Find("HUD").gameObject;

            Camera.main.transform.position = CamPos;
            HUD.transform.Find("ArrowHolder").gameObject.SetActive(false);
            AudioSource currentBGM = HUD.GetComponent<HUDInfo>().ActiveBGM;
            if (currentBGM == Stage1BGM)
            {
                Stage1BGM.Stop();
                Stage1BGM.gameObject.SetActive(false);

                Stage2BGM.gameObject.SetActive(true);
                Stage2BGM.Play();

                HUD.GetComponent<HUDInfo>().ActiveBGM = Stage2BGM;
            }
            else if (currentBGM == Stage2BGM)
            {
                Stage2BGM.Stop();
                Stage2BGM.gameObject.SetActive(false);

                BossBGM.gameObject.SetActive(true);
                BossBGM.Play();

                HUD.GetComponent<HUDInfo>().ActiveBGM = BossBGM;
            }

            if (!BossNext)
            {
                SpawnPoints.GetComponent<Spawn>().enabled = true;
                WitchSpawnPoints.GetComponent<WitchSpawn>().enabled = true;
                if (BarrelSpawnPoints != null)
                    BarrelSpawnPoints.GetComponent<BarrelSpawn>().enabled = true;
            }


        }
    }
}
