using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplauseAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] applause;
    private AudioClip applauseClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    public void playApplause()
    {
        int index = Random.Range(0, applause.Length);
        applauseClip = applause[index];
        audioSource.clip = applauseClip;
        audioSource.Play();
    }
}
