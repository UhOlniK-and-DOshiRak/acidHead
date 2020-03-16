using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject dialogPanel, sellPanel;
    public Button sellButton, refuseButton;

    public void initDialog()
    {
        if (dialogPanel.active == false)
        {
            dialogPanel.SetActive(true);
            sellButton.interactable = true;
            refuseButton.interactable = true;
        }
    }

    public void onRefuse()
    {
        dialogPanel.SetActive(false);
        DataHolder.currentClientComplete = true;
    }

    public void onSell()
    {
        sellPanel.SetActive(true);
        sellButton.interactable = false;
        refuseButton.interactable = false;
    }

    public void onComplete()
    {
        sellPanel.SetActive(false);
        dialogPanel.SetActive(false);
        DataHolder.currentClientComplete = true;
    }
}
