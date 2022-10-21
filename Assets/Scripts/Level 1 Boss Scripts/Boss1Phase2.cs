using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss1Phase2 : MonoBehaviour
{
    [SerializeField] GameObject ImagePrefab;
    [SerializeField] GameObject BossPrefab;
    [SerializeField] List<GameObject> PlayerReticles;
    [SerializeField] GameObject PhaseTimer;
    [SerializeField] GameObject PlayerEntityInfo;
    private TextMeshProUGUI PhaseTimerText;
    public Transform[] ChildObjects;
    private List<GameObject> BossImages;
    private int numOfChildren;
    private bool BossInstantiated = false;
    private int BossHealth = 30;
    private int DeadImages = 0;
    private float TimeTaken;

    // Start is called before the first frame update
    void Start()
    {

        PlayerEntityInfo.GetComponent<PlayerEntityInfo>().EndVariantMode("Boss");
        PhaseTimer.SetActive(true);
        PhaseTimerText = PhaseTimer.GetComponent<TextMeshProUGUI>();
        TimeTaken = Time.time;
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

        if (Time.time >= TimeTaken + 1f)
            PhaseTimerText.text = "1";
        if (Time.time >= TimeTaken + 2f)
            PhaseTimerText.text = "2";
        if (Time.time >= TimeTaken + 3f)
            PhaseTimerText.text = "3";
        if (Time.time >= TimeTaken + 4f)
            PhaseTimerText.text = "4";

        if (Time.time > TimeTaken + 5f)
        {
            TimeTaken = Time.time;
            PhaseTimerText.text = "0";
            PlayerReticles[0].GetComponent<Shoot>().PlayerTakeDamage(33f);
            ClearList(BossImages);
            SpawnNPCs();
        }
        CurateList(BossImages);
    }
    public void RestartPhase()
    {
        BossHealth -= 10;
        PhaseTimerText.text = "0";
        TimeTaken = Time.time;
        ClearList(BossImages);
        SpawnNPCs();
    }
    public void EndEncounter()
    {
        BossHealth -= 10;
        PhaseTimer.SetActive(false);
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
                    for (int j = 0; j < PlayerReticles.Count; j++)
                        if (PlayerReticles[j].activeSelf)
                            PlayerReticles[j].GetComponent<Shoot>().PlayerTakeDamage(33f);
                    clear = true;
                    break;
                }
            }
        }
        if (clear)
        {
            TimeTaken = Time.time;
            PhaseTimerText.text = "0";
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
