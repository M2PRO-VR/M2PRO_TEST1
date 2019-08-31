using UnityEngine;
using Oculus.Avatar;
using Oculus.Platform;
using Oculus.Platform.Models;
using System.Collections;

public class OculusAvatarSync : MonoBehaviour
{

    private const string ROOM_NAME = "AvatarRoom";
    private const string GAME_VERSION = "v1.0";

    private void Awake()
    {
        PhotonNetwork.OnEventCall += OnEvent;
    }

    void Start()
    {
        Debug.Log("PhotonManager: ロビーに接続");
        PhotonNetwork.ConnectUsingSettings(GAME_VERSION);
    }

    void OnJoinedLobby()
    {
        Debug.Log("PhotonManager: ロビーに入室成功");
        JoinRoom("Player" + PhotonNetwork.player.ID.ToString());
    }

    public void JoinRoom(string playerName)
    {
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 20,
            IsOpen = true,
            IsVisible = true,
        };
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, null);
    }

    public readonly byte InstantiateVrAvatarEventCode = 123;

    public void OnJoinedRoom()
    {
        int viewId = PhotonNetwork.AllocateViewID();

        PhotonNetwork.RaiseEvent(InstantiateVrAvatarEventCode, viewId, true, new RaiseEventOptions() { CachingOption = EventCaching.AddToRoomCache, Receivers = ReceiverGroup.All });
    }

    private void OnEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode == InstantiateVrAvatarEventCode)
        {
            GameObject go = null;

            if (PhotonNetwork.player.ID == senderid)
            {
                go = Instantiate(Resources.Load("LocalAvatar")) as GameObject;
            }
            else
            {
                go = Instantiate(Resources.Load("RemoteAvatar")) as GameObject;
            }

            if (go != null)
            {
                PhotonView pView = go.GetComponent<PhotonView>();

                if (pView != null)
                {
                    pView.viewID = (int)content;
                }
            }
        }
    }

}