using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamage : MonoBehaviour
{
    public void DealDamage()
    {
        Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseMisses(GetComponent<GhostTarget>().GhostDamage, GetComponent<GhostTarget>().PlayerName);
    }
}
