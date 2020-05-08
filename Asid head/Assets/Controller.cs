using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    int sceneIndex;
    SceneTransitions sceneTransitions;

    void Start()
    {
        sceneIndex = PlayerPrefs.GetInt("Level", 2);
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
