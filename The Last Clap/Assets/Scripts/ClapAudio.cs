using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clap;
    private AudioClip clapClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playClap()
    {
            int index = Random.Range(0, clap.Length);
            clapClip = clap[index];
            audioSource.clip = clapClip;
            audioSource.Play();
    }
}
