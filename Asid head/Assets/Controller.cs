using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    int sceneIndex;
    SceneTransitions sceneTransitions;

    void Start()
    {
        sceneIndex = PlayerPrefs.GetInt("Level", 2);
        if (sceneIndex == 2)
        {
            GameObject.Find("Continue").GetComponent<Button>().enabled = false;
            GameObject.Find("Continue").GetComponent<Animator>().SetTrigger("Disabled");
        }
        else
            GameObject.Find("Continue").GetComponent<Button>().enabled = true;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    public void NewGame()
    {
        FindObjectOfType<Sound>().ButtonSound();
        PlayerPrefs.DeleteAll();
        sceneTransitions.LoadScene(1);
    }

    public void Continue()
    {
        FindObjectOfType<Sound>().ButtonSound();
        sceneTransitions.LoadScene(sceneIndex);
    }

    public void Exit()
    {
        FindObjectOfType<Sound>().ButtonSound();
        Application.Quit();
    }
}
