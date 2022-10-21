using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LaunchScene(string name)
    {
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;

        SceneManager.LoadScene(name);
        
    }
}
