using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This is a TERRIBLE way to do this but whatever its not the point of this demo
 * @author A1C Jake Brower
 * @date 15 June 2021
 */
public class ClickHandler : MonoBehaviour
{
    public Text menuText;
    public Button tele;
    public Button players;
    public Button pubs;
    public Button settings;

    private void Start()
    {
        tele.onClick.AddListener(TeleportClick);
        players.onClick.AddListener(playersClick);
        pubs.onClick.AddListener(pubsClick);
        settings.onClick.AddListener(settingsClick);
    }

    public void TeleportClick()
    {
        menuText.text = "Teleport List";
    }

    public void playersClick()
    {
        menuText.text = "Player List";
    }

    public void pubsClick()
    {
        menuText.text = "Pubs";
    }

    public void settingsClick()
    {
        menuText.text = "Settings";
    }
}
