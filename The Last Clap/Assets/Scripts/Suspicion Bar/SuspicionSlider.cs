using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionSlider : MonoBehaviour
{
    private Color calcColor;
    private float perc;
    public Slider suspicionSlider;
    public GameObject fillColor;
    public float suspicionValue;

    public GameObject bar;

    [SerializeField]
    private Color maxColor;

    [SerializeField]
    private Color minColor;

    [SerializeField]
    private Color midColor;


    // Update is called once per frame
    void Update()
    {
        suspicionValue = bar.GetComponent<SuspicionBar>().suspicionBar;

        //Sets slider value to equal suspicionValue var.
        suspicionSlider.value = suspicionValue;

        if (suspicionValue > 40)
        {
            suspicionValue = 40;
            //Debug.Log("i ran");
        }
        fillColor.GetComponent<Image>().color = maxColor;

        checkColorChange();
    }

    private void checkColorChange()
    {
        if (suspicionValue >= 26)
        {
            perc = (suspicionValue - 26) / 14;
            calcColor = new Color(1f, (0.92f + (perc*-0.92f)), (0.016f + (perc*-0.016f)), 1f);
            fillColor.GetComponent<Image>().color = calcColor;
        }

        if (suspicionValue <= 13)
        {
            perc = (suspicionValue) / 13;
            fillColor.GetComponent<Image>().color = minColor;
        }

        if (suspicionValue < 26 && suspicionValue > 13)
        {
            perc = (suspicionValue - 13) / 13;
            calcColor = new Color((0f + (perc*0.92f)), (1f + (perc*-0.084f)), (0f + (perc*0.016f)), 1f);
            fillColor.GetComponent<Image>().color = calcColor;
        }
    }
}
