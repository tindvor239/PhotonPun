using TMPro;
using UnityEngine;
using Cysharp.Text;
using Photon.Realtime;
using UnityEngine.UI;
using SimpleUI;

namespace Photon.Pun.UserInterfaces
{
    public class RoomUserInterface : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private TMP_Text _roomName;
        [SerializeField]
        private TMP_Text _roomPlayerAmount;
        [SerializeField]
        private Button _joinButton;

        string _roomID;

        private void Awake()
        {
            _joinButton.onClick.AddListener(JoinRoom);
        }

        private void OnDestroy()
        {
            _joinButton.onClick.RemoveListener(JoinRoom);
        }

        private void JoinRoom()
        {
            PhotonNetwork.JoinRoom(_roomID);
        }

        public void Setup(RoomInfo roomInfo)
        {
            _roomID = roomInfo.Name;
            _roomName.SetText(roomInfo.Name);
            _roomPlayerAmount.SetText(ZString.Format("{0}/{1}", roomInfo.PlayerCount, roomInfo.MaxPlayers));
        }

        public override void OnJoinedRoom()
        {
            PanelManager.Instance.ShowPanelFromResources<JoinedRoomController>(UIName.JOINED_ROOM_UI);
        }
    }
}