using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerEntityInfo : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player1Reticle;
    public GameObject Player2;
    public GameObject Player2Reticle;
    public GameObject Player3;
    public GameObject Player3Reticle;
    public GameObject Player4;
    public GameObject Player4Reticle;

    public void EndVariantMode(string player)
    {
        Shoot p1 = Player1Reticle.GetComponent<Shoot>();
        Shoot p2 = Player2Reticle.GetComponent<Shoot>();
        switch (player)
        {
            
            case "Player1":
                if (!p2.variant)
                    p1.SetDefaults();
                else
                {
                    p1.SetVariantBool();
                    p1.SetGhostDamage(p1.DamageDefault);
                    p1.RemoveSpawnMultipliers();
                }
                break;
            case "Player2":
                if (!p1.variant)
                    p2.SetDefaults();
                else
                {
                    p2.SetVariantBool();
                    p2.SetGhostDamage(p2.DamageDefault);
                    p2.RemoveSpawnMultipliers();
                }
                break;
            case "Boss":
                Player1Reticle.GetComponent<Shoot>().SetDefaults();
                Player2Reticle.GetComponent<Shoot>().SetDefaults();
                break;
        }
        
    }
    public void DeactivatePlayer(string name)
    {
        switch (name)
        {
            case "Player1":
                Player1.SetActive(false);
                Player1Reticle.SetActive(false);
                break;
            case "Player2":
                Player2.SetActive(false);
                Player2Reticle.SetActive(false);
                break;
        }
    }
    public void Deactivate()
    {
        Player1.SetActive(false);
        Player1Reticle.SetActive(false);
        Player2.SetActive(false);
        Player2Reticle.SetActive(false);
        Player3.SetActive(false);
        Player3Reticle.SetActive(false);
        Player4.SetActive(false);
        Player4Reticle.SetActive(false);
    }
}
