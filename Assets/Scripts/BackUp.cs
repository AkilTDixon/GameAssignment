using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUp : MonoBehaviour
{

    [SerializeField] GameObject RifleObject;
    [SerializeField] ParticleSystem MuzzleFlash;
    [SerializeField] Transform FlashPoint;
    [SerializeField] GameObject GhostEntity;

    private string[] EnemyList = {"ZombieEnemy", "ZombieEnemy(Clone)", "WitchEnemy", "WitchEnemy(Clone)" };



    private Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        RifleObject.transform.forward = transform.position - RifleObject.transform.position;
        camTransform = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.up * Time.deltaTime * 5.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (-transform.right) * Time.deltaTime * 5.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (-transform.up) * Time.deltaTime * 5.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * 5.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HitScan();
        }

        RifleObject.transform.forward = -(transform.position - RifleObject.transform.position);

    }
    void HitScan()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ParticleSystem obj = Instantiate(MuzzleFlash, FlashPoint.position, MuzzleFlash.transform.rotation);

        MuzzleFlash.transform.right = -RifleObject.transform.forward;

        bool enemyHit = false;



        RaycastHit[] hits = Physics.RaycastAll(camTransform.position, camTransform.TransformDirection(transform.position - camTransform.position), Mathf.Infinity);
        
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
                        enemy.GetComponent<Enemy>().TakeDamage(10, "eh");
                        enemyHit = true;
                    }
                  
                        
                }
            }
            if (EnemyCount >= 2)
                GetComponent<MultiKillBonus>().AddBonus(((int)Mathf.Ceil(EnemyCount / 2f)) * 5, EnemyType, "eh");
            if (enemyHit)
                Debug.Log("Hit");
            else
            {
                GameObject ghost = Instantiate(GhostEntity, Camera.main.transform.Find("GhostSpawnPoint").transform.position, GhostEntity.transform.rotation);
                ghost.transform.forward = -(ghost.transform.forward);
                Debug.Log("Miss");
            }

        }

    }
}
