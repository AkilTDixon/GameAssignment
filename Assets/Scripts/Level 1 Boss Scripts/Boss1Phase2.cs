using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Phase2 : MonoBehaviour
{
    [SerializeField] GameObject ImagePrefab;
    [SerializeField] GameObject BossPrefab;
    [SerializeField] List<GameObject> PlayerReticles;

    public Transform[] ChildObjects;
    private List<GameObject> BossImages;
    private int numOfChildren;
    private bool BossInstantiated = false;
    private int BossHealth = 30;
    private int DeadImages = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        Random.InitState(((int)System.DateTime.Now.Ticks));
        BossImages = new List<GameObject>();
        ChildObjects = GetComponentsInChildren<Transform>();
        numOfChildren = ChildObjects.Length;
        //lastSpawned = 0f;
        SpawnNPCs();
    }

    // Update is called once per frame
    void Update()
    {
        //if (BossImages.Count == 0)

        CurateList(BossImages);
    }
    public void RestartPhase()
    {
        BossHealth -= 10;
        ClearList(BossImages);
        SpawnNPCs();
    }
    public void EndEncounter()
    {
        BossHealth -= 10;
        ClearList(BossImages);
        Destroy(gameObject);
    }
    void SpawnNPCs()
    {
        int randNum = Random.Range(1, ChildObjects.Length-1);
        
        for (int i = BossImages.Count+1; i < ChildObjects.Length; i++)
        {

            Transform spawnPoint = ChildObjects[i];
            GameObject obj;
            if (i != randNum)
                obj = (GameObject)Instantiate(ImagePrefab, spawnPoint.position, ImagePrefab.transform.rotation);
            else
            {
                obj = (GameObject)Instantiate(BossPrefab, spawnPoint.position, BossPrefab.transform.rotation);
                obj.GetComponent<Enemy>().HealthPoints = BossHealth;
            }
            BossImages.Add(obj);

        }
    }
    void CurateList(List<GameObject> l)
    {
        bool clear = false;
        for (int i = 0; i < l.Count; i++)
        {
            if (l[i] == null)
            {
                DeadImages++;
                if (DeadImages >= 3)
                {
                    PlayerReticles[0].GetComponent<Shoot>().PlayerTakeDamage();
                    clear = true;
                    break;
                }
            }
        }
        if (clear)
        {
            ClearList(BossImages);
            SpawnNPCs();
        }
        else
            DeadImages = 0;
    }
    void ClearList(List<GameObject> l)
    {
        for (int i = 0; i < l.Count; i++)  
            Destroy(l[i].gameObject);
       
        l.Clear();

    }
}
