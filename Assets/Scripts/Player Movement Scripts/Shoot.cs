using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{

    [SerializeField] ParticleSystem MuzzleFlash;
    [SerializeField] Transform FlashPoint;
    [SerializeField] GameObject PlayerEntity;
    [SerializeField] GameObject GhostEntity;
    [SerializeField] Transform GhostSpawnPoint;
    [SerializeField] Transform AimPoint;
    [SerializeField] float Range = 4f;
    [SerializeField] AudioSource[] GunSounds;

    [SerializeField] GameObject Stage1SpawnPoints;
    [SerializeField] GameObject Stage2SpawnPoints;
    [SerializeField] GameObject Stage1WitchSpawnPoints;
    [SerializeField] GameObject Stage2WitchSpawnPoints;
    [SerializeField] float GhostDamage = 33;
    public float VariantModeCooldown = 20f;
    public float VariantActiveTime = 10f;

    private float VariantStart = 0;
    public Vector3 MouseP;
    public bool Boss1Phase1;
    private bool DepthMode;
    private GameObject Mode;
    private GameObject HUD;
    private float NormalRange;
    public bool variant = false;
    private string[] EnemyList = { "ZombieEnemy", "ZombieEnemy(Clone)", "WitchEnemy", "WitchEnemy(Clone)", "Boss1Phase1", "Boss1Phase2(Clone)", "Boss1Phase2Image(Clone)", "SkeletonEnemy", "SkeletonEnemy(Clone)", "ExplosiveBarrel", "ExplosiveBarrel(Clone)" };
    private float lastShot = 0;

    //Defaults
    public float DamageDefault;

    private int Stage1SpawnNumber;
    private float Stage1SpawnInterval;
    private int Stage1EntitiesPerInterval;

    private int Stage2SpawnNumber;
    private float Stage2SpawnInterval;
    private int Stage2EntitiesPerInterval;

    private float Stage1WitchSpawnChance;
    private float Stage1WitchSpawnChanceIncrement;
    
    private float Stage2WitchSpawnChance;
    private float Stage2WitchSpawnChanceIncrement;
    //
    
    private Transform camTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        DamageDefault = GhostDamage;

        Stage1WitchSpawnChance = Stage1WitchSpawnPoints.GetComponent<WitchSpawn>().SpawnChance;
        Stage2WitchSpawnChance = Stage2WitchSpawnPoints.GetComponent<WitchSpawn>().SpawnChance;

        Stage1WitchSpawnChanceIncrement = Stage1WitchSpawnPoints.GetComponent<WitchSpawn>().SpawnChanceIncrement;
        Stage2WitchSpawnChanceIncrement = Stage2WitchSpawnPoints.GetComponent<WitchSpawn>().SpawnChanceIncrement;

        Stage1SpawnNumber = Stage1SpawnPoints.GetComponent<Spawn>().SpawnNumber;
        Stage1SpawnInterval = Stage1SpawnPoints.GetComponent<Spawn>().SpawnInterval;
        Stage1EntitiesPerInterval = Stage1SpawnPoints.GetComponent<Spawn>().EntitiesPerInterval;

        Stage2SpawnNumber = Stage2SpawnPoints.GetComponent<Spawn>().SpawnNumber;
        Stage2SpawnInterval = Stage2SpawnPoints.GetComponent<Spawn>().SpawnInterval;
        Stage2EntitiesPerInterval = Stage2SpawnPoints.GetComponent<Spawn>().EntitiesPerInterval;

        GunSounds = PlayerEntity.GetComponents<AudioSource>();
        Random.InitState(((int)System.DateTime.Now.Ticks));
        NormalRange = Range;
        Boss1Phase1 = false;
        DepthMode = false;
        HUD = Camera.main.transform.Find("HUD").gameObject;
        Mode = HUD.transform.Find("GameplayUI").Find("Mode").gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {

        
        if (Mode.GetComponent<TextMeshProUGUI>().text == "Singleplayer")
            Singleplayer();
        else
            Multiplayer();

        //RangeCheck();

    }
    void Singleplayer()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            DepthMode = !DepthMode;

        if (Input.GetKeyDown(KeyCode.Q) && !variant && (Time.time > VariantStart + (VariantModeCooldown + VariantActiveTime) || VariantStart == 0))   
            VariantMode();
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !variant)
            HitScan();
        else if (Input.GetKey(KeyCode.Mouse0) && Time.time > lastShot + 0.1f && variant)
        {
            HitScan();
        }
        PlayerEntity.transform.forward = -(transform.position - AimPoint.position);


        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mouseScreenPosition.z - (Camera.main.transform.position.z + (-PlayerEntity.transform.position.z));
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        if (DepthMode)
        {
            if (Range != Mathf.Infinity)
                Range = Mathf.Infinity;
            //mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = mouseScreenPosition.z - (Camera.main.transform.position.z + ((-PlayerEntity.transform.position.z) * 3f));
            
            mPos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            MouseP = mPos;
            transform.position = ClampToDepthMode(mPos);
        }
        else
        {
            if (Range != NormalRange)
                Range = NormalRange;
            mPos.z = PlayerEntity.transform.position.z;
            transform.position = ClampToRange(mPos);
        }

    }
    public void SetVariantBool()
    {
        variant = false;
    }
    public void RemoveSpawnMultipliers()
    {
        Spawn s1 = Stage1SpawnPoints.GetComponent<Spawn>();
        //WitchSpawn ws1 = Stage1WitchSpawnPoints.GetComponent<WitchSpawn>();

        Spawn s2 = Stage2SpawnPoints.GetComponent<Spawn>();
        //WitchSpawn ws2 = Stage2WitchSpawnPoints.GetComponent<WitchSpawn>();

        s2.SpawnNumber /= 5;
        s1.SpawnNumber /= 5;
    }
    public void SetSpawnDefaults()
    {
        Spawn s1 = Stage1SpawnPoints.GetComponent<Spawn>();
        WitchSpawn ws1 = Stage1WitchSpawnPoints.GetComponent<WitchSpawn>();

        Spawn s2 = Stage2SpawnPoints.GetComponent<Spawn>();
        WitchSpawn ws2 = Stage2WitchSpawnPoints.GetComponent<WitchSpawn>();

/*        ws1.VariantMode = false;
        ws1.VariantStart = 0;
        s1.VariantMode = false;
        s1.VariantStart = 0;

        ws2.VariantStart = 0;
        ws2.VariantMode = false;
        s2.VariantMode = false;
        s2.VariantStart = 0;*/

        s1.SpawnNumber /= 5;
        s1.SpawnInterval = Stage1SpawnInterval;
        s1.EntitiesPerInterval = Stage1EntitiesPerInterval;

        s2.SpawnNumber /= 5;
        s2.SpawnInterval = Stage2SpawnInterval;
        s2.EntitiesPerInterval = Stage2EntitiesPerInterval;

        ws1.SpawnChance = Stage1WitchSpawnChance;
        ws1.SpawnChanceIncrement = Stage1WitchSpawnChanceIncrement;

        ws2.SpawnChance = Stage2WitchSpawnChance;
        ws2.SpawnChanceIncrement = Stage2WitchSpawnChanceIncrement;
    }
    public void SetDefaults()
    {
        Spawn s1 = Stage1SpawnPoints.GetComponent<Spawn>();
        WitchSpawn ws1 = Stage1WitchSpawnPoints.GetComponent<WitchSpawn>();

        Spawn s2 = Stage2SpawnPoints.GetComponent<Spawn>();
        WitchSpawn ws2 = Stage2WitchSpawnPoints.GetComponent<WitchSpawn>();

        variant = false;

        ws1.VariantMode = false;
        ws1.VariantStart = 0;
        s1.VariantMode = false;
        s1.VariantStart = 0;

        ws2.VariantStart = 0;
        ws2.VariantMode = false;
        s2.VariantMode = false;
        s2.VariantStart = 0;

        s1.SpawnNumber /= 5;
        s1.SpawnInterval = Stage1SpawnInterval;
        s1.EntitiesPerInterval = Stage1EntitiesPerInterval;

        s2.SpawnNumber /= 5;
        s2.SpawnInterval = Stage2SpawnInterval;
        s2.EntitiesPerInterval = Stage2EntitiesPerInterval;

        ws1.SpawnChance = Stage1WitchSpawnChance;
        ws1.SpawnChanceIncrement = Stage1WitchSpawnChanceIncrement;

        ws2.SpawnChance = Stage2WitchSpawnChance;
        ws2.SpawnChanceIncrement = Stage2WitchSpawnChanceIncrement;

        GhostDamage = DamageDefault;

    }
    public void SetGhostDamage(float value)
    {
        GhostDamage = value;
    }
    void VariantMode()
    {
        Spawn s1 = Stage1SpawnPoints.GetComponent<Spawn>();
        WitchSpawn ws1 = Stage1WitchSpawnPoints.GetComponent<WitchSpawn>();

        Spawn s2 = Stage2SpawnPoints.GetComponent<Spawn>();
        WitchSpawn ws2 = Stage2WitchSpawnPoints.GetComponent<WitchSpawn>();

        variant = true;
        VariantStart = Time.time;

        HUD.GetComponent<HUDInfo>().VariantActivated(PlayerEntity.gameObject.name);

        ws1.VariantMode = true;
        ws1.vActiveTime = VariantActiveTime;
        s1.VariantMode = true;
        s1.vActiveTime = VariantActiveTime;

        ws2.vActiveTime = VariantActiveTime;
        ws2.VariantMode = true;
        s2.VariantMode = true;
        s2.vActiveTime = VariantActiveTime;

        //Stage1SpawnNumber = s1.SpawnNumber;
        s1.SpawnNumber *= 5;
        //Stage1SpawnInterval = s1.SpawnInterval;
        s1.SpawnInterval *= 0.5f;
        //Stage1EntitiesPerInterval = s1.EntitiesPerInterval;
        s1.EntitiesPerInterval = 5;

        //Stage2SpawnNumber = s2.SpawnNumber;
        s2.SpawnNumber *= 5;
        //Stage2SpawnInterval = s2.SpawnInterval;
        s2.SpawnInterval *= 0.5f;
        //Stage2EntitiesPerInterval = s2.EntitiesPerInterval;
        s2.EntitiesPerInterval = 5;

        ws1.SpawnChance = 0.1f;
        ws1.SpawnChanceIncrement = 0.1f;

        ws2.SpawnChance = 0.1f;
        ws2.SpawnChanceIncrement = 0.1f;

        GhostDamage = 0.5f;


    }
    Vector3 ClampToDepthMode(Vector3 pos)
    {
        pos.y = AimPoint.position.y;
        pos.x = AimPoint.position.x;
        //if (pos.z < AimPoint.position.z)
            //pos.z = AimPoint.position.z;

        return pos;
    }
    Vector3 ClampToRange(Vector3 pos)
    {
        if (pos.y < AimPoint.position.y - Range)
            pos.y = AimPoint.position.y - Range;
        else if (pos.y > AimPoint.position.y + Range)
            pos.y = AimPoint.position.y + Range;

        if (pos.x < AimPoint.position.x - Range)
            pos.x = AimPoint.position.x - Range;
        else if (pos.x > AimPoint.position.x + Range)
            pos.x = AimPoint.position.x + Range;


        return pos;
    }
    void Multiplayer()
    {
        Vector3 nextChange;

        if (gameObject.name == "Player1Reticle")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                DepthMode = !DepthMode;

            if (Input.GetKeyDown(KeyCode.Q) && !variant && (Time.time > VariantStart + (VariantModeCooldown + VariantActiveTime) || VariantStart == 0))
                VariantMode();

            if (Input.GetKey(KeyCode.W))
            {
                nextChange = transform.position + transform.up * Time.deltaTime * 5.0f;
                if (nextChange.y < AimPoint.position.y + Range)
                    transform.position += transform.up * Time.deltaTime * 5.0f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                nextChange = transform.position + (-transform.up) * Time.deltaTime * 5.0f;
                if (nextChange.y > AimPoint.position.y - Range)
                    transform.position += (-transform.up) * Time.deltaTime * 5.0f;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && !variant)
            {
                HitScan();

            }
            else if (Input.GetKey(KeyCode.LeftControl) && Time.time > lastShot + 0.1f && variant)
            {
                HitScan();
            }
        }
        else if (gameObject.name == "Player2Reticle")
        {
            if (Input.GetKeyDown(KeyCode.Keypad7))
                DepthMode = !DepthMode;

            if (Input.GetKeyDown(KeyCode.Keypad1) && !variant && (Time.time > VariantStart + (VariantModeCooldown + VariantActiveTime) || VariantStart == 0))
                VariantMode();

            if (Input.GetKey(KeyCode.Keypad8))
            {
                nextChange = transform.position + transform.up * Time.deltaTime * 5.0f;
                if (nextChange.y < AimPoint.position.y + Range)
                    transform.position += transform.up * Time.deltaTime * 5.0f;
            }
            if (Input.GetKey(KeyCode.Keypad5))
            {
                nextChange = transform.position + (-transform.up) * Time.deltaTime * 5.0f;
                if (nextChange.y > AimPoint.position.y - Range)
                    transform.position += (-transform.up) * Time.deltaTime * 5.0f;
            }
            if (Input.GetKeyDown(KeyCode.Keypad0) && !variant)
            {
                HitScan();
            }
            else if (Input.GetKey(KeyCode.Keypad0) && Time.time > lastShot + 0.1f && variant)
            {
                HitScan();
            }

        }



        Vector3 newPos = AimPoint.position;

        //Fixed X position
        if (PlayerEntity.transform.forward.x > 0)
            newPos = new Vector3(AimPoint.position.x - Range, transform.position.y, transform.position.z);
        else
            newPos = new Vector3(AimPoint.position.x + Range, transform.position.y, transform.position.z);


        if (DepthMode)
        {
            if (Range != Mathf.Infinity)
                Range = Mathf.Infinity;
            
            newPos.z = - (Camera.main.transform.position.z + ((-PlayerEntity.transform.position.z) * 3f));

            //mPos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            transform.position = ClampToDepthMode(newPos);
        }
        else
        {
            if (Range != NormalRange)
                Range = NormalRange;
            newPos.z = PlayerEntity.transform.position.z;
            transform.position = ClampToRange(newPos);
        }

        PlayerEntity.transform.forward = -(transform.position - AimPoint.position);
    }
/*    void RangeCheck()
    {
        Vector3 direction = transform.position - AimPoint.position;

        RaycastHit[] hits = Physics.RaycastAll(AimPoint.position, direction, Range);


        if (hits.Length > 0)
        {
            for (int j = 0; j < hits.Length; j++)
            {
                for (int i = 0; i < EnemyList.Length; i++)
                {
                    if (hits[j].collider.name == EnemyList[i])
                    {
                        hits[j].collider.GetComponent<Enemy>().HighlightEnemy();
                    }
                }
            }
        }
    }*/
    void HitScan()
    {
        lastShot = Time.time;
        int randNum = Random.Range(0, GunSounds.Length - 1);

        GunSounds[randNum].Play();

        ParticleSystem obj = Instantiate(MuzzleFlash, FlashPoint.position, MuzzleFlash.transform.rotation);

        MuzzleFlash.transform.right = AimPoint.up;

        bool enemyHit = false;



        RaycastHit[] hits = Physics.RaycastAll(AimPoint.position, transform.position - AimPoint.position, Range);

        if (hits.Length > 0)
        {
            int EnemyCount = 0;
            string EnemyType = "";
            for (int j = 0; j < hits.Length; j++)
            {

                for (int i = 0; i < EnemyList.Length; i++)
                {

                    if (hits[j].collider.name == EnemyList[i])
                    {
                        EnemyCount++;
                        EnemyType = hits[j].collider.name;
                        GameObject enemy = hits[j].collider.gameObject;
                        if (enemy.GetComponent<Enemy>() != null)
                            enemy.GetComponent<Enemy>().TakeDamage(10, PlayerEntity.gameObject.name);
                        else if (enemy.GetComponent<DamageableEnvironment>() != null)
                            enemy.GetComponent<DamageableEnvironment>().TakeDamage(10, PlayerEntity.gameObject.name);
                        enemyHit = true;
                    }


                }
            }
            if (EnemyCount >= 2)
                GetComponent<MultiKillBonus>().AddBonus(((int)Mathf.Ceil(EnemyCount / 2f)) * 5, EnemyType, PlayerEntity.gameObject.name);
            if (enemyHit)
                Debug.Log("Hit");
            else
            {
                if (!Boss1Phase1)
                {
                    PlayerTakeDamage(GhostDamage);
                }
            }

        }
        else
        {
            if (!Boss1Phase1)
            {
                PlayerTakeDamage(GhostDamage);
            }
        }

    }
    public void PlayerTakeDamage(float value)
    {
        GameObject ghost = Instantiate(GhostEntity, GhostSpawnPoint.position, GhostEntity.transform.rotation);
        ghost.GetComponent<GhostTarget>().SetTarget(PlayerEntity.gameObject.name);
        ghost.GetComponent<GhostTarget>().SetDamage(value);
        //ghost.transform.SetParent(PlayerEntity.transform);
        Vector3 towardPos = PlayerEntity.transform.position;
        towardPos.y = 0;
        towardPos.z = 0;
        ghost.transform.rotation = Quaternion.LookRotation(towardPos);
        Debug.Log("Miss");
    }
}
