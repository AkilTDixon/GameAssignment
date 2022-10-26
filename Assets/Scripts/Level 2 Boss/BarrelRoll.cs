using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRoll : MonoBehaviour
{
    public bool BarrelShot = false;
    public Vector3 direction = new Vector3(0, 0, -10);
    private string playerShot;


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Boss2Phase1")
        {
            GetComponent<ExplodeBarrel>().ObjectShot(playerShot);
        }
        
    }

    void Start()
    {
        
    }
    void Update()
    {

        GetComponent<Rigidbody>().AddForce(direction * Time.deltaTime * 500f, ForceMode.Impulse);
        if (BarrelShot)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + (transform.up * 1.5f), GetComponent<ExplodeBarrel>().ExplosiveRadius);
            bool hit = false;
            foreach (Collider nearby in colliders)
            {
                if (nearby.name == "SkeletonEnemy(Clone)" || nearby.name == "WitchEnemy(Clone)" || nearby.name == "Boss2Phase1")
                {
                    Enemy enComp = nearby.gameObject.GetComponent<Enemy>();
                    hit = true;
                    enComp.TakeDamage(50, playerShot);



                }
            }
            if (hit)
                GetComponent<ExplodeBarrel>().BarrelDeath();
        }
    }

    public void ChangeDirection(Vector3 pos, string player)
    {
        BarrelShot = true;
        direction = pos - transform.position;
        playerShot = player;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
