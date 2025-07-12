using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] Text roomNameText;
    public RoomInfo roomInfo;

    public void SetUp(RoomInfo _roomInfo)
    {
        roomInfo = _roomInfo;
        roomNameText.text = _roomInfo.Name;
    }

    public void OnClick()
    {
        Launcher.instance.JoinRoom(roomInfo);
    }

}
