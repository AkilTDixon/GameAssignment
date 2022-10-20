using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFade : MonoBehaviour
{

    private Enemy EnemyHolder;
    private float lastFade;
    // Start is called before the first frame update
    void Start()
    {
        lastFade = 0;
        EnemyHolder = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastFade + 0.01f)
        {
            lastFade = Time.time;
            EnemyHolder.FadeOut();
        }
    }
}
