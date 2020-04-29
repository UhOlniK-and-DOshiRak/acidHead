using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public Transform clientSpawnPoint;
    public Client[] clients;
    private Client currentClient;
    private int currentClientIndex;

    private void Start()
    {
        currentClientIndex = 0;
    }

    private void Update()
    {
        if (DataHolder.dayStarted)
        {
            if (GameObject.FindGameObjectsWithTag("Client").Length == 0 && currentClientIndex < clients.Length)
            {
                showNewClient();
                Debug.Log("Client #" + currentClientIndex + " appears");
                currentClientIndex++;
            }
        }
    }

    private void showNewClient()
    {
        currentClient = clients[currentClientIndex];
        Instantiate(currentClient, clientSpawnPoint.position, clientSpawnPoint.rotation);
    }

}
