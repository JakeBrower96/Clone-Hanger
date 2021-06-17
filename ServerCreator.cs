using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;

/**
 * This will create the various servers based off of name (TODO: Implement)
 * the dedicated server will store the players inside of it (for now
 * just the usernames) and the server name
 * @author A1C Jake Brower
 * @date 15 June 2021
 */
[System.Serializable]
public class Server
{
    public string gameID;
    public syncListGO players = new syncListGO ();

    public Server(string gameID, GameObject host)
    {
        this.gameID = gameID;
        players.Add(host);
    }

    public Server() { }
}

[System.Serializable]
public class syncListGO : SyncList<GameObject>
{

}

[System.Serializable]
public class syncListGames : SyncList<Server>
{

}

public class ServerCreator : NetworkBehaviour
{

    public static ServerCreator instance;
    public syncListGames servers = new syncListGames();
    public SyncListString gameIds = new SyncListString();

    [SerializeField]
    GameObject modelPrefab;

    public void Start()
    {
        instance = this;
    }
    public static string GetRandomMatchID()
    {
        string randID = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int rand = UnityEngine.Random.Range(0, 36);
            if (rand < 26)
            {
                randID += (char)(rand + 65);
            } else
            {
                randID += (rand - 26).ToString();
            }
        }
        Debug.Log("New Awacs ID: " + randID);
        return randID;
    }

    public bool HostGame(string gameID, GameObject host)
    {
        if (!gameIds.Contains(gameID))
        {
            gameIds.Add(gameID);
            servers.Add(new Server(gameID, host));
            Debug.Log("New Server made");
            return true;
        } else
        {
            Debug.Log("Game ID already exists");
            return false;
        }
    }

    public bool JoinGame(string gameID, GameObject player)
    {
        if (gameIds.Contains(gameID))
        {
            for (int i = 0; i < servers.Count; i++)
            {
                if (servers[i].gameID == gameID)
                {
                    servers[i].players.Add(player);
                    break;
                }
            }
            Debug.Log("Awacs Found");
            return true;
        }
        else
        {
            Debug.Log("Game ID doesn't exists");
            return false;
        }
    }

    public void BeginGame(string _gameID)
    {
        Debug.Log("Only See this one time JCF"); 
        ServerModel newModel = Instantiate(modelPrefab).GetComponent<ServerModel>();
        newModel.GetComponentInParent<NetworkMatchChecker>().matchId = _gameID.ToGUID();



        for (int i = 0; i < servers.Count; i++)
        {
            if(servers[i].gameID == _gameID)
            {
                Debug.Log("Found Server Correctly");
                foreach (var player in servers[i].players)
                {
                    PlayerCreator playerCreator = player.GetComponent<PlayerCreator>();
                    newModel.AddPlayer(playerCreator);
                    Debug.Log("Added Player");
                    playerCreator.StartGame();
                }
                break;
            }
        }
    }
}

public static class MatchExtentions
{
    public static Guid ToGUID(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

        byte[] input = Encoding.Default.GetBytes(id);
        byte[] hashBytes = provider.ComputeHash(input);

        return new Guid(hashBytes);
    }
}

