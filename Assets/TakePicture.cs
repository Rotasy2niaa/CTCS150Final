using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float delay = 1f; // Delay before playing the audio
    [SerializeField] GameObject timeline;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Check if the space key is pressed
            {
                timeline.SetActive(true); // Activate the timeline GameObject

                yield return new WaitForSeconds(delay); // Wait for 1 second before playing the audio
                
                audioSource.Play();
            }
            else
            {
                yield return null; // Wait for the next frame if space is not pressed
            }
        }
    }
}
