using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiKillBonus : MonoBehaviour
{
    public void AddBonus(int value, string type)
    {
        switch (type)
        {
            case "ZombieEnemy(Clone)":
                Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseLowBounty(value);
                break;
            case "WitchEnemy(Clone)":
                Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseWitchBounty(value);
                break;
        }
    }
}
