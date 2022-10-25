using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnvironment : MonoBehaviour
{


    public void TakeDamage(int damage, string player)
    {
        GetComponent<ExplodeBarrel>().ObjectShot(player);
    }
}
