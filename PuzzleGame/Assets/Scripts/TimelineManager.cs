using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    void Start()
    {
        Invoke("GoGame",12f);
    }

    void GoGame()
    {
        SceneManager.LoadScene("World");
    }
}
