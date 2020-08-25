using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionBar : MonoBehaviour
{
    //every clap works towards maxPoints on suspicion bar
    //once a player surpasses maxPoints, the player that clap gets removed from match (They loose)
    //last player to stay standing wins

    public GameObject player;
    public GameObject time;
    //value of 40 to start
    public int suspicionMaxPoints = 40;
    public float claps = 0;
    public float suspicionBar = 0;

    public Text suspicionText;

    public bool barFilled;
    public float last_clap_time;
    private float timer;

    private bool playerWaited10Sec;

    private bool preventSuspicionWait;

    void Start()
    {        
        suspicionText.text = "Suspicion: " + suspicionBar + "/40";
        barFilled = false;
        preventSuspicionWait = false;
        last_clap_time = 0f;
    }
    
    void Update()
    {
        timer = FindObjectOfType<Timer>().time;
        suspicionText.text = "Suspicion: " + Mathf.Round(suspicionBar) +"/40";

        playerWaited10Sec = FindObjectOfType<Clap>().playerWaited10;
        
        //will need to check if all AI in array have clapping set to true, use for loop
        //claps have a times 2 multiplier for each clap

        //if AI claps
        for(int x = 0; x <= FindObjectOfType<HubScript>().enemies.Length -1; x++)
        {
            //if an AI is clapping, do same thing for if player claps
            if(FindObjectOfType<HubScript>().enemies[x].GetComponent<AI>().clapping)
            {
                //clap
                initiatClap();
            }
        }

        //if player claps
        if (player.GetComponent<Clap>().clapping)
        {
            //initiatClap
            initiatClap();
        }

        //if 3 seconds have passed since the last clap the suspicion Bar will start to go down
        if ((last_clap_time - 3f >= time.GetComponent<Timer>().time && suspicionBar > 0))
        {
            if (timer >= 0 || suspicionBar < suspicionMaxPoints)
            {
                suspicionBar -= Time.deltaTime;

                //if it goes over and the game continues, goes down a little faster
                if (suspicionBar > suspicionMaxPoints)
                {
                    suspicionBar -= Time.deltaTime;
                }
            }
        }

        //if suspicion is pushed over by AI, immediately start decementing suspicion so the game wont waste time (Goes until next clap)
        //wont decriment if game has eneded
        if (preventSuspicionWait && FindObjectOfType<DetermineWinner>().inPlay)
        {
            //decrements twice as fast
            suspicionBar -= Time.deltaTime;
            suspicionBar -= Time.deltaTime;
        }

        //if suspicionBar == suspicionMaxPoints run function to remove player from play
        if(suspicionBar >= suspicionMaxPoints)
        {
            barFilled = true;
            preventSuspicionWait = true;
            //if ai pushed it over, return suspicion bar to 40
            suspicionBar = 40;
            Debug.Log("Suspicion Bar = " + suspicionBar);

        }
        else
        {
            barFilled = false;
            FindObjectOfType<HubScript>().UpdateAI();
        }
    }

    //clapping helper function
    void initiatClap()
    {
        preventSuspicionWait = false;
        //few seconds of suspicion free clapping at beginning of game
        //no suspicion added during applause
        //no more suspicion if game has ended, meaning time has run out
        if ((timer >= 0 || suspicionBar < suspicionMaxPoints) && !FindObjectOfType<Rounds>().applause)
        {
            claps++;
            //Use this after timer has been implemented
            suspicionBar = suspicionBar + Mathf.Round((30 - time.GetComponent<Timer>().time) / 3f);
            last_clap_time = time.GetComponent<Timer>().time;

            //if player waited 10 seconds before last clap, then add 5 to suspicion
            if (FindObjectOfType<Clap>().playerWaited10 && player.GetComponent<Clap>().clapping)
            {
                suspicionBar += 2;
                //on top of that if there is less than 5 seconds left, add more suspicion
                if (timer <= 5)
                {
                    suspicionBar += 4;
                }
            }
        }
    }
}
