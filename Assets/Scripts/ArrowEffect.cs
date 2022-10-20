using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowEffect : MonoBehaviour
{
    private float lastFade = 0;
    private List<GameObject> ArrowImages;
    private int head = 2;
    private float fadeAmount = -0.1f;
    // Start is called before the first frame update
    void Start()
    {
        ArrowImages = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            ArrowImages.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastFade + 0.02f)
        {
            lastFade = Time.time;
            if (head == -1)
            {
                fadeAmount = -fadeAmount;

                head = 2;
            }
            Color col = ArrowImages[head].GetComponent<RawImage>().color;
            col.a = col.a + (fadeAmount);
            ArrowImages[head].GetComponent<RawImage>().color = col;
            if (col.a <= 0 || col.a >= 1)
                head--;
        }
    }
}
