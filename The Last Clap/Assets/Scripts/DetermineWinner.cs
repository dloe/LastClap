using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DetermineWinner : MonoBehaviour
{
    //may use arrays for multiple AIs
    public Text playerWin;
    //public Text Aiwin;

    public GameObject playerStar;
    //will need aiStar for each AI
    public GameObject aiStar;
    //bool to determine if game is still ongoing
    public bool inPlay = true;
    public GameObject[] enemies;
    public GameObject player;
    private GameObject lastClapper;
    public GameObject endScreen;
    bool cont = true;

    //win/lose text
    public Text gameResultsText;

    void Start()
    {
        endScreen.transform.gameObject.SetActive(false);
        playerWin.transform.gameObject.SetActive(false);
        //Aiwin.transform.gameObject.SetActive(false);
        //set array to hub array
        enemies = FindObjectOfType<HubScript>().enemies;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //in order to continue checking if someone went over suspcion, we need to wait until the suspicion bar is under 40
        if (FindObjectOfType<SuspicionBar>().suspicionBar <= 39)
            cont = true;

        //if timer = 0, check each AI and player last
        if (FindObjectOfType<Timer>().time <= 0 && inPlay)
        {
            lastClapper = FindObjectOfType<HubScript>().findLastClapper();
            //if last clap obj is player, they win
            if (lastClapper == player)
            {
                //player round win count increased
                FindObjectOfType<Clap>().round_wins++;
                playerStar.GetComponent<WinStars>().addStar();
                //if player has won two rounds they win or if there are no AI left
                if (FindObjectOfType<Clap>().round_wins >= 2 || FindObjectOfType<HubScript>().areThereStillAI())
                {
                    //playerWin.transform.gameObject.SetActive(true);
                    inPlay = false;
                    Debug.Log("PLAYER WINS THE GAME!");
                    endScreen.transform.GetChild(3).gameObject.SetActive(true);
                    endScreen.transform.gameObject.SetActive(true);
                    //player cant clap anymore
                    FindObjectOfType<Clap>().betweenRounds = true;
                    //two instances where player wins
                    //HERE
                    gameResultsText.text = "You win!";
                }
            }
            //if last clap obj is NOT player (AI)
            else
            {
                //relocate last clapper
                lastClapper.GetComponent<AI>().round_wins++;
                //now has the AI who clapped last as a adjustable gameobject

                //instead of accessing this gameObject, access AI scripts GAMEOBJECT
                lastClapper.GetComponent<AI>().aiStar.GetComponent<WinStars>().addStar();
                

                Debug.Log(lastClapper.GetComponent<AI>().round_wins);
                //if AI has won two rounds they win
                if (lastClapper.GetComponent<AI>().round_wins == 2)//FindObjectOfType<AI>().round_wins >= 2)
                {
                    //each AI has text that it accesses
                    lastClapper.GetComponent<AI>().AIWinnerText.transform.gameObject.SetActive(true);
                    inPlay = false;
                    endScreen.transform.gameObject.SetActive(true);
                    //AI wins if they win two rounds (get two stars)
                    Debug.Log("An AI has won two games. Game will now end...");
                    Debug.Log("Player loses... GG");
                    gameResultsText.text = "You lose...";
                }
            }
            FindObjectOfType<Rounds>().NextRound();
        }

        //if suspicion goes over
        if (FindObjectOfType<SuspicionBar>().suspicionBar >= 40 && inPlay && cont)
        {
            //when someone pushes themselves over the edge, that person who claps can no longer play, if player goes over, then the game ends
            if (FindObjectOfType<Clap>().last)
            {
                //if player clapped last, they loose (GAME ENDS)
                FindObjectOfType<Clap>().playerLost = true;
                //Aiwin.transform.gameObject.SetActive(true);
                inPlay = false;
                endScreen.transform.gameObject.SetActive(true);
                //PLAYER LOOSES
                Debug.Log("Player lost. gg.");
                gameResultsText.text = "You lose...";
            }
            else
            {
                //if AI claps last, they lose and sets this last clapping AI to stop
                lastClapper = FindObjectOfType<HubScript>().findLastClapper();
                lastClapper.GetComponent<AI>().removedFromPlay = true;
                Debug.Log(lastClapper.name + " Removed from play");
                //the hand disappears when removed
                FindObjectOfType<HubScript>().setAllFalse();
                //image is disabled
                lastClapper.GetComponent<Image>().enabled = false;
                
                cont = false;

                //checks if it can still have AI to play
                if (FindObjectOfType<HubScript>().areThereStillAI())
                {
                    Debug.Log("Game will end now. Player wins!");
                    //Second player star can pop up if they win both rounds OR here where all the AI are gone so player wins
                    playerStar.GetComponent<WinStars>().addStar();
                    //ADD THE STUFF HERE TO OFFICIALLY END  THE GAME
                    FindObjectOfType<Clap>().betweenRounds = true;
                    playerWin.transform.gameObject.SetActive(true);
                    inPlay = false;
                    FindObjectOfType<Timer>().paused = true;
                    //PUT UP END OF GAME SCREEN HERE
                    endScreen.transform.GetChild(3).gameObject.SetActive(true);
                    endScreen.transform.gameObject.SetActive(true);
                    //activate between rounds until next round
                    FindObjectOfType<Clap>().betweenRounds = true;
                    //OR HERE
                    gameResultsText.text = "You win!";
                }
            }
        }
    }
}