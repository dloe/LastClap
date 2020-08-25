using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRuleSheet : MonoBehaviour
{


    [SerializeField]
    private Color maxColor;

    [SerializeField]
    private Color minColor;

    [SerializeField]
    private Color midColor;

    public float suspicionValue;
    public GameObject fillColor;
    public Slider suspicionSlider;
    public Button nextButton;

    // Start is called before the first frame update
    void Start()
    {
        suspicionValue = 0;
        StartCoroutine(rotateColors());
        Debug.Log("im here");
    }

    // Update is called once per frame
    void Update()
    {
        suspicionSlider.value = suspicionValue;
        checkColorChange();
        
    }



    private void checkColorChange()
    {
        if (suspicionValue >= 26)
        {
            fillColor.GetComponent<Image>().color = maxColor;
        }

        if (suspicionValue <= 13)
        {

            fillColor.GetComponent<Image>().color = minColor;
        }

        if (suspicionValue < 26 && suspicionValue > 13)
        {

            fillColor.GetComponent<Image>().color = midColor;
        }
    }

    private IEnumerator rotateColors()
    {
        yield return new WaitForSeconds(0.9f);
        if (suspicionValue <= 40)
        {
            suspicionValue += 10;
        }
        if ( suspicionValue > 40)
        {
            suspicionValue = 0;
        }
        StartCoroutine(rotateColors());
        Debug.Log("im running");
    }

    private void stopRotateColors()
    {
        StopCoroutine(rotateColors());
    }


}
