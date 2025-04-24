using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float delay = 1f; // Delay before playing the audio
    [SerializeField] float fadeTime = 1f; // Delay before playing the audio
    [SerializeField] GameObject timeline;
    [SerializeField] Image blackout;
    [SerializeField] TMP_Text textUI;
    [SerializeField] GameObject btnUI;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        blackout.color = new Color(0f, 0f, 0f, 1f);

        textUI.gameObject.SetActive(false);
        btnUI.gameObject.SetActive(false);

        StartCoroutine("FadeOut");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Check if the space key is pressed
        {
            StartCoroutine("FadeIn");
            StartCoroutine("WaitToPlaySound");
        }
    }

    IEnumerator WaitToPlaySound()
    {
        yield return new WaitForSeconds(delay); // Wait for 1 second before playing the audio

        audioSource.Play();
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeTime)
        {
            yield return null;
            timer += Time.deltaTime;
            blackout.color = new Color(0f, 0f, 0f, timer / fadeTime);
        }
        textUI.text = FrameController.s_Instance.GetValue().ToString("F2");
        textUI.gameObject.SetActive(true);
        btnUI.SetActive(true);
        //while (timer < fadeTime)
        //{
        //    yield return null;
        //    timer += Time.deltaTime;
        //    blackout.color = new Color(0f, 0f, 0f, 1 - timer / fadeTime);
        //}
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeTime)
        {
            yield return null;
            timer += Time.deltaTime;
            blackout.color = new Color(0f, 0f, 0f, 1 - timer / fadeTime);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
