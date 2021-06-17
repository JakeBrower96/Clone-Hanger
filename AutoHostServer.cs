using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoHostServer : MonoBehaviour
{
    [SerializeField]
    NetworkManager networkManager;

    private void Start()
    {
        if (!Application.isBatchMode) //headless build
        {
            Debug.Log("Client build");
            networkManager.StartClient();
        } else
        {
            Debug.Log("Server build");
        }
    }

    public void JoinLocal()
    {
        networkManager.networkAddress = "localhost";
        networkManager.StartClient(); 
    }
}
