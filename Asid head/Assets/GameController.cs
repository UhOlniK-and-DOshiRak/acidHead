using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI clientsCountUI;
    public GameObject phone, timer;
    public int targetMoney;
    public int foodCost, rentCost;
    public int sceneIndex;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Level", sceneIndex);        

        DataHolder.clientsCount = 0;
        DataHolder.dayStarted = false;
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
}
