using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rounds : MonoBehaviour
{
    private Timer time;
    public GameObject audioApplause;
    public Text roundText;
    public bool applause;
    public int MAX_ROUNDS = 1;
    public int round = 1;
    // Start is called before the first frame update
    void Start()
    {
        //when game starts, dont have players or AI clap yet
        time = GetComponent<Timer>();
        StartCoroutine(startround());
    }

    // Update is called once per frame
    public void NextRound()
    {
        if (time.time <= 0)
        {
            //start stuff between rounds
            if (round < MAX_ROUNDS && FindObjectOfType<DetermineWinner>().inPlay)
            {
                //set player cant clap
                FindObjectOfType<Clap>().betweenRounds = true;
                Debug.Log("Round ends... starting next round.");
                FindObjectOfType<SuspicionBar>().last_clap_time = 34f;
                round++;
                time.time = 30f;
                StartCoroutine(startround());
                FindObjectOfType<HubScript>().resetAI();
            }
            else
            {
                time.CancelInvoke("UpdateTimer");
            }
        }
    }

    IEnumerator startround()
    {
        applause = true;
        time.paused = true;
        roundText.text = "Round " + round;
        audioApplause.GetComponent<ApplauseAudio>().playApplause();
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Round Starting...");
        FindObjectOfType<Clap>().betweenRounds = false;
        time.paused = false;
        roundText.text = null;
        applause = false;

    }

}
