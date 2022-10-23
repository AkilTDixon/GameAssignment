using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiKillBonus : MonoBehaviour
{
    public void AddBonus(int value, string type, string player)
    {
        switch (type)
        {
            case "SkeletonEnemy(Clone)":
                Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseLowBounty(value, player);
                break;
            case "ZombieEnemy(Clone)":
                Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseLowBounty(value, player);
                break;
            case "WitchEnemy(Clone)":
                Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseWitchBounty(value, player);
                break;
        }
    }
}
