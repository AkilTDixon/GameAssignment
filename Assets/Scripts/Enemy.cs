using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int HealthPoints = 100;
    [SerializeField] private GameObject TextHolder;
    [SerializeField] private float ActiveTime = 5f;
    [SerializeField] private string EnemyType;
    [SerializeField] private GameObject AssociatedBountyCount;
    [SerializeField] private float EntrySpeed = -0.01f;

    private GameObject BountyGraphic;
    private List<Material> baseMaterial;
    private Material DamageTaken;
    private Material HighlightMaterial;
    private List<GameObject> Limbs;
    private Animator anim;
    private TextMeshPro tmsh;
    private float spawnTime;
    private Quaternion baseRotation;
    private bool deadAnim;
    private bool tookDamage;
    private bool IntroDone;
    private float lastDamage;
    private float lastFade;
    public bool Highlighted;

    
    void Start()
    {
        IntroDone = false;
        Highlighted = false;
        anim = GetComponent<Animator>();
        tmsh = TextHolder.GetComponent<TextMeshPro>();
        tmsh.text = "HP: " + HealthPoints;
        baseRotation = tmsh.transform.rotation;
        deadAnim = false;
        tookDamage = false;
        lastDamage = 0;
        lastFade = 0;
        baseMaterial = GetComponent<LimbInfo>().BaseMaterial; 
        Limbs = GetComponent<LimbInfo>().Limbs;
        DamageTaken = GetComponent<LimbInfo>().DamageTakenMaterial;
        HighlightMaterial = GetComponent<LimbInfo>().HightlightMaterial;
    }
    void Update()
    {

        if (transform.position.z > -5.07f)
        {
            anim.SetFloat("MovePeriod", 1);
            GetComponent<GhoulMovement>().enabled = false;
            transform.position += new Vector3(0, 0, EntrySpeed);
            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
            if (transform.position.z <= -5.07f)
                IntroDone = true;
        }
        else
        {
            
            if (!Highlighted)
                for (int i = 0; i < Limbs.Count; i++)
                    Limbs[i].GetComponent<SkinnedMeshRenderer>().material = baseMaterial[i];

            if (IntroDone)
            {
                if (transform.position.z > -5.07f)
                {
                    Vector3 temp = transform.position;
                    temp.z = -5.07f;
                    transform.position = temp;
                }
                anim.SetFloat("MovePeriod", 0);
                GetComponent<GhoulMovement>().enabled = true;
                spawnTime = Time.time;
                transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                IntroDone = false;
            }
            if (tookDamage && Time.time > lastDamage + 0.25f)
            {
                for (int i = 0; i < Limbs.Count; i++)
                    Limbs[i].GetComponent<SkinnedMeshRenderer>().material = baseMaterial[i];
                tookDamage = false;
            }

            tmsh.transform.rotation = baseRotation;
            if (Time.time - spawnTime > ActiveTime && !deadAnim)
            {
                if (Time.time > lastFade + 0.05f)
                {
                    FadeOut();

                }
            }
        }
        Highlighted = false;
    }
    void FadeOut()
    {
        lastFade = Time.time;
        for (int i = 0; i < Limbs.Count; i++)
        {
            
            Color col = Limbs[i].GetComponent<SkinnedMeshRenderer>().material.color;
            if (col.a <= 0)
                Destroy(gameObject);
            col.a -= 0.05f;
            Limbs[i].GetComponent<SkinnedMeshRenderer>().material.color = col;
        }
        
    }
    public void HighlightEnemy()
    {
        Highlighted = true;
        if (!tookDamage)
        {
            for (int i = 0; i < Limbs.Count; i++)
            {
                Limbs[i].GetComponent<SkinnedMeshRenderer>().material = HighlightMaterial;
            }
        }
    }
    public void TakeDamage(int dmg)
    {

        if (!deadAnim)
        {
            HealthPoints -= dmg;
            tmsh.text = "HP: " + HealthPoints;
            tookDamage = true;
            lastDamage = Time.time;
            for (int i = 0; i < Limbs.Count; i++)
                Limbs[i].GetComponent<SkinnedMeshRenderer>().material = DamageTaken;

            if (HealthPoints <= 0)
            {

                switch (EnemyType)
                {
                    case "Zombie":
                        Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseLowBounty(1);
                        break;
                    case "Witch":
                        Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseWitchBounty(1);
                        break;
                    case "Boss":
                        Camera.main.transform.Find("HUD").GetComponent<HUDInfo>().IncreaseBossBounty(1);
                        break;
                }
                SpawnBountyGraphic();
                GetComponent<GhoulMovement>().enabled = false;
                tmsh.enabled = false;
                //GetComponent<BoxCollider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
                
                anim.SetTrigger("EnemyDeath");
                deadAnim = true;
            }
            print(HealthPoints);
        }
    }

    void SpawnBountyGraphic()
    {
        Instantiate(AssociatedBountyCount, transform.position + (new Vector3(0,1f,0)), AssociatedBountyCount.transform.rotation);

    }

}