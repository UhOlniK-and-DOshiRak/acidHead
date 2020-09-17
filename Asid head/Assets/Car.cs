using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    Transform stopPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        stopPoint = GameObject.Find("CarGonePoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, stopPoint.position) > 3f)
        {
            transform.position = Vector2.MoveTowards(transform.position, stopPoint.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
