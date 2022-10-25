using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulMovement : MonoBehaviour
{
    public float MoveSpeed = 1.5f;
    [SerializeField] float WaitIntervalMin = 5;
    [SerializeField] float WaitIntervalMax = 10;
    [SerializeField] float MoveIntervalMin = 1;
    [SerializeField] float MoveIntervalMax = 3;
    [SerializeField] string EnemyType = "Zombie";
    [SerializeField] Vector3 MoveDirection;
    [SerializeField] int PhaseNumber;
    public string Target;


    private float lastMoved;
    private float movePeriod = 0;
    private float randDir;
    private Animator anim;
    private int Sign;
    private bool Attacking;
    private int ChanceToMove = 950;
    void Start()
    {
        Target = "";
        if (EnemyType == "Image")
            ChanceToMove = 5000;
        anim = GetComponent<Animator>();
        Vector3 temp = transform.forward;
        //temp.z = 0;
        //temp.y = 0;
        transform.forward = temp;



        Attacking = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (EnemyType == "Zombie" || EnemyType == "Witch")
        {
            if (collision.gameObject.name == "InvisibleWall")
                Sign = -1;      
        }
        else
        {
            if (collision.collider.name == "Player1" || collision.collider.name == "Player2" || collision.collider.name == "Player3" || collision.collider.name == "Player4")
            {
                //ChanceToMove = 1200;
                Target = collision.collider.name;
                Attacking = true;
               // MoveSpeed = 0;
                anim.SetFloat("MovePeriod", 0);
                anim.SetBool("PlayerClose", true);
            }
        }
        if (EnemyType == "Skeleton")
        {
            if (collision.gameObject.name == "Wall")
                Sign = -1;
            if (collision.collider.name == "Player1" || collision.collider.name == "Player2" || collision.collider.name == "Player3" || collision.collider.name == "Player4")
            {
                Target = collision.collider.name;
                GetComponent<GhostTarget>().PlayerName = Target;
                Attacking = true;
                anim.SetFloat("MovePeriod", 0);
                anim.SetBool("PlayerClose", true);
                ChanceToMove = 1200;
            }
        }
            

    }
    private void OnCollisionExit(Collision collision)
    {
        if (EnemyType == "Boss" || EnemyType == "Image")
        {

            if (collision.collider.name == "Player1" || collision.collider.name == "Player2" || collision.collider.name == "Player3" || collision.collider.name == "Player4")
            {
                //ChanceToMove = 950;
                Attacking = false;
                //MoveSpeed = 0;
                anim.SetFloat("MovePeriod", 1);
                anim.SetBool("PlayerClose", false);
            }
        }
        if (EnemyType == "Skeleton")
        {
            if (collision.collider.name == "Player1" || collision.collider.name == "Player2" || collision.collider.name == "Player3" || collision.collider.name == "Player4")
            {
                ChanceToMove = 950;
                Attacking = false;
                anim.SetFloat("MovePeriod", 1);
                anim.SetBool("PlayerClose", false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (EnemyType == "Zombie" || EnemyType == "Witch" || EnemyType == "Image" || EnemyType == "Skeleton")
            EnemyMovement();
        else
            BossMovement();

       
    }
    void BossMovement()
    {
        if (PhaseNumber == 1)
        {
            if (!Attacking)
            {
                
                anim.SetFloat("MovePeriod", 1);

                transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(MoveDirection);

                lastMoved = Time.time;

            }
        }
    }
    void EnemyMovement()
    {
        float randBase = Random.Range(0, 1000);


        if (randBase >= ChanceToMove && Time.time > lastMoved + Random.Range(WaitIntervalMin, WaitIntervalMax))
        {
            if (movePeriod == 0)
            {
                movePeriod = Random.Range(MoveIntervalMin, MoveIntervalMax);
                lastMoved = Time.time;
                randDir = Random.Range(-1f, 1f);
                Sign = 1;

            }

        }

        //Move
        if (movePeriod > 0)
        {

            anim.SetFloat("MovePeriod", movePeriod);

            if (randDir > 0)
                randDir = 1f;
            else if (randDir < 0)
                randDir = -1f;



            transform.position += new Vector3((Sign * randDir) * MoveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.LookRotation(new Vector3(Sign * randDir, 0, 0));

            if (Time.time - lastMoved > movePeriod)
            {
                print("Move Period: " + movePeriod + " | Time elapsed: " + (Time.time - lastMoved));
                movePeriod = 0;
                lastMoved = Time.time;
                anim.SetFloat("MovePeriod", movePeriod);
            }


        }
    }
}
