using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimages : MonoBehaviour
{
    
    [SerializeField] GameObject ObjectToDuplicate;
    [SerializeField] int MaxImages = 5;
    private float lastImage;
    private int imageCount;
    public GameObject[] images;
    private int head;

    void Start()
    {
        images = new GameObject[MaxImages];
        head = MaxImages - 1;
        imageCount = 0;
        lastImage = 0;    

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastImage + 0.025f)
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

    void AdjustList(List<GameObject> l)
    {
        for (int i = 0; i < l.Count - 1; i++)
        {
            l[i] = l[i + 1];
        }
    }
}
