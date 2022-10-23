using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



/*
 Camera Positions

Level 1 Stage 1: -1.47, 3.9, -53

Phase 1: -61.44, -0.02, -39.36

Phase 2: -60.895, 1.86, -45.714
 
 */

/*
 green chest - 256.3795, -259.6655, 0
green count - 314.9794f, -263.1476f, 0f

brown chest - 377.2794f, -259.6655f, 0f
brown count - 430f, -263.1476f, 0f

purple chest - 482.3794f, -259.6655f, 0f
purple count - 537.0648f, -263.1476f, 0f
 
 */
public class HUDInfo : MonoBehaviour
{
    public GameObject HPCount;
    public GameObject HPImage;
    public GameObject LifeCount;
    public GameObject LifeImage;
    public GameObject LowBountyCount;
    public GameObject LowBountyImage;
    public GameObject WitchBountyCount;
    public GameObject WitchBountyImage;
    public GameObject BossBountyCount;
    public GameObject BossBountyImage;
    public GameObject VariantActive;
    public GameObject VariantIcon;
    [SerializeField] int LowMultiplierTally = 2;
    [SerializeField] int WitchMultiplierTally = 4;
    [SerializeField] int BossMultiplierTally = 20;
    [SerializeField] GameObject Player1ScoreTally;
    [SerializeField] GameObject Player2ScoreTally;
    [SerializeField] GameObject Mode;
    [SerializeField] GameObject MissionCompleteUIHolder;
    [SerializeField] GameObject GameplayUIHolder;
    [SerializeField] GameObject GameOverUIHolder;
    [SerializeField] GameObject MultiplayerUIHolder;
    public GameObject P2HPCount;
    public GameObject P2HPImage;
    public GameObject P2LifeCount;
    public GameObject P2LifeImage;
    public GameObject P2LowBountyCount;
    public GameObject P2LowBountyImage;
    public GameObject P2WitchBountyCount;
    public GameObject P2WitchBountyImage;
    public GameObject P2BossBountyCount;
    public GameObject P2BossBountyImage;
    public GameObject P2VariantActive;
    public GameObject P2VariantIcon;
    public GameObject PlayerEntityInfo;

    public AudioSource ActiveBGM;

    private float VariantCooldown = 20f;
    private float VariantActiveTime = 10f;
    private bool P1Variant = false;
    private bool P2Variant = false;
    private float P1variantStart = 0;
    private float P2variantStart = 0;
    public int NumberOfContinues = 1;
    private TextMeshProUGUI v1Text;
    private TextMeshProUGUI v2Text;
    private float P1count = 0;
    private float P2count = 0;
    public float PlayZLevel;
    private int ScoreTallyPlayer1;
    private int ScoreTallyPlayer2;
    private int ScoreTallyPlayer3;
    private int ScoreTallyPlayer4;


    void Start()
    {
        PlayZLevel = GameObject.Find("Player1").gameObject.transform.position.z;
        v1Text = VariantActive.GetComponent<TextMeshProUGUI>();
        v2Text = P2VariantActive.GetComponent<TextMeshProUGUI>();
        VariantCooldown = GameObject.Find("Player1Reticle").gameObject.GetComponent<Shoot>().VariantModeCooldown;
        VariantActiveTime = GameObject.Find("Player1Reticle").gameObject.GetComponent<Shoot>().VariantActiveTime;
    }
    public void VariantActivated(string player)
    {
        switch (player)
        {
            case "Player1":
                P1Variant = true;
                break;
            case "Player2":
                P2Variant = true;
                break;
        }                 

    }
    void Player1VariantMode()
    {
        if (P1variantStart == 0)
        {
            P1count = VariantCooldown + VariantActiveTime;
            v1Text.text = P1count + "";

            P1variantStart = Time.time;
        }
        if (Time.time >= P1variantStart + 1f)
        {
            P1count--;
            P1variantStart = Time.time;
            if (P1count == VariantActiveTime)
                 PlayerEntityInfo.GetComponent<PlayerEntityInfo>().EndVariantMode("Player1");

            if (P1count <= 0)
            {
                v1Text.text = "Ready";
                P1variantStart = 0;
                P1Variant = false;
                
            }
            else
                v1Text.text = P1count + "";
        }
    }
    void Player2VariantMode()
    {
            if (P2variantStart == 0)
            {
                P2count = VariantCooldown + VariantActiveTime;
                v2Text.text = P2count + "";

                P2variantStart = Time.time;
            }

            if (Time.time >= P2variantStart + 1f)
            {
                P2count--;
                P2variantStart = Time.time;
                if (P2count == VariantActiveTime)
                    PlayerEntityInfo.GetComponent<PlayerEntityInfo>().EndVariantMode("Player2");
                if (P2count <= 0)
                {
                    v2Text.text = "Ready";
                    P2variantStart = 0;
                    P2Variant = false;
                    //PlayerEntityInfo.GetComponent<PlayerEntityInfo>().EndVariantMode("Player2");
                }
                else
                    v2Text.text = P2count + "";
            }
    }
    void Update()
    {
        if (P1Variant)
            Player1VariantMode();
        if (P2Variant)
            Player2VariantMode();

    }

    public void EndLevel()
    {
        Time.timeScale = 0;
        int low = int.Parse(LowBountyCount.GetComponent<TextMeshProUGUI>().text);
        int witch = int.Parse(WitchBountyCount.GetComponent<TextMeshProUGUI>().text);
        int boss = int.Parse(BossBountyCount.GetComponent<TextMeshProUGUI>().text);

        print(boss);


        ScoreTallyPlayer1 = (1 + (low * LowMultiplierTally)) * (1 + (witch * WitchMultiplierTally)) * (1 + (boss * BossMultiplierTally));
        MissionCompleteUIHolder.SetActive(true);
        if (Mode.GetComponent<TextMeshProUGUI>().text == "Singleplayer")
        {
            Player2ScoreTally.SetActive(false);
        }
        else
        {
            int P2low = int.Parse(P2LowBountyCount.GetComponent<TextMeshProUGUI>().text);
            int P2witch = int.Parse(P2WitchBountyCount.GetComponent<TextMeshProUGUI>().text);
            int P2boss = int.Parse(P2BossBountyCount.GetComponent<TextMeshProUGUI>().text);

            ScoreTallyPlayer2 = (1 + (P2low * LowMultiplierTally)) * (1 + (P2witch * WitchMultiplierTally)) * (1 + (P2boss * BossMultiplierTally));

        }
        GameplayUIHolder.SetActive(false);

        Player1ScoreTally.GetComponent<TextMeshProUGUI>().text = "Player 1 Score: " + ScoreTallyPlayer1;
        Player2ScoreTally.GetComponent<TextMeshProUGUI>().text = "Player 2 Score: " + ScoreTallyPlayer2;
        PlayerEntityInfo.GetComponent<PlayerEntityInfo>().Deactivate();





    }


    void GameOver()
    {
        Time.timeScale = 0;
        GameOverUIHolder.SetActive(true);
        //PlayerEntityInfo.GetComponent<PlayerEntityInfo>().Player1.SetActive(false);
        PlayerEntityInfo.GetComponent<PlayerEntityInfo>().Player1Reticle.SetActive(false);


        if (NumberOfContinues <= 0)
            GameOverUIHolder.transform.Find("ContinueButton").gameObject.SetActive(false);
    }
    public void ContinueGameplay()
    {
        //if (Mode.GetComponent<TextMeshProUGUI>().text != "Multiplayer")
        //{
            Time.timeScale = 1f;
            GameOverUIHolder.SetActive(false);
            NumberOfContinues--;

            HPCount.GetComponent<TextMeshProUGUI>().text = "99";
            LifeCount.GetComponent<TextMeshProUGUI>().text = "3";
        //PlayerEntityInfo.GetComponent<PlayerEntityInfo>().Player1.SetActive(true);
        PlayerEntityInfo.GetComponent<PlayerEntityInfo>().Player1Reticle.SetActive(true);
        //}
    }
    T SetWhichPlayer<T>(string player, GameObject obj1, GameObject obj2)
    {
        T ret = obj1.GetComponent<T>();
        

        switch (player)
        {
            case "Player1":
                ret = obj1.GetComponent<T>();
                break;
            case "Player2":
                ret = obj2.GetComponent<T>();
                break;
        }
        return ret;
    }
    GameObject SetWhichPlayer(string player, GameObject obj1, GameObject obj2)
    {
        GameObject ret = obj1;


        switch (player)
        {
            case "Player1":
                ret = obj1;
                break;
            case "Player2":
                ret = obj2;
                break;
        }
        return ret;
    }
    public void IncreaseMisses(float value, string player)
    {
        TextMeshProUGUI HPtoChange = SetWhichPlayer<TextMeshProUGUI>(player, HPCount, P2HPCount);
        
        float textNum = float.Parse(HPtoChange.GetComponent<TextMeshProUGUI>().text);
        textNum -= value;
        HPtoChange.GetComponent<TextMeshProUGUI>().text = textNum + "";

        if (textNum <= 0)
        {
            ChangeLife(-1, player);
            HPtoChange.GetComponent<TextMeshProUGUI>().text = "99";
        }
    }
    public void ChangeLife(int value, string player)
    {
        TextMeshProUGUI LifetoChange = SetWhichPlayer<TextMeshProUGUI>(player, LifeCount, P2LifeCount);

        int textNum = int.Parse(LifetoChange.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        LifetoChange.GetComponent<TextMeshProUGUI>().text = textNum + "";

        if (textNum <= 0 && Mode.GetComponent<TextMeshProUGUI>().text == "Singleplayer")
            GameOver();
        else if (textNum <= 0 && Mode.GetComponent<TextMeshProUGUI>().text == "Multiplayer")
            DisablePlayer(player);
    }
    void DisablePlayer(string player)
    {
        if (!LifeCount.activeSelf || !P2LifeCount.activeSelf)
        {
            NumberOfContinues = 0;
            GameOver();
            return;
        }

        GameObject LifetoChange = SetWhichPlayer(player, LifeCount, P2LifeCount);


        GameObject HPtoChange = SetWhichPlayer(player, HPCount, P2HPCount);
        GameObject LowBountyToChange = SetWhichPlayer(player, LowBountyCount, P2LowBountyCount);
        GameObject WitchBountyToChange = SetWhichPlayer(player, WitchBountyCount, P2WitchBountyCount);
        GameObject BossBountyToChange = SetWhichPlayer(player, BossBountyCount, P2BossBountyCount);
        GameObject HPImageToChange = SetWhichPlayer(player, HPImage, P2HPImage);
        GameObject LifeImageToChange = SetWhichPlayer(player, LifeImage, P2LifeImage);
        GameObject LowImageToChange = SetWhichPlayer(player, LowBountyImage, P2LowBountyImage);
        GameObject WitchImageToChange = SetWhichPlayer(player, WitchBountyImage, P2WitchBountyImage);
        GameObject BossImageToChange = SetWhichPlayer(player, BossBountyImage, P2BossBountyImage);
        GameObject VariantToChange = SetWhichPlayer(player, VariantActive, P2VariantActive);
        GameObject IconToChange = SetWhichPlayer(player, VariantIcon, P2VariantIcon);


        LifeImageToChange.SetActive(false);
        LifetoChange.SetActive(false);
        HPtoChange.SetActive(false);
        LowBountyToChange.SetActive(false);
        WitchBountyToChange.SetActive(false);
        BossBountyToChange.SetActive(false);
        HPImageToChange.SetActive(false);
        LowImageToChange.SetActive(false);
        WitchImageToChange.SetActive(false);
        BossImageToChange.SetActive(false);
        VariantToChange.SetActive(false);
        IconToChange.SetActive(false);

        PlayerEntityInfo.GetComponent<PlayerEntityInfo>().DeactivatePlayer(player);

    }
    public void IncreaseLowBounty(int value, string player)
    {
        TextMeshProUGUI BountyToChange = SetWhichPlayer<TextMeshProUGUI>(player, LowBountyCount, P2LowBountyCount);

        int textNum = int.Parse(BountyToChange.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        BountyToChange.GetComponent<TextMeshProUGUI>().text = textNum + "";

    }
    public void IncreaseWitchBounty(int value, string player)
    {
        TextMeshProUGUI BountyToChange = SetWhichPlayer<TextMeshProUGUI>(player, WitchBountyCount, P2WitchBountyCount);
        int textNum = int.Parse(BountyToChange.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        BountyToChange.GetComponent<TextMeshProUGUI>().text = textNum + "";
    }
    public void IncreaseBossBounty(int value, string player)
    {
        TextMeshProUGUI BountyToChange = SetWhichPlayer<TextMeshProUGUI>(player, BossBountyCount, P2BossBountyCount);

        int textNum = int.Parse(BountyToChange.GetComponent<TextMeshProUGUI>().text);
        textNum += value;
        BountyToChange.GetComponent<TextMeshProUGUI>().text = textNum + "";

        Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().EndLevel();
    }
    public void SetMultiplayerUI()
    {
        Mode.GetComponent<TextMeshProUGUI>().text = "Multiplayer";


        LowBountyImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-619.3f, -172.2f);
        LowBountyCount.GetComponent<RectTransform>().anchoredPosition = new Vector2(-560.7f, -175.682f);

        WitchBountyImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-498.4f, -172.2f);
        WitchBountyCount.GetComponent<RectTransform>().anchoredPosition = new Vector2(-445.6794f, -175.6821f);

        BossBountyImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-393.3f, -172.2f);
        BossBountyCount.GetComponent<RectTransform>().anchoredPosition = new Vector2(-338.6146f, -175.682f);

/*        RectTransform gameplayUI = GameplayUIHolder.GetComponent<RectTransform>();
        RectTransform multiUI = MultiplayerUIHolder.GetComponent<RectTransform>();

        gameplayUI.anchorMax = new Vector2(0, 0);
        gameplayUI.anchorMin = new Vector2(0, 0);
        gameplayUI.pivot = new Vector2(0.5f, 0.5f);

        multiUI.anchorMax = new Vector2(1f, 0);
        multiUI.anchorMin = new Vector2(1f, 0);
        multiUI.pivot = new Vector2(0.5f, 0.5f);*/

        MultiplayerUIHolder.SetActive(true);


    }

}
