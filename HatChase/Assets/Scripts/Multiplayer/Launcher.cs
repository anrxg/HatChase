using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Connecting To Master...");
        PhotonNetwork.ConnectUsingSettings();
        MenuManager.instance.OpenMenu("LoadingMenu");
    }

    #region Overrides

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Matser!");
        OnJoinedLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.instance.OpenMenu("TitleMenu");
        Debug.Log("Lobby Joined...");
    }
    #endregion


}
