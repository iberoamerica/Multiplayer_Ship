using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Multiplayer.Playmode;
using Unity.Netcode;
public class MPPM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mppmTag = CurrentPlayer.Tag;
        var networkManager = NetworkManager.Singleton;
        if (mppmTag.Contains("Server"))
        {
            networkManager.StartServer();
        }else if (mppmTag.Contains("Host"))
        {
            networkManager.StartHost();
        }else if (mppmTag.Contains("Client"))
        {
            networkManager.StartClient();
        }
    }
}
