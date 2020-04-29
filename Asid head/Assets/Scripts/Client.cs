using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public float speed;
    Transform stopPoint, gonePoint;
    bool onStopPoint, alreadySelected;
    public GameObject onStopIcon;
    DialogTrigger dialogTrigger;

    //DialogController dialogController;

    private void Start()
    {
        //dialogController = GameObject.FindGameObjectWithTag("ClientMenu").GetComponent<DialogController>();
        stopPoint = GameObject.FindGameObjectWithTag("Player").transform;
        gonePoint = GameObject.Find("ClientGonePoint").GetComponent<Transform>();
        onStopPoint = false;
        DataHolder.currentClientComplete = false;
        dialogTrigger = GetComponent<DialogTrigger>();
        alreadySelected = false;
    }

    private void Update()
    {
        if (onStopPoint == false)
        {
            if (Vector2.Distance(transform.position, stopPoint.position) > 2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, stopPoint.position, speed * Time.deltaTime);
            }
            else
            {
                onStopPoint = true;
                onStopIcon.SetActive(true);
            }
        }
        else
        {
            if (DataHolder.currentClientComplete == true)
            {
                if (Vector2.Distance(transform.position, gonePoint.position) > 0f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, gonePoint.position, speed * Time.deltaTime);
                }
                else {
                    Destroy(this.gameObject);
                }
            }
        }

    }

    private void OnMouseDown()
    {
        if (onStopPoint == true && DataHolder.currentClientComplete == false && alreadySelected == false)
        {
            Destroy(onStopIcon);
            dialogTrigger.TriggerDialog();
            alreadySelected = true;
            //DataHolder.currentClientComplete = true;
            //dialogController.initDialog();
        }
    }
}
