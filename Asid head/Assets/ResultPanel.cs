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
    public Text daysHome, daysFood, daysDebt;
    public GameObject final;

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

        if (DataHolder.daysHome > 1)
            daysHome.text = DataHolder.daysHome.ToString() + " дня";
        else
            daysHome.text = DataHolder.daysHome.ToString() + " день";

        if (DataHolder.daysFood > 1)
            daysFood.text = DataHolder.daysFood.ToString() + " дня";
        else
            daysFood.text = DataHolder.daysFood.ToString() + " день";

        if (DataHolder.daysDebt > 1)
            daysDebt.text = DataHolder.daysDebt.ToString() + " дня";
        else
            daysDebt.text = DataHolder.daysDebt.ToString() + " день";


        StartCoroutine(Result());
    }

    IEnumerator Result()
    {
        animator.SetTrigger("show");
        yield return new WaitForSeconds(1);
    }

    public void LoadExpenses()
    {
        FindObjectOfType<Sound>().ButtonSound();
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
        FindObjectOfType<Sound>().ButtonSound();
        int sceneIndex = FindObjectOfType<GameController>().sceneIndex;
        if (currentSalary >= 0)
        {
            PlayerPrefs.SetInt("Money", currentSalary);
            if (debtT.isOn)
            {
                PlayerPrefs.SetInt("Debt", 0);
                PlayerPrefs.SetInt("DaysDebt", 2);
            }
            else
            {
                if (currentDebt == 0)
                {
                    PlayerPrefs.SetInt("Debt", currentDebt);
                    PlayerPrefs.SetInt("DaysDebt", DataHolder.daysDebt);
                }
                else if (DataHolder.daysDebt > 1)
                {
                    PlayerPrefs.SetInt("Debt", currentDebt);
                    PlayerPrefs.SetInt("DaysDebt", DataHolder.daysDebt - 1);
                }
                else
                {
                    Debug.Log("Game Over! Debt");
                    Final f = Instantiate(final, GameObject.Find("FinalsContainer").transform).GetComponent<Final>();
                    f.showFinal(Final.FinalType.debt);
                    return;
                }
            }
            PlayerPrefs.SetInt("Authority", currentAuthority);

            if (rentT.isOn)
            {
                if (DataHolder.daysHome < 3)
                {
                    PlayerPrefs.SetInt("DaysHome", DataHolder.daysHome + 1);
                }
                else
                {
                    PlayerPrefs.SetInt("DaysHome", DataHolder.daysHome);
                }
            }
            else
            {
                if (DataHolder.daysHome > 1)
                {
                    PlayerPrefs.SetInt("DaysHome", DataHolder.daysHome - 1);
                }
                else
                {
                    Debug.Log("Game Over! Rent");
                    Final f = Instantiate(final, GameObject.Find("FinalsContainer").transform).GetComponent<Final>();
                    if (currentAuthority >= 70)
                    {
                        f.showFinal(Final.FinalType.homeHigh);
                    }
                    else
                    {
                        f.showFinal(Final.FinalType.homeLow);
                    }
                    return;
                }
            }

            if (foodT.isOn)
            {
                if (DataHolder.daysFood < 3)
                {
                    PlayerPrefs.SetInt("DaysFood", DataHolder.daysFood + 1);
                }
                else
                {
                    PlayerPrefs.SetInt("DaysFood", DataHolder.daysFood);
                }
            }
            else
            {
                if (DataHolder.daysFood > 1)
                {
                    PlayerPrefs.SetInt("DaysFood", DataHolder.daysFood - 1);
                }
                else
                {
                    Debug.Log("Game Over! Food");
                    Final f = Instantiate(final, GameObject.Find("FinalsContainer").transform).GetComponent<Final>();
                    f.showFinal(Final.FinalType.food);
                    return;
                }
            }


            if (sceneIndex == 5)
            {
                if (currentAuthority >= 90)
                {
                    Final f = Instantiate(final, GameObject.Find("FinalsContainer").transform).GetComponent<Final>();
                    f.showFinal(Final.FinalType.high);
                }
                else
                {
                    Final f = Instantiate(final, GameObject.Find("FinalsContainer").transform).GetComponent<Final>();
                    f.showFinal(Final.FinalType.low);
                }
                return;
            }
            else
            {
                FindObjectOfType<SceneTransitions>().LoadScene(sceneIndex + 1);
            }     
            
        }
    }
}
