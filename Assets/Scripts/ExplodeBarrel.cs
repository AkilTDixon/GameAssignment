using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBarrel : MonoBehaviour
{

    public float ExplosiveRadius = 5.0f;
    public float ExplosivePower = 10f;
    public ParticleSystem ExplosiveParticles;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SphereCollider>().radius = ExplosiveRadius;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player1")
        {
            Rigidbody rig = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 dir = collision.gameObject.transform.position - transform.position;
            rig.AddForce(dir * ExplosivePower, ForceMode.Impulse);
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Player1")
        if (other.name == "Player1")
        {
            Rigidbody rig = other.gameObject.GetComponent<Rigidbody>();

            Vector3 dir = other.gameObject.transform.position - transform.position;
            rig.AddForce(dir * ExplosivePower, ForceMode.Impulse);
        }
    }   
    void knockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosiveRadius);

        foreach(Collider nearby in colliders)
        {
            Rigidbody rig = nearby.GetComponent<Rigidbody>();

            if (rig != null)
            {
               
                    rig.velocity = Vector3.zero;
                    rig.AddExplosionForce(ExplosivePower, transform.position, ExplosiveRadius, 0.01f, ForceMode.Impulse);
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRadius);
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosiveRadius);
        
        foreach (Collider nearby in colliders)
        {
            if (nearby.name == "Player1" || nearby.name == "Player2")
            {
                Rigidbody rig = nearby.GetComponent<Rigidbody>();

                if (rig != null)
                {
                    GhostTarget barrelHit = GetComponent<GhostTarget>();
                    Vector3 dir = nearby.gameObject.transform.position - transform.position;
                    rig.AddForce(dir * ExplosivePower, ForceMode.Impulse);
                    barrelHit.SetTarget(nearby.name);
                    GetComponent<InflictDamage>().DealDamage();

                    BarrelDeath();

                }
            }
        }
    }
    
    void BarrelDeath()
    {
        ParticleSystem obj = Instantiate(ExplosiveParticles, transform.position, ExplosiveParticles.transform.rotation);
        obj.Play();
        Destroy(gameObject);
    }

    public void ObjectShot(string player)
    {
        int EnemyCount = 0;
        string EnemyType = "";
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosiveRadius);

        foreach (Collider nearby in colliders)
        {
            if (nearby.name == "SkeletonEnemy(Clone)" || nearby.name == "WitchEnemy(Clone)")
            {
                Enemy enComp = nearby.gameObject.GetComponent<Enemy>();

                EnemyCount++;
                EnemyType = enComp.EnemyType;

                enComp.TakeDamage(50, player);
                
            }
        }


        BarrelDeath();
    }
}
