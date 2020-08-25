using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float time = 30;
    public bool paused = false;

    void Start()
    {
        StartCoundownTimer();
    }

    private void FixedUpdate()
    {
        endGame();
    }

    void StartCoundownTimer()
    {
        if (timerText != null)
        {
            
            if(time >= 0)
            {
                time = 30;
                timerText.text = "Time Left: 30";
                InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
            }
        }
    }

    void UpdateTimer()
    {
        //timer stops when player loses
        //wont count down if suspicion bar is filled
        if (timerText != null && !paused && !FindObjectOfType<Clap>().playerLost)
        {
            time -= Time.deltaTime;
            string seconds = (time % 60).ToString("00");
            timerText.text = "Time Left: " + seconds;
        }
    }

    void endGame()
    {
        if (time <= 0)
        {
            //time = 30;
            //round ends
            //timer will be set to 30 when we need to run it again
            //SceneManager.LoadScene("NEXT ROUND OR MENU");
            //CancelInvoke("UpdateTimer");
        }
    }
}
