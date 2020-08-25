using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubScript : MonoBehaviour
{
    //enemies
    public int enemyCount;
    public GameObject player;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject[] enemies;

    //adjust suspicionHere (will add in future)
    public int suspicionMax;

    //stack that keeps track of who clapped last
    public Stack<string> lastclapper = new Stack<string>();

    bool playedSound;
    void Start()
    {
        playedSound = false;
        
        //sets array to amount of AI we punch in
        enemies = new GameObject[enemyCount];

        //has to manually set each element to the array to a gameobject
        enemies[0] = enemy1;
        if(enemy2 != null)
            enemies[1] = enemy2;
        if (enemy3 != null)
            enemies[2] = enemy3;
        if (enemy4 != null)
            enemies[3] = enemy4;

    }
    //instead of checking the single AI in suspicionBar and other scripts, we will instead run through a for loop to check each AI instead if their isClapped (AKA last) bool was active and perform acordingly
    //will also check to see who clapped last by running through array

        //runs through each AI and sets them all to faslse (including player), then set the specific value to true in script of that gameobject in separate script
    public void setAllFalse()
    {
        //if they clapped last
        for(int x = 0; x <= enemies.Length -1; x++)
        {
            enemies[x].GetComponent<AI>().last = false;
        }
        //sets player to false as well
        player.GetComponent<Clap>().last = false;
    }

    //return gameobject of last person to have clapped
    public GameObject findLastClapper()
    {
        GameObject lastClapper = null;
        string AIname;
        while (lastclapper.Count != 0) {
            AIname = lastclapper.Pop();
            for (int x = 0; x <= enemies.Length - 1; x++)
            {
                if (AIname == enemies[x].GetComponent<AI>().AIname && !enemies[x].GetComponent<AI>().removedFromPlay)
                {
                    Debug.Log(AIname);
                    lastClapper = enemies[x];
                    return lastClapper;
                }
                else if(AIname == player.GetComponent<Clap>().playerName)
                {
                    lastClapper = player;
                    return lastClapper;
                }
            }
        }
        Debug.Log("could not find last clapper");
        return lastClapper;
    }
    //still AI left?
    public bool areThereStillAI()
    {
        bool result = true;
        for(int x = 0; x <= enemies.Length - 1; x++)
        {
            if(enemies[x].GetComponent<AI>().removedFromPlay == false)
            {
                result = false;
                Debug.Log("Game will continue");
                break;
            }
        }
        return result;
    }
    //set all Ai to default states when restarted
    public void resetAI()
    {
        for(int x = 0; x <= enemies.Length - 1; x++)
        {
            enemies[x].GetComponent<AI>().resetRand();
            enemies[x].GetComponent<AI>().cantCountDown = true;
            enemies[x].GetComponent<AI>().clapping = false;
            enemies[x].GetComponent<AI>().last = false;
            enemies[x].GetComponent<AI>().timer = FindObjectOfType<Timer>().time;
            enemies[x].GetComponent<AI>().resetRand();
        }
    }

    //Update Ai to test suspicion
    public void UpdateAI()
    {
        for (int x = 0; x <= enemies.Length - 1; x++)
        {
            enemies[x].GetComponent<AI>().overSupsicion = false;
        }
    }

    //if a certain amount of time passes without any clapping, play a random background sound (IE someone coughing or sneezing, etc)
    //test code, not currently used
    public void playBackgroundSounds()
    {

        if(FindObjectOfType<Clap>().playerWaited10 && playedSound == false)
        {
            //play sound here for array of sounds
            int index = Random.Range(0, 2); //replace 2 with array length 

            playedSound = true;

        }
        if(FindObjectOfType<Clap>().playerWaited10 == false)
        {
            playedSound = false;
        }

    }
}
