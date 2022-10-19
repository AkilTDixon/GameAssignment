using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbInfo : MonoBehaviour
{
    [SerializeField] public Material DamageTakenMaterial;
    [SerializeField] public Material HightlightMaterial;
    public List<Material> BaseMaterial;
    public List<GameObject> Limbs;
    void Start()
    {
        
        for (int i = 0; i < transform.childCount-1; i++)
        {
            if (transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>() != null)
            {
                Limbs.Add(transform.GetChild(i).gameObject);
                BaseMaterial.Add(transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().material);
            }
        }
    }

}
