using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This handles the username and player info for the dedicated server
 * won't make a unique server yet (TODO: implement)
 * @author A1C Jake Brower
 * @date 15 June 2021
 */
public class MenuHandler : MonoBehaviour
{
    public InputField username;
    public InputField serverName;
    public Button joinGame;
    public Button hostGame;

    [SerializeField]
    Canvas loadingScreenIG;

    [SerializeField]
    Text gameIDText;

    public static MenuHandler menuHandler;
    [SerializeField]
    GameObject begingameButton;

    private void Start()
    {
        menuHandler = this;
    }

    public void Host()
    {
        username.interactable = false;
        serverName.interactable = false;
        joinGame.interactable = false;
        hostGame.interactable = false;
        PlayerCreator.localPlayer.HostGame();
    }

    public void HostSuccess(bool success, string gameID)
    {
        if (success)
        {
            loadingScreenIG.enabled = true;
            gameIDText.text = gameID;
            begingameButton.SetActive(true);
        }
        else
        {
            username.interactable = true;
            serverName.interactable = true;
            joinGame.interactable = true;
            hostGame.interactable = true;
        }
    }

    public void Join()
    {
        username.interactable = false;
        serverName.interactable = false;
        joinGame.interactable = false;
        hostGame.interactable = false;
        PlayerCreator.localPlayer.JoinGame(serverName.text);
    }

    public void JoinSuccess(bool success, string gameID)
    {
        if (success)
        {
            loadingScreenIG.enabled = true;
            gameIDText.text = gameID;
        }
        else
        {
            username.interactable = true;
            serverName.interactable = true;
            joinGame.interactable = true;
            hostGame.interactable = true;
        }
    }

    public void BeginGame()
    {
        PlayerCreator.localPlayer.BeginGame();
        loadingScreenIG.enabled = false;
        gameIDText.enabled = false;
        begingameButton.SetActive(false);
        menuHandler.gameObject.SetActive(false);
        Debug.Log("CLicked Begin");
    }
}
