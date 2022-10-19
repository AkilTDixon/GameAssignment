using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyIconScript : MonoBehaviour
{
    private float lastFade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastFade + 0.1f)
        {
            Color col = GetComponent<SpriteRenderer>().color;
            col.a -= 0.05f;
            if (col.a <= 0)
                Destroy(gameObject);
            GetComponent<SpriteRenderer>().color = col;
            transform.position += new Vector3(0, 0.1f, 0);
            
            lastFade = Time.time;
        }
    }

}
