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
    public Vector3 MouseP;
    public bool Boss1Phase1;
    private bool DepthMode;
    private GameObject Mode;
    private float NormalRange;
    
    private string[] EnemyList = { "ZombieEnemy", "ZombieEnemy(Clone)", "WitchEnemy", "WitchEnemy(Clone)", "Boss1Phase1", "Boss1Phase2(Clone)", "Boss1Phase2Image(Clone)" };



    private Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        NormalRange = Range;
        Boss1Phase1 = false;
        DepthMode = false;
        Mode = Camera.main.transform.Find("HUD").Find("Mode").gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {

        
        if (Mode.GetComponent<TextMeshProUGUI>().text == "Singleplayer")
            Singleplayer();
        else
            Multiplayer();

        RangeCheck();

    }
    void Singleplayer()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            DepthMode = !DepthMode;



        if (Input.GetKeyDown(KeyCode.Mouse0))
            HitScan();

        PlayerEntity.transform.forward = -(transform.position - AimPoint.position);


        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mouseScreenPosition.z - (Camera.main.transform.position.z + 5.07f);
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        if (DepthMode)
        {
            if (Range != Mathf.Infinity)
                Range = Mathf.Infinity;
            //mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = mouseScreenPosition.z - (Camera.main.transform.position.z + 12f);
            
            mPos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            MouseP = mPos;
            transform.position = ClampToDepthMode(mPos);
        }
        else
        {
            if (Range != NormalRange)
                Range = NormalRange;
            mPos.z = -5.07f;
            transform.position = ClampToRange(mPos);
        }

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
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                HitScan();

            }
        }
        else if (gameObject.name == "Player2Reticle")
        {
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
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                HitScan();

            }
        }
        Vector3 newPos = AimPoint.position;

        if (PlayerEntity.transform.forward.x > 0)
            newPos = new Vector3(AimPoint.position.x - Range, transform.position.y, transform.position.z);
        else
            newPos = new Vector3(AimPoint.position.x + Range, transform.position.y, transform.position.z);

        if (newPos.y < AimPoint.position.y - Range)
            newPos.y = AimPoint.position.y - Range;
        else if (newPos.y > AimPoint.position.y + Range)
            newPos.y = AimPoint.position.y + Range;

        transform.position = newPos;
        PlayerEntity.transform.forward = -(transform.position - AimPoint.position);
    }
    void RangeCheck()
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
    }
    void HitScan()
    {

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
                        enemy.GetComponent<Enemy>().TakeDamage(10);
                        enemyHit = true;
                    }


                }
            }
            if (EnemyCount >= 2)
                GetComponent<MultiKillBonus>().AddBonus(((int)Mathf.Ceil(EnemyCount / 2f)) * 5, EnemyType);
            if (enemyHit)
                Debug.Log("Hit");
            else
            {
                if (!Boss1Phase1)
                {
                    PlayerTakeDamage();
                }
            }

        }
        else
        {
            if (!Boss1Phase1)
            {
                PlayerTakeDamage();
            }
        }

    }
    public void PlayerTakeDamage()
    {
        GameObject ghost = Instantiate(GhostEntity, GhostSpawnPoint.position, GhostEntity.transform.rotation);
        //ghost.transform.SetParent(PlayerEntity.transform);
        Vector3 towardPos = PlayerEntity.transform.position;
        towardPos.y = 0;
        towardPos.z = 0;
        ghost.transform.rotation = Quaternion.LookRotation(towardPos);
        Debug.Log("Miss");
    }
}
