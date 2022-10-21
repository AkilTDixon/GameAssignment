using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Damage : MonoBehaviour
{
    public void BossHit()
    {
        if (GetComponent<GhoulMovement>().Target != "")
            Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseMisses(99, GetComponent<GhoulMovement>().Target);

    }

}
