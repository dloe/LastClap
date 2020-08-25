using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clap : MonoBehaviour
{
    public string playerName = "Player";
    public int claps = 0;
    public int round_wins = 0;
    public GameObject audioClap;
    
    public bool clapping = false;
    public bool last = false;
    private Animator anim;

    private bool cooldown;
    public bool playerWaited10;

    public bool playerLost;

    public bool betweenRounds = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerLost = false;
        cooldown = false;
        playerWaited10 = false;

        //starts the 10 second thing
        StartCoroutine(count10sec());
    }

    // Update is called once per frame
    void Update()
    {
        clapping = false;
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            if(!betweenRounds)
            { 
                claps++;
                if (cooldown == false)
                {
                    //if cool down not active, then add to suspicion, still can clap though
                    clapping = true;

                    //sets everything to false, then sets itself true
                    FindObjectOfType<HubScript>().setAllFalse();
                    last = true;
                    FindObjectOfType<HubScript>().lastclapper.Push(playerName);
                    //audio has cooldown of 1 second, so no spam audio
                    anim.Play("clap");
                    audioClap.GetComponent<ClapAudio>().playClap();
                    anim.SetBool("Clapping", true);

                    //starts coroutine
                    StartCoroutine(count1sec());
                    playerWaited10 = false;
                }
            }

            //if player waits more than 10 seconds to clap, if they clap in the last 3 seconds of the round, the suspicion will be exponential
            if(last == true)
            {
                //start check if 10 seconds have gone by
                StartCoroutine(count10sec());
            }
        }
    }

    IEnumerator count1sec()
    {
        cooldown = true;
        yield return new WaitForSeconds(1.0f);
        cooldown = false;
    }

    IEnumerator count10sec()
    {
        int prewaitClaps = claps;
        yield return new WaitForSeconds(10.0f);

        //check if player if player still have not clapped
        if(prewaitClaps == claps)
        {
            Debug.Log("Player has waited longer than 10 seconds to clap: initializing counter measures...");
            playerWaited10 = true;

            
        }
    }
}
