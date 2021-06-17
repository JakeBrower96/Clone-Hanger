using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerModel : NetworkBehaviour
{

    List<PlayerCreator> players = new List<PlayerCreator>();

    public void AddPlayer(PlayerCreator player)
    {
        players.Add(player);
    }
}
