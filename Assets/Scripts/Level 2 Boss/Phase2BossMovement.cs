using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2BossMovement : MonoBehaviour
{

    [SerializeField] GameObject playerEntityInfo;
    [SerializeField] float MoveSpeed = 5f;
    private PlayerEntityInfo PEI;
    private Animator anim;
    private bool Attacking = false;
    private float lastEnrageCheck = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player1" || collision.collider.name == "Player2" || collision.collider.name == "Player3" || collision.collider.name == "Player4")
        {
           
            GetComponent<GhostTarget>().PlayerName = collision.collider.name;
            Attacking = true;
            anim.SetFloat("MovePeriod", 0);
            anim.SetBool("PlayerClose", true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "Player1" || collision.collider.name == "Player2" || collision.collider.name == "Player3" || collision.collider.name == "Player4")
        {

            Attacking = false;
            anim.SetFloat("MovePeriod", 1);
            anim.SetBool("PlayerClose", false);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("MovePeriod", 1);
        PEI = playerEntityInfo.GetComponent<PlayerEntityInfo>();
    }
    float Dist(Vector3 p1, Vector3 p2)
    {
        return Mathf.Sqrt( ((p2.x - p1.x)* (p2.x - p1.x)) + ((p2.y - p1.y) * (p2.y - p1.y)) + ((p2.z - p1.z) * (p2.z - p1.z)));
    }
    void EnrageCheck()
    {
        


        if (lastEnrageCheck == 0f)
            lastEnrageCheck = Time.time;

        if (Time.time > lastEnrageCheck + 8f)
        {
            transform.position = PEI.Player1.transform.position;
            anim.speed = 10f;
        }
    }
    void Update()
    {
        if (PEI.Player1.transform.position.y > 8f)
            EnrageCheck();
        else
            lastEnrageCheck = 0f;

/*        if (Dist(transform.position, PEI.Player1.transform.position) >= 15f)
        {
            Attacking = false;
            anim.SetFloat("MovePeriod", 1);
            anim.SetBool("PlayerClose", false);
        }*/

            Vector3 dir = PEI.Player1.transform.position - transform.position;
        dir.y = 0;
        dir.z = 0;

        if (dir.x > 0)
            dir.x = 1;
        else if (dir.x < 0)
            dir.x = -1;
        else
            dir.x = 0;

        if (!Attacking)
            transform.position += new Vector3(dir.x * MoveSpeed * Time.deltaTime, 0, 0);
        
        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, 0));
        

    }
}
