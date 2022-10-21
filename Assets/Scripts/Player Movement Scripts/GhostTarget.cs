using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTarget : MonoBehaviour
{
    public string PlayerName = "Player1";
    public float GhostDamage = 33;

    public void SetTarget(string name)
    {
        PlayerName = name;
    }
    public void SetDamage(float value)
    {
        GhostDamage = value;
    }
}
