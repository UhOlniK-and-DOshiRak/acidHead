using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public Animator animator;
    public GameObject orderButton, refuseButton, continueButton, workSpace, askCash, returnCash;
    public static bool isMoney;

    private Queue<string> sentences;

    void Start()
    {
        isMoney = false;
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        returnCash.SetActive(false);
        isMoney = false;
        animator.SetBool("Show", true);
        nameText.text = dialog.name;
        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (DataHolder.dayStarted)
            {                
                EndDialog();
            }
            else
            {
                EndFinalDialog();
                FindObjectOfType<ResultPanel>().LoadResult();
            }
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void EndDialog()
    {
        orderButton.SetActive(true);
        refuseButton.SetActive(true);
        continueButton.SetActive(false);
        //animator.SetBool("Show", false);        
        //DataHolder.currentClientComplete = true;
    }

    public void EndFinalDialog()
    {
        animator.SetBool("Show", false);       
        
    }

    public void ShowWorkspace()
    {
        workSpace.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = new Vector3(0,0,0);
        orderButton.SetActive(false);
        askCash.SetActive(true);
    }

    public void Refuse()
    {
        animator.SetBool("Show", false);        
        DataHolder.currentClientComplete = true;
        orderButton.SetActive(false);
        refuseButton.SetActive(false);
        askCash.SetActive(false);
        continueButton.SetActive(true);
        workSpace.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        FindObjectOfType<GameController>().UpdateClientsCount();
        if (FindObjectOfType<OrderSlot>().EnoughCash() == 0 && !isMoney)
        {
            FindObjectOfType<Workspace>().InstantiateWarning(Warning.WarningType.NoReasonRefuse);
            FindObjectOfType<Player>().ReduceAuthority(5);
        }
        else if (FindObjectOfType<OrderSlot>().EnoughCash() > 0 && !isMoney)
        {
            FindObjectOfType<Player>().AddAuthority(5);
        }
        if (isMoney)
        {
            FindObjectOfType<Workspace>().InstantiateWarning(Warning.WarningType.GetMoneyRefuse);
            FindObjectOfType<Player>().AddMoney(GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetCashAmount());
            FindObjectOfType<Player>().ReduceAuthority(10);
        }
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }

    public void ReturnMoney()
    {
        isMoney = false;
        returnCash.SetActive(false);
        FindObjectOfType<OrderSlot>().DeleteCash();
    }

    public void Sale()
    {
        animator.SetBool("Show", false);
        DataHolder.currentClientComplete = true;
        orderButton.SetActive(false);
        refuseButton.SetActive(false);
        askCash.SetActive(false);
        continueButton.SetActive(true);
        workSpace.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        FindObjectOfType<GameController>().UpdateClientsCount();
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }

    public void AskCash()
    {
        if (!isMoney)
        {
            Cash[] money = GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().cash;
            foreach (Cash cash in money)
            {
                RectTransform rectTransform = Instantiate(cash, GameObject.Find("ItemWorldContainer").transform).GetComponent<RectTransform>();
                rectTransform.anchoredPosition = GameObject.Find("PosCash").GetComponent<RectTransform>().anchoredPosition;
            }
            isMoney = true;
            returnCash.SetActive(true);
            askCash.SetActive(false);
        }
    }
}
