using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDInfo : MonoBehaviour
{
    [SerializeField] public GameObject HPCount;
    [SerializeField] public GameObject LifeCount;
    [SerializeField] public GameObject LowBountyCount;
    [SerializeField] public GameObject WitchBountyCount;
    [SerializeField] public GameObject BossBountyCount;


    public void IncreaseMisses(int value)
    {

        int textNum = int.Parse(HPCount.GetComponent<TextMeshProUGUI>().text);
        textNum -= value;
        HPCount.GetComponent<TextMeshProUGUI>().text = textNum + "";
        
        if (textNum == 0)
        {
            ChangeLife(-1);
            HPCount.GetComponent<TextMeshProUGUI>().text = "99";
        }
    }
    public void ChangeLife(int value)
    {
        int textNum = int.Parse(LifeCount.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        LifeCount.GetComponent<TextMeshProUGUI>().text = textNum + "";
    }



    public void IncreaseLowBounty(int value)
    {
        
        int textNum = int.Parse(LowBountyCount.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        LowBountyCount.GetComponent<TextMeshProUGUI>().text = textNum + "";

    }
    public void IncreaseWitchBounty(int value)
    {
        int textNum = int.Parse(WitchBountyCount.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        WitchBountyCount.GetComponent<TextMeshProUGUI>().text = textNum + "";
    }
    public void IncreaseBossBounty(int value)
    {
        int textNum = int.Parse(BossBountyCount.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        BossBountyCount.GetComponent<TextMeshProUGUI>().text = textNum + "";
    }

}
