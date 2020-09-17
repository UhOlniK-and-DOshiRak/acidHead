using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] phrases;
    private Queue<string> sentences;
    public Text dialogText;
    SceneTransitions sceneTransitions;
    public Sprite second, third;
    public Image image;

    void Start()
    {
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (string sentence in phrases)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        FindObjectOfType<Sound>().ButtonSound();
        Debug.Log(sentences.Count);
        if (sentences.Count == 7) image.sprite = second;
        if (sentences.Count == 1) image.sprite = third;
        if (sentences.Count == 0)
        {
            EndDialog();
        }
        else
        {
            string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0f);
        }
    }

    public void EndDialog()
    {
        sceneTransitions.LoadScene(2);
    }

    public void SkipDialog()
    {
        sceneTransitions.LoadScene(2);
    }


}
