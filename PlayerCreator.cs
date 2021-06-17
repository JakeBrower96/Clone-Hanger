using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

/**
 * This is going to hold the player information
 * Username
 * Server the player is on(this will be created later)
 * @author A1C Jake Brower
 * @date 15 June 2021
 */
public class PlayerCreator : NetworkBehaviour
{
    private string username;
    private string serverName;

    public static PlayerCreator localPlayer;
    NetworkMatchChecker networkMatchChecker;

    [SyncVar]public string gameID;

    public void Start()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        networkMatchChecker = GetComponent<NetworkMatchChecker>();
    }

    public void HostGame()
    {
        string matchID = ServerCreator.GetRandomMatchID();
        CmdHostGame(matchID);
    }

    [Command]
    void CmdHostGame(string _gameID)
    {
        gameID = _gameID;
        if (ServerCreator.instance.HostGame(_gameID, gameObject))
        {
            Debug.Log("Shit worked");
            networkMatchChecker.matchId = _gameID.ToGUID();
            TargetHostGame(true, _gameID);
        } else
        {
            Debug.Log("Error has occured");
            TargetHostGame(false, _gameID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string _gameID)
    {
        MenuHandler.menuHandler.HostSuccess(success, _gameID);

    }

    public void JoinGame(string gameID)
    {
        CmdJoinGame(gameID);
    }

    [Command]
    void CmdJoinGame(string _gameID)
    {
        gameID = _gameID;
        if (ServerCreator.instance.JoinGame(_gameID, gameObject))
        {
            Debug.Log("Shit worked");
            networkMatchChecker.matchId = _gameID.ToGUID();
            TargetJoinGame(true, _gameID);
        }
        else
        {
            Debug.Log("Error has occured");
            TargetJoinGame(false, _gameID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string _gameID)
    {
        MenuHandler.menuHandler.JoinSuccess(success, _gameID);

    }

    public void BeginGame()
    {
        CmdBeginGame();
        Debug.Log("Called CmDBeginGame");
    }

    [Command]
    void CmdBeginGame()
    {
        ServerCreator.instance.BeginGame(gameID);
        Debug.Log("Error has occured in CMDBeginGame");
        
    }

    public void StartGame()
    {
        TargetBeginGame();
        Debug.Log("Starting Game");
    }

    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log("Loading Main Scene");
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        Debug.Log("Loaded Scene successfully");
    }
}
