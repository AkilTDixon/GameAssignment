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

    private string Mode;
    private string[] EnemyList = { "ZombieEnemy", "ZombieEnemy(Clone)", "WitchEnemy", "WitchEnemy(Clone)" };



    private Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        Mode = Camera.main.transform.Find("HUD").Find("Mode").GetComponent<TextMeshProUGUI>().text;
    }
    
    // Update is called once per frame
    void Update()
    {

        RangeCheck();
        if (Mode == "Singleplayer")
            Singleplayer();
        else
            Multiplayer();
     


    }
void Singleplayer()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            HitScan();

        PlayerEntity.transform.forward = -(transform.position - AimPoint.position);

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = mouseScreenPosition.z - (Camera.main.transform.position.z + 5.07f);
        Vector3 mPos = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mPos.z = -5.07f;
        

        transform.position = ClampToRange(mPos);
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

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                GameObject ghost = Instantiate(GhostEntity, GhostSpawnPoint.position, GhostEntity.transform.rotation);
                Vector3 towardPos = PlayerEntity.transform.position;
                towardPos.y = 0;
                towardPos.z = 0;
                //ghost.transform.SetParent(PlayerEntity.transform);
                ghost.transform.rotation = Quaternion.LookRotation(towardPos);
                Debug.Log("Miss");
            }

        }
        else
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
}
