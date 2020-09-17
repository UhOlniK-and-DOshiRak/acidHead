using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public Transform clientSpawnPoint;
    public Client[] clients;
    private Client currentClient;
    private int currentClientIndex;
    public GameObject car;
    public Transform carSpawnPoint;
    bool isCar;

    private void Start()
    {
        currentClientIndex = 0;
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject car in cars)
        {
            Destroy(car);
        }
        isCar = false;
    }

    private void Update()
    {
        if (DataHolder.dayStarted)
        {
            if (GameObject.FindGameObjectsWithTag("Client").Length == 0 && currentClientIndex < clients.Length)
            {
                showNewClient();
                currentClientIndex++;
            }
            if (currentClientIndex == clients.Length && GameObject.FindGameObjectsWithTag("Client").Length == 0)
            {
                Time.timeScale = 15f;
            }
            //else if (Time.time >= carTime)
            //{
            //    if (Time.time > carTimewait)
            //    {
            //        Instantiate(car, carSpawnPoint.position, carSpawnPoint.rotation);
            //        carTime += timeBetweenCars;
            //    }
            //}
            if (currentClientIndex % 2 == 0 && !isCar)
            {
                Instantiate(car, carSpawnPoint.position, carSpawnPoint.rotation);
                isCar = true;
            }
            else if (currentClientIndex % 2 != 0)
            {
                isCar = false;
            }

        }
        
    }

    private void showNewClient()
    {
        currentClient = clients[currentClientIndex];
        Instantiate(currentClient, clientSpawnPoint.position, clientSpawnPoint.rotation);
    }

}
