using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStars : MonoBehaviour
{
    //private GameObject render;
    public Sprite[] starSprites;
    private int wins = 0;
    // Start is called before the first frame update
    void Start()
    {
        //render = gameObject.GetComponent<Image>();
        this.transform.GetComponent<UnityEngine.UI.Image>().sprite = starSprites[wins];
    }

    // Update is called once per frame
    public void addStar()
    {
        wins++;
        this.transform.GetComponent<UnityEngine.UI.Image>().sprite = starSprites[wins];
    }
}
