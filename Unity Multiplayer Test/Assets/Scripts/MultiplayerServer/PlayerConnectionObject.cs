using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject PlayerUnitPrefab;

    [SyncVar(hook ="OnPlayerNameChanged")]
    public string PlayerName = "Anonymous";

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        CmdSpawnMyUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            CmdSpawnMyUnit();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            string n = "Quill" + Random.Range(1, 100);

            CmdChangePlayerName(n);
        }
    }

    void OnPlayerNameChanged(string newName)
    {
        Debug.Log("OnPlayerNameChanged: " + PlayerName + " NewName: " + newName);

        PlayerName = newName;

        gameObject.name = "PlayerConnectionObject [" + newName + "]";
    }

    ///////////////////// COMMANDS /////////////////////

    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(PlayerUnitPrefab);


        //go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void CmdChangePlayerName(string n)
    {
        PlayerName = n;

        //RpcChangePlayerName(PlayerName); 
    }

    //    [ClientRpc]
    //    void RpcChangePlayerName(string n)
    //    {
    //        PlayerName = n;
    //    }
}
