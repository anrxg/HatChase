using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    [SerializeField] TMP_InputField roomNameInput;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] TMP_Text errorText;

    void Start()
    {
        Debug.Log("Connecting To Master...");
        PhotonNetwork.ConnectUsingSettings();
        MenuManager.instance.OpenMenu("LoadingMenu"); 
    }

    #region Photon Callback Overrides

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master!");
        PhotonNetwork.JoinLobby(); 
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby Joined...");
        MenuManager.instance.OpenMenu("TitleMenu"); 
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        MenuManager.instance.OpenMenu("RoomMenu");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name; 
    }

    public override void OnCreateRoomFailed(short returnCode, string errorMessage)
    {
        Debug.LogError("Create Room Failed: " + errorMessage);
        errorText.text = "Cannot create room: " + errorMessage;
        MenuManager.instance.OpenMenu("ErrorMenu");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room");
        MenuManager.instance.OpenMenu("TitleMenu"); 
    }

    #endregion

    #region Public Button Methods

    public void CreateRoom()
    {
        string roomName = roomNameInput.text;

        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Room name is empty.");
            return;
        }

        PhotonNetwork.CreateRoom(roomName);
        MenuManager.instance.OpenMenu("LoadingMenu"); 
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(); 
        MenuManager.instance.OpenMenu("LoadingMenu"); 
    }

    #endregion
}
