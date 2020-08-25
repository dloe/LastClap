using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Funeral_FinalBoss : MonoBehaviour
{

    public GameObject finalAi;
    public GameObject finalAiStars;
    public GameObject finalAiText;
    public Animator AI_anim;
    public GameObject background1;
    public Sprite coffin_open;

    public AudioSource coffinSound;
    // Start is called before the first frame update
    void Start()
    {
        finalAi.GetComponent<AI>().removedFromPlay = true;
        finalAi.GetComponent<Image>().enabled = false;
        finalAiStars.GetComponent<Image>().enabled = false;
        finalAiText.transform.gameObject.SetActive(false);
        //FindObjectOfType<HubScript>().enemies[3] = null;
        //coffinSound = GetComponent<AudioSource>();
        //finalAi.GetComponent<AI>().removedFromPlay = true;
    }

    bool avoidRepeat = false;
    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<Rounds>().round == 2 && avoidRepeat == false)
        {
            avoidRepeat = true;
            addAI();
        }
    }

    //when round 2 starts it calls this function that actives AI, its image and all its UI elements
    void addAI()
    {
        //sets the AI into the match so it wont be detected before round 2
        //FindObjectOfType<HubScript>().enemies[3] = finalAi;


        finalAi.GetComponent<AI>().removedFromPlay = false;
        finalAi.GetComponent<Image>().enabled = true;
        finalAiStars.GetComponent<Image>().enabled = true;
        //finalAiText.transform.gameObject.SetActive(true);
        background1.GetComponent<Image>().sprite = coffin_open;
        coffinSound.Play();

        //turn on the components in awake for this specific AI
        //finalAi.GetComponent<AI>().AI_anim = GetComponent<Animator>();
        finalAi.GetComponent<AI>().removedFromPlay = false;
        //AIWinnerText.transform.gameObject.SetActive(false);
    }
}
