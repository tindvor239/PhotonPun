using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cysharp.Text;

public class TestConnect : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameSettings _gameSettings;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = _gameSettings.NickName;
        PhotonNetwork.GameVersion = _gameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        DebugExtend.Log("Connect successfully", Color.green);
        DebugExtend.Log(ZString.Format("Welcome: {0}", PhotonNetwork.LocalPlayer.NickName), Color.white);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        DebugExtend.Log(ZString.Format("Disconnected from server for reason {0}", cause), Color.red);
    }
}