using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Phone : MonoBehaviour, IDragHandler
{
    public GameObject rules, message;
    private Canvas canvas;
    RectTransform rectTransform;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        Message();
    }

    public void Rules()
    {
        message.SetActive(false);
        rules.SetActive(true);
    }

    public void Message()
    {
        rules.SetActive(false);
        message.SetActive(true);
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
}
