using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimages : MonoBehaviour
{
    
    [SerializeField] GameObject ObjectToDuplicate;
    [SerializeField] int MaxImages = 5;
    [SerializeField] float Cooldown = 0.25f;
    public bool VariantMode = false;
    private float lastImage;
    private int imageCount;
    public GameObject[] images;
    private int head;
    public float timeActivated;

    void Start()
    {
        timeActivated = Time.time;
        images = new GameObject[MaxImages];
        head = MaxImages - 1;
        imageCount = 0;
        lastImage = 0;    

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeActivated + Cooldown && !VariantMode)
        {

            DeleteList(images);
            
            head = MaxImages - 1;
            imageCount = 0;
            lastImage = 0;
            enabled = false;
        }
        if (Time.time > lastImage + 0.025f && enabled) 
        {
            if (head == -1)
                head = MaxImages - 1;

            lastImage = Time.time;

            GameObject obj = Instantiate(ObjectToDuplicate, transform.position, transform.rotation);
            
            if (images[head] != null)
                Destroy(images[head].gameObject);
            
            images[head] = obj;
            head--;
            
            
        }
    }

    void DeleteList(GameObject[] l)
    {
        for (int i = 0; i < l.Length; i++)
        {
            if (l[i] != null)
                Destroy(l[i].gameObject);
            
        }
    }
}
