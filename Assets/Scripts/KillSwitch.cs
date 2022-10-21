using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSwitch : MonoBehaviour
{
    void KillMonster()
    {
        Destroy(gameObject);
    }

    void GhostCall()
    {
        
        Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseMisses(GetComponent<GhostTarget>().GhostDamage, GetComponent<GhostTarget>().PlayerName);
    }

}
