using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CashCount : MonoBehaviour, IDropHandler
{
    bool isActive;
    private Animator animator;
    [SerializeField]
    private Image[] images;

    private List<Cash> cashes;
    int sum;
    public Text sumText;

    Image GetImage(int value)
    {
        switch (value)
        {
            default:
            case 1: return images[0];
            case 2: return images[1];
            case 3: return images[2];
            case 5: return images[3];
            case 7: return images[4];
            case 10: return images[5];
            case 15: return images[6];
            case 20: return images[7];
        }
    }

    void Start()
    {
        isActive = false;
        animator = GetComponent<Animator>();

        cashes = new List<Cash>();
        sum = 0;
        sumText.text = sum.ToString();
        sumText.fontSize = 55;
        sumText.color = Color.green;
    }

    public void GetAnimation()
    {
        if (!isActive) Show();
        else Hide();
    }

    public void Show()
    {
        FindObjectOfType<Sound>().OpenCashSound();
        isActive = true;
        animator.SetTrigger("show");
    }

    public void Hide()
    {
        
        isActive = false;
        animator.SetTrigger("hide");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop Cash");
        Cash cash = eventData.pointerDrag.GetComponent<Cash>();
        if (cash != null)
        {
            AddCash(cash);
        }
    }

    void AddCash(Cash cash)
    {
        Image currentImage = GetImage(cash.value);
        bool exists = false;
        foreach (Cash cashEx in cashes)
        {
            if (cashEx.value == cash.value)
            {
                cashEx.amount += cash.amount;
                currentImage.gameObject.transform.Find("count").GetComponent<Text>().text = cashEx.amount.ToString();
                exists = true;
                Destroy(cash.gameObject);
            }
        }
        if (!exists)
        {
            cashes.Add(cash);
            currentImage.gameObject.transform.Find("count").GetComponent<Text>().text = cash.amount.ToString();
            Color c = currentImage.color;
            c.a = 255;
            currentImage.color = c;
            Destroy(cash.gameObject);
        }
        SummarizeCash();
    }

    void SummarizeCash()
    {
        sumText.fontSize = 55;
        sumText.color = Color.green;
        sum = 0;
        foreach (Cash cash in cashes)
        {
            sum += (cash.value * cash.amount);
        }
        sumText.text = sum.ToString();
    }

    public void CheckCash()
    {
        FindObjectOfType<Sound>().TumblerSound();
        sumText.color = Color.green;
        if (GameObject.FindGameObjectWithTag("Client").GetComponent<Order>().GetPrice() - sum <= 0)
        {
            sumText.fontSize = 30;
            sumText.text = "Достаточно денег!";
        }
        else
        {
            sumText.color = Color.red;
            sumText.fontSize = 30;
            sumText.text = "Не хватает денег!";
        }
    }

    public void Clear()
    {
        sum = 0;
        sumText.text = sum.ToString();
        sumText.fontSize = 55;
        sumText.color = Color.green;
        cashes = new List<Cash>();
        foreach (Image image in images)
        {
            Color c = image.color;
            c.a = 0.3f;
            image.color = c;
            image.gameObject.transform.Find("count").GetComponent<Text>().text = "0";
        }
    }
}
