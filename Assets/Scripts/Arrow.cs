using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int Damage = 10;
    private float projectileSpeed = 15.0f;
    private Rigidbody rBod;
    private int life;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Arrow(Clone)")
        {
            
            if (collision.gameObject.name == "ZombieEnemy")
                collision.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
            
            Destroy(gameObject);
        }
    }


    void Awake()
    {
        life = 0;
        rBod = GetComponent<Rigidbody>();
        rBod.velocity = Vector3.right * projectileSpeed;
    }
    void Update()
    {
        life++;
        if (life > 1000)
            Destroy(gameObject);
    }
    public void SetDirection(Vector3 direction)
    {
        rBod.velocity = direction * projectileSpeed;
    }
}
