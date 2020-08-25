using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{

    public string SwitchLevel;


   

    // Start is called before the first frame update
    public void PlayGame ()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void menuSwitch ()
    {
        SceneManager.LoadScene(SwitchLevel);
    }
    
}
