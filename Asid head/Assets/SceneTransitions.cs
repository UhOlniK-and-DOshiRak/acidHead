using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator transitionAnim;    

    void Start()
    {
        transitionAnim = GetComponent<Animator>();
    }

    public void LoadScene(int sceneNumber)
    {
        StartCoroutine(Transition(sceneNumber));
    }

    public void Quit()
    {
        StartCoroutine(QuitApp());
    }

    IEnumerator Transition(int sceneNumber)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneNumber);
    }

    IEnumerator QuitApp()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
