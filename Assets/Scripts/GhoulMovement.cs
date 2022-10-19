using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1.5f;
    [SerializeField] float WaitIntervalMin = 5;
    [SerializeField] float WaitIntervalMax = 10;
    [SerializeField] float MoveIntervalMin = 1;
    [SerializeField] float MoveIntervalMax = 3;
    private float lastMoved;
    private float movePeriod = 0;
    private float randDir;
    private Animator anim;
    private int Sign;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        Vector3 temp = transform.forward;
        temp.z = 0;
        temp.y = 0;
        transform.forward = temp;


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "InvisibleWall")
        {
            Sign = -1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float randBase = Random.Range(0, 1000);
        

        if (randBase >= 950 && Time.time > lastMoved + Random.Range(WaitIntervalMin, WaitIntervalMax))
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
