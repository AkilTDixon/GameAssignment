using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFlash : MonoBehaviour
{

    private ParticleSystem flash;
    // Start is called before the first frame update
    void Start()
    {
        flash = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!flash.IsAlive())
            Destroy(gameObject);
    }
}
