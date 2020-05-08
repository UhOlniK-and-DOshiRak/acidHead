using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject endDebt, endFood, endHomeLow, endHomeHigh, endLow, endHigh;

    public enum FinalType
    {
        debt,
        food,
        homeLow,
        homeHigh,
        low,
        high,
    }

    public void showFinal(FinalType type)
    {
        switch (type)
        {
            case FinalType.debt: endDebt.SetActive(true);
                break;
            case FinalType.food:
                endFood.SetActive(true);
                break;
            case FinalType.homeLow:
                endHomeLow.SetActive(true);
                break;
            case FinalType.homeHigh:
                endHomeHigh.SetActive(true);
                break;
            case FinalType.low:
                endLow.SetActive(true);
                break;
            case FinalType.high:
                endHigh.SetActive(true);
                break;
        }
    }

    public void End()
    {
        FindObjectOfType<GameController>().ToMainMenu();
    }
}
