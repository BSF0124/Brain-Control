using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Select : MonoBehaviour
{
    // Start is called before the first frame update

    private string scene_Name = "World";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Quit_Game()
    {
        Application.Quit();
    }

    public void Go_Setting()
    {
        Debug.Log("옵션");
    }

    public void Go_Scene()
    {
        SceneManager.LoadScene(scene_Name);
    }
}
