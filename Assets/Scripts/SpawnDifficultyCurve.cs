using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnDifficultyCurve : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Mode;
    [SerializeField] GameObject Stage1SpawnPoints;
    [SerializeField] GameObject Stage1WitchSpawnPoints;
    [SerializeField] GameObject Stage2SpawnPoints;
    [SerializeField] GameObject Stage2WitchSpawnPoints;
    [SerializeField] GameObject ZombiePrefab;
    [SerializeField] GameObject WitchPrefab;
    [SerializeField] GameObject BossPhase1Prefab;
    [SerializeField] GameObject BossPhase2Prefab;

    private Enemy zombie;
    private Enemy witch;
    private Enemy boss1;

    private Spawn stage1zombie;
    private WitchSpawn stage1witch;

    private Spawn stage2zombie;
    private WitchSpawn stage2witch;

    private int BaseHealthZombie;
    private int BaseHealthWitch;
    private int BaseHealthBoss1;
    private int BaseHealthBoss2;

    private float BaseActiveZombie;
    private float BaseActiveWitch;

    private float BaseEntryZombie;
    private float BaseEntryWitch;


    // Start is called before the first frame update
    void Start()
    {
        zombie = ZombiePrefab.GetComponent<Enemy>();
        witch = WitchPrefab.GetComponent<Enemy>();
        boss1 = BossPhase1Prefab.GetComponent<Enemy>();

        stage1zombie = Stage1SpawnPoints.GetComponent<Spawn>();
        stage1witch = Stage1WitchSpawnPoints.GetComponent<WitchSpawn>();

        stage2zombie = Stage2SpawnPoints.GetComponent<Spawn>();
        stage2witch = Stage2WitchSpawnPoints.GetComponent<WitchSpawn>();

        BaseHealthZombie = zombie.HealthPoints;
        BaseHealthWitch = witch.HealthPoints;
        BaseHealthBoss1 = boss1.HealthPoints;

        BaseActiveZombie = zombie.ActiveTime;
        BaseActiveWitch = witch.ActiveTime;

        BaseEntryZombie = zombie.EntrySpeed;
        BaseEntryWitch = witch.EntrySpeed;


    }
    /*
     HealthWindowPercent = (hpHolder.HealthPoints - ((hpHolder.HealthPoints - (hpHolder.HealthPoints * HealthWindowPercent)) * 2))
     /
     hpHolder.HealthPoints
     
     */
    public void IncreaseDifficulty()
    {
        boss1.GetComponent<Stage1Phase1Transition>().HealthWindowPercent = (boss1.HealthPoints - ((boss1.HealthPoints - (boss1.HealthPoints * boss1.GetComponent<Stage1Phase1Transition>().HealthWindowPercent)) * 2)) / boss1.HealthPoints;

/*        zombie.EntrySpeed *= 2f;
        zombie.ActiveTime /= 2f;
        zombie.HealthPoints += (zombie.HealthPoints / 2);

        witch.EntrySpeed *= 2f;
        witch.ActiveTime /= 2f;
        witch.HealthPoints += (witch.HealthPoints / 2);*/

        stage1zombie.SpawnNumber *= 2;
        stage1zombie.EntitiesPerInterval *= 2;
        stage1zombie.SpawnInterval /= 2;
        stage1zombie.OverrideMode = true;
        stage1zombie.EntrySpeed = BaseEntryZombie * 2f;
        stage1zombie.ActiveTime = BaseActiveZombie / 2f;
        stage1zombie.HealthPoints = BaseHealthZombie + (BaseHealthZombie / 2);

        stage2zombie.SpawnNumber *= 2;
        stage2zombie.EntitiesPerInterval *= 2;
        stage2zombie.SpawnInterval /= 2;
        stage2zombie.OverrideMode = true;
        stage2zombie.EntrySpeed = BaseEntryZombie * 2f;
        stage2zombie.ActiveTime = BaseActiveZombie / 2f;
        stage2zombie.HealthPoints = BaseHealthZombie + (BaseHealthZombie / 2);

        stage1witch.SpawnChance *= 5;
        stage1witch.SpawnChanceIncrement *= 2;
        stage1witch.OverrideMode = true;
        stage1witch.EntrySpeed = BaseEntryWitch * 2f;
        stage1witch.ActiveTime = BaseActiveWitch / 2f;
        stage1witch.HealthPoints = BaseHealthWitch + (BaseHealthWitch / 2);

        stage2witch.SpawnChance *= 3;
        stage2witch.SpawnChanceIncrement *= 2;
        stage2witch.OverrideMode = true;
        stage2witch.EntrySpeed = BaseEntryWitch * 2f;
        stage2witch.ActiveTime = BaseActiveWitch / 2f;
        stage2witch.HealthPoints = BaseHealthWitch + (BaseHealthWitch / 2);


    }
}
