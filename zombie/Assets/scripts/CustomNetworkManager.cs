
using UnityEngine;
using Mirror;
using Telepathy;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    public RulesManager RulesManager;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        if (RulesManager.ZPlayers.Count<1)
        {
            conn.identity.AssignClientAuthority(conn);
            RulesManager.AddZombie(conn.identity.gameObject);

        }
        else
        {
            conn.identity.AssignClientAuthority(conn);
            RulesManager.AddPlayer(conn.identity.gameObject);
            Debug.Log("yeah");
        }
    }
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (RulesManager.ZPlayers.Contains(conn.identity.gameObject))
        {
            RulesManager.ZPlayers.Remove(conn.identity.gameObject);
            
        }

        else
        {
            RulesManager.Players.Remove(conn.identity.gameObject);
        }
        base.OnServerDisconnect(conn);
    }



}
