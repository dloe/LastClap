using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AI : MonoBehaviour
{ 
    //ARRAY will look at clapping, last, claps
    public bool clapping;
    public bool last;
    public int claps;
    public float AiPerfection = 2; //margin of error for what the ai believes the score to be. 0, is perfect
    int randNumMax;
    int randNumMin;
    public float hypotheticalSusp;
    int winningClap = 4; //the lower the number the more likely they are to clap when winning
    public Animator AI_anim;
    private bool newSec;
    public GameObject player;
    public GameObject audioClap;
    public string AIname = "Default";
    public Text AIWinnerText;

    //if an AI goes over suspicion, they are removed from play
    public bool removedFromPlay;

    public int round_wins = 0;

    public bool overSupsicion;
    //int randInt;
    public int randWaitClap;

    public float timer;

    public bool cantCountDown;

    public GameObject aiStar;

    void Awake()
    {
        //AI will also have clap animator like player
        AI_anim = GetComponent<Animator>();
        removedFromPlay = false;
        AIWinnerText.transform.gameObject.SetActive(false);
    }

    void Start()
    {
        overSupsicion = FindObjectOfType<SuspicionBar>().barFilled;
        clapping = false;
        last = false;
        cantCountDown = true;
        claps = 0;
        randNumMax = 4;
        randNumMin = 1;
        timer = FindObjectOfType<Timer>().time;
        //start random int at random Number, range Will be tweeked as playtests
        randWaitClap = Random.Range(randNumMin, randNumMax);
        //will clap at rand times
        RandomClapInterval();
    }

    //normal update or Fixed?
    void Update()
    {
        overSupsicion = FindObjectOfType<SuspicionBar>().barFilled;
        clapping = false;
        //update timer
        timer = FindObjectOfType<Timer>().time;
        
        if (newSec)
        {
            hypotheticalSusp = FindObjectOfType<SuspicionBar>().suspicionBar + (30f - timer)/3f + Random.Range(-AiPerfection, AiPerfection);
            if (randWaitClap >= timer)
            {
                cantCountDown = false;
            }
            //if suspicion bar is less than 35 dont clap
            else if (hypotheticalSusp >= 40)
            {
                //dont clap for last few suspicion points
                cantCountDown = false;
            }
            else
            {
                cantCountDown = true;
            }
            newSec = false;
        }

        //if countdown hits zero, and it can count and we have not surpassed suspicion, clap
        if (randWaitClap == 0 && cantCountDown == true && overSupsicion == false)//overSuspicion == false;
        {
            //Debug.Log("Clap");                
            //reset countdown timer
            randWaitClap = Random.Range(randNumMin, randNumMax);
            if(randWaitClap >= timer)
            {
                //wont run if next number is greater than whats left on timer
                cantCountDown = false;
            }
            claps++;
            clapping = true;

            //set all other AI to have last = false, including player
            //sets everything to false, then turns this AI to true
            FindObjectOfType<HubScript>().setAllFalse();
            last = true;
            FindObjectOfType<HubScript>().lastclapper.Push(AIname);
            AI_anim.Play("AIclap");
            audioClap.GetComponent<ClapAudio>().playClap();
            player.GetComponent<Clap>().last = false;

            //wont clap unfairly if there is less than 2 seconds left on timer
            if (timer < 2)
            {
                cantCountDown = false;
            }
        }

        //AI should clap based on how high suspicion bar is instead of how much time
        if(timer <= 30 && timer > 15)
        {
            randNumMin = 2;
            randNumMax = 4;
        }
        if(timer <= 15 && timer >= 5)
        {
            randNumMin = 3;
            randNumMax = 6;
        }
        if(timer <= 5 && timer > 0)
        {
            randNumMin = 4;
            randNumMax = 7;
        }

        //if this AI is removed from play, it can no longer clap and not participate any longer
        if(removedFromPlay)
        {
            //cant clap
            cantCountDown = false;
            //maybe change the sprite or something?
        }

    }
    //function to check when was the last time someone clapped (AI or player) UNUSED
    bool CheckLastClap()
    {
        bool TimeLastClap = false;
        return TimeLastClap;
    }

    void RandomClapInterval()
    {
        //sets back to false once AI claps and function is now called again
        if (randWaitClap < timer && randWaitClap > 0)
        {
            //counts down according to randIntClap, when reaches 0, it will clap then restart funciton
                //while the time left to wait is greater than zero, wait a second then take one away
                    //counting function
                    InvokeRepeating("Count1Second", 0, 1.0f);
        }
    }

    //count 1 second at a time
    void Count1Second()
    {
        newSec = true;
        //will still be checking numbers once suspicion has been reached, game will continue when someone goes over suspicion. But AI cant clap until it goes back down again
        if (randWaitClap > 0 && cantCountDown == true && (!last || Random.Range(0, winningClap) == 1)) //&& overSuspicion == false;
        {
           // wait time looses a sec if its greater than zero, not over suspicion and it can still count down
            randWaitClap--;
        }
        if(randWaitClap == 0)
        {
            //Debug.Log("Waittime = 0");
        }
    }

    public void resetRand()
    {
        randWaitClap = Random.Range(randNumMin, randNumMax);
    }
}