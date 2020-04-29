using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    int sceneIndex;
    SceneTransitions sceneTransitions;

    void Start()
    {
        sceneIndex = PlayerPrefs.GetInt("Level", 1);
        sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        sceneTransitions.LoadScene(1);
    }

    public void Continue()
    {
        sceneTransitions.LoadScene(sceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
