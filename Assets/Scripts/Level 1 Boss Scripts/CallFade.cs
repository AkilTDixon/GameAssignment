using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFade : MonoBehaviour
{
    void StartFade()
    {
        GetComponent<ImageFade>().enabled = true;
    }

}
