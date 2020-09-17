using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Phone : MonoBehaviour, IDragHandler
{
    public GameObject rules, message;
    public Text messageText;
    private Canvas canvas;
    RectTransform rectTransform;
    [SerializeField]
    [TextArea(3, 10)]
    private string[] messages;
    private int currentMessage;
    private int maxMessage;
    public GameObject nextButton, prevButton;
    public GameObject goalImage, noteImage;
    bool firstLevel;

    private void Start()
    {
        if (goalImage != null && noteImage != null)
        {
            firstLevel = true;
        }
        else firstLevel = false;
        currentMessage = 0;
        maxMessage = messages.Length - 1;
        messageText.text = messages[currentMessage];
        nextButton.SetActive(true);
        prevButton.SetActive(false);
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        Message();
    }

    public void Rules()
    {
        message.SetActive(false);
        rules.SetActive(true);
        nextButton.SetActive(false);
        prevButton.SetActive(false);
    }

    public void Message()
    {
        rules.SetActive(false);
        message.SetActive(true);
        nextButton.SetActive(true);
        prevButton.SetActive(true);
        SetButtons();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if (!DataHolder.dayStarted)
        {
            FindObjectOfType<GameController>().StartDay();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void NextMessage()
    {
        if (currentMessage < maxMessage)
        {
            currentMessage += 1;
            messageText.text = messages[currentMessage];
        }
        SetButtons();
        SetImage();
    }

    public void PrevMesssage()
    {
        if (currentMessage > 0)
        {
            currentMessage -= 1;
            messageText.text = messages[currentMessage];
        }
        SetButtons();
        SetImage();
    }

    public void SetButtons()
    {
        if (currentMessage == maxMessage)
        {
            nextButton.SetActive(false);
            prevButton.SetActive(true);
        }
        if (currentMessage == 0)
        {
            prevButton.SetActive(false);
            nextButton.SetActive(true);
        }
        if (currentMessage > 0 && currentMessage < maxMessage)
        {
            prevButton.SetActive(true);
            nextButton.SetActive(true);
        }
    }

    public void SetImage()
    {
        if (firstLevel)
        {
            goalImage.SetActive(false);
            noteImage.SetActive(false);
            if (currentMessage == 1)
            {
                goalImage.SetActive(true);
            }
            if (currentMessage == 3)
            {
                noteImage.SetActive(true);
            }
        }
    }
}
