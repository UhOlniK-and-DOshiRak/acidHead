using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    private Animator animator;
    public Text salary, cash, authority, clients, debt;
    public Text money, rent, food, debtE;
    public Toggle rentT, foodT, debtT;
    int currentCash, currentDebt, currentSalary, currentAuthority, foodCost, rentCost;
    public GameObject results, expenses, warningText;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadResult()
    {
        GameObject.Find("CanvasAnimation").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("CanvasAnimation").GetComponent<CanvasGroup>().blocksRaycasts = true;
        currentCash = FindObjectOfType<Player>().GetMoney();
        currentSalary = currentCash - FindObjectOfType<GameController>().targetMoney;
        currentDebt = PlayerPrefs.GetInt("Debt", 0);
        if (currentSalary < 0)
        {
            currentDebt += Mathf.Abs(currentSalary);
            currentSalary = 0;
        }
        cash.text = currentCash.ToString();
        currentAuthority = FindObjectOfType<Player>().GetAuthority();
        authority.text = currentAuthority.ToString() + "/100";
        clients.text = DataHolder.clientsCount.ToString();
        salary.text = currentSalary.ToString();
        debt.text = currentDebt.ToString();
        StartCoroutine(Result());
    }

    IEnumerator Result()
    {
        animator.SetTrigger("show");
        yield return new WaitForSeconds(1);
    }

    public void LoadExpenses()
    {
        results.SetActive(false);
        expenses.SetActive(true);
        foodCost = FindObjectOfType<GameController>().foodCost;
        rentCost = FindObjectOfType<GameController>().rentCost;
        money.text = currentSalary.ToString();
        food.text = foodCost.ToString();
        rent.text = rentCost.ToString();
        debtE.text = currentDebt.ToString();
    }

    public void SetMoney()
    {
        money.text = currentSalary.ToString();
        if (currentSalary < 0)
        {
            warningText.SetActive(true);
        }
        else
        {
            warningText.SetActive(false);
        }
    }

    public void buyFood()
    {
        if (foodT.isOn)
        {
            currentSalary -= foodCost;
        }
        else currentSalary += foodCost;
        SetMoney();
    }

    public void buyRent()
    {
        if (rentT.isOn)
        {
            currentSalary -= rentCost;
        }
        else currentSalary += rentCost;
        SetMoney();
    }

    public void payDebt()
    {
        if (debtT.isOn)
        {
            currentSalary -= currentDebt;
        }
        else currentSalary += currentDebt;
        SetMoney();
    }

    public void LoadNextDay()
    {
        int sceneIndex = FindObjectOfType<GameController>().sceneIndex;
        if (currentSalary >= 0)
        {
            PlayerPrefs.SetInt("Money", currentSalary);
            if (debtT.isOn)
            {
                PlayerPrefs.SetInt("Debt", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Debt", currentDebt);
            }
            PlayerPrefs.SetInt("Authority", currentAuthority);

            if (sceneIndex == 2)
            {
                FindObjectOfType<SceneTransitions>().LoadScene(0);
            }
            else
            {
                FindObjectOfType<SceneTransitions>().LoadScene(sceneIndex + 1);
            }     
            
        }
    }
}
