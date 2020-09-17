using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Text dialogText;
    public Animator animator;
    public GameObject orderButton, refuseButton, continueButton, workSpace, askCash, returnCash;
    public static bool isMoney;
    public GameObject cashTab;

    private Queue<string> sentences;

    void Start()
    {
        isMoney = false;
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        cashTab.SetActive(false);
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
        FindObjectOfType<Sound>().ButtonSound();
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
            yield return new WaitForSeconds(0f);
            //yield return new WaitForSeconds(0.005f);
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
        cashTab.SetActive(true);
        FindObjectOfType<Sound>().ButtonSound();
        workSpace.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = new Vector3(0,0,0);
        orderButton.SetActive(false);
        askCash.SetActive(true);
    }

    public void Refuse()
    {
        FindObjectOfType<Sound>().ButtonSound();
        Workspace workspace = FindObjectOfType<Workspace>();
        animator.SetBool("Show", false);        
        DataHolder.currentClientComplete = true;
        orderButton.SetActive(false);
        refuseButton.SetActive(false);
        askCash.SetActive(false);
        continueButton.SetActive(true);
        workSpace.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        FindObjectOfType<GameController>().UpdateClientsCount();
        FindObjectOfType<OrderCheck>().CheckOnRefuse();
        FindObjectOfType<OrderSlot>().SetLabel();
        FindObjectOfType<CashCount>().Clear();
        //if (DataHolder.nameCheckEnabled)
        //{
        //    if (!isMoney && !workspace.checkName(workspace.getOrder(), workspace.GetPlayer()))
        //    {
        //        Debug.Log("Correct order");
        //        workspace.GetPlayer().AddAuthority(5);
        //        return;
        //    }
        //}
        //if (FindObjectOfType<OrderSlot>().EnoughCash() == 0 && !isMoney && workspace.checkName(workspace.getOrder(), workspace.GetPlayer()))
        //{
        //    FindObjectOfType<Workspace>().InstantiateWarning(Warning.WarningType.NoReasonRefuse);
        //    FindObjectOfType<Player>().ReduceAuthority(5);
        //}
        //else if (FindObjectOfType<OrderSlot>().EnoughCash() > 0 && !isMoney)
        //{
        //    FindObjectOfType<Player>().AddAuthority(5);
        //}
        //if (isMoney)
        //{
        //    FindObjectOfType<Workspace>().InstantiateWarning(Warning.WarningType.GetMoneyRefuse);
        //    FindObjectOfType<Player>().AddMoney(GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetCashAmount());
        //    FindObjectOfType<Player>().ReduceAuthority(10);
        //}
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }

    public void ReturnMoney()
    {
        FindObjectOfType<Sound>().ButtonSound();
        isMoney = false;
        returnCash.SetActive(false);
        FindObjectOfType<OrderSlot>().DeleteCash();
        FindObjectOfType<CashCount>().Clear();
    }

    public void Sale()
    {
        cashTab.SetActive(false);
        animator.SetBool("Show", false);
        DataHolder.currentClientComplete = true;
        orderButton.SetActive(false);
        refuseButton.SetActive(false);
        askCash.SetActive(false);
        continueButton.SetActive(true);
        workSpace.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        FindObjectOfType<GameController>().UpdateClientsCount();
        FindObjectOfType<OrderSlot>().SetLabel();
        FindObjectOfType<CashCount>().Clear();
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
    }

    public void AskCash()
    {
        if (!isMoney)
        {
            FindObjectOfType<Sound>().CoinSound();
            Cash[] money = GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().cash;
            foreach (Cash cash in money)
            {
                RectTransform rectTransform = Instantiate(cash, GameObject.Find("ItemWorldContainerCash").transform).GetComponent<RectTransform>();
                rectTransform.anchoredPosition = GameObject.Find("PosCash").GetComponent<RectTransform>().anchoredPosition;
            }
            isMoney = true;
            returnCash.SetActive(true);
            askCash.SetActive(false);
        }
    }
}
