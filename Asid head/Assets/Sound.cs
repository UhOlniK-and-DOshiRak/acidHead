using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound instance;

    private AudioSource source;
    public AudioClip coin, message, warning, openCash, tumbler;
    public AudioClip[] book;
    public AudioClip[] button;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void CoinSound()
    {
        source.clip = coin;
        source.Play();
    }

    public void MessageSound()
    {
        source.clip = message;
        source.Play();
    }

    public void WarningSound()
    {
        source.clip = warning;
        source.Play();
    }

    public void BookSound()
    {
        int randomNaumber = Random.Range(0, book.Length);
        source.clip = book[randomNaumber];
        source.Play();
    }

    public void ButtonSound()
    {
        int randomNaumber = Random.Range(0, button.Length);
        source.clip = button[randomNaumber];
        source.Play();
    }

    public void OpenCashSound()
    {
        source.clip = openCash;
        source.Play();
    }

    public void TumblerSound()
    {
        source.clip = tumbler;
        source.Play();
    }
}
