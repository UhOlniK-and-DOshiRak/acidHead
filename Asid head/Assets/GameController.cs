using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI clientsCountUI;
    public GameObject phone, timer, pauseMenu;
    public int targetMoney;
    public int foodCost, rentCost;
    public int sceneIndex;

    public bool checkNames;
    public bool checkAge;
    public bool checkSum;

    public int maxOrderSum;

    void Start()
    {
        

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Level", sceneIndex);        

        DataHolder.clientsCount = 0;
        DataHolder.dayStarted = false;
        DataHolder.daysHome = PlayerPrefs.GetInt("DaysHome", 3);
        DataHolder.daysFood = PlayerPrefs.GetInt("DaysFood", 3);
        DataHolder.daysDebt = PlayerPrefs.GetInt("DaysDebt", 2);
        DataHolder.nameCheckEnabled = checkNames;
        DataHolder.ageCheckEnabled = checkAge;
        DataHolder.sumCheckEnabled = checkSum;
        DataHolder.maxSum = maxOrderSum;
        Phone();
        phone.GetComponent<Animator>().SetTrigger("appear");
    }

    public void UpdateClientsCount()
    {
        DataHolder.clientsCount++;
        clientsCountUI.SetText(DataHolder.clientsCount.ToString());
    }

    public void StartDay()
    {
        DataHolder.dayStarted = true;
        timer.SetActive(true);
    }

    public void Phone()
    {

        phone.SetActive(true);
        FindObjectOfType<Sound>().MessageSound();
    }

    public void EndDay()
    {
        FindObjectOfType<DialogManager>().EndFinalDialog();
        DataHolder.dayStarted = false;
        FindObjectOfType<Workspace>().GetComponent<RectTransform>().localScale = Vector3.zero;
        FindObjectOfType<DialogManager>().askCash.SetActive(false);
        FindObjectOfType<DialogManager>().refuseButton.SetActive(false);
        FindObjectOfType<DialogManager>().returnCash.SetActive(false);
        FindObjectOfType<DialogManager>().orderButton.SetActive(false);
        FindObjectOfType<DialogManager>().continueButton.SetActive(true);
        FindObjectOfType<Player>().GetComponent<DialogTrigger>().TriggerDialog();
    }

    public void Pause()
    {
        FindObjectOfType<Sound>().ButtonSound();
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        FindObjectOfType<Sound>().ButtonSound();
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ToMainMenu()
    {
        FindObjectOfType<Sound>().ButtonSound();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    
}
