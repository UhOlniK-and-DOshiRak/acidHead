using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Book : MonoBehaviour, IDragHandler
{
    public GameObject page1, page2, page3;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 initialRectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        initialRectTransform = rectTransform.anchoredPosition;
        //rectTransform.anchoredPosition = FindObjectOfType<Workspace>().GetComponent<RectTransform>().anchoredPosition;
    }

    public void ToPage1()
    {
        FindObjectOfType<Sound>().BookSound();
        page1.SetActive(true);
    }

    public void ToPage2()
    {
        FindObjectOfType<Sound>().BookSound();
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void ToPage3()
    {
        FindObjectOfType<Sound>().BookSound();
        page2.SetActive(false);
        page3.SetActive(true);
    }

    public void ToPage4()
    {
        FindObjectOfType<Sound>().BookSound();
        page3.SetActive(false);
    }


    public void Close()
    {
        FindObjectOfType<Sound>().BookSound();
        rectTransform.anchoredPosition = initialRectTransform;
        gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent != FindObjectOfType<OrderSlot>().transform)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}
