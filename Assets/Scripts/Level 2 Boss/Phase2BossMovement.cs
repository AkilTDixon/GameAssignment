using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase2BossMovement : MonoBehaviour
{

    [SerializeField] GameObject playerEntityInfo;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] GameObject Phase2Timer;

    private TextMeshProUGUI Phase2EnrageText;
    private GameObject Target;
    private PlayerEntityInfo PEI;
    private Animator anim;
    private bool Attacking = false;
    private bool Enraged = false;
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
        Phase2EnrageText = Phase2Timer.GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
        anim.SetFloat("MovePeriod", 1);
        PEI = playerEntityInfo.GetComponent<PlayerEntityInfo>();
        Target = PEI.Player1;
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
            Phase2Timer.SetActive(true);
            Phase2EnrageText.text = "ENRAGED!";
            Enraged = true;
            transform.position = Target.transform.position;
            anim.speed = 10f;
        }
    }
    void Update()
    {
        if (!Target.activeSelf)
        {
            GetComponent<GhostTarget>().PlayerName = "Player2";
            Attacking = false;
            anim.SetFloat("MovePeriod", 1);
            anim.SetBool("PlayerClose", false);
            Target = PEI.Player2;
        }
        if (!Enraged)
        {
            if (Target.transform.position.y > 8f)
                EnrageCheck();
            else
                lastEnrageCheck = 0f;
        }
        else
        {
            transform.position = Target.transform.position;
        }


        Vector3 dir = Target.transform.position - transform.position;
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
