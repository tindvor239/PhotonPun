using SimpleUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.UserInterfaces
{
    public class JoinedRoomController : BasePunUserInterface
    {
        [SerializeField]
        private TMP_Text _roomName;
        [SerializeField]
        private Button _leaveButton;
        [SerializeField]
        private Button _startButton;

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _roomName.SetText(PhotonNetwork.CurrentRoom.Name);
        }

        protected override void SetData()
        {
            _leaveButton.onClick.AddListener(OnLeave);
            _startButton.onClick.AddListener(OnStartGame);
        }

        private void OnLeave()
        {
            PhotonNetwork.LeaveRoom();
            Hide();
            PanelManager.Instance.ShowPanelFromResource<RoomController>(UIName.ROOM_UI);
        }

        private void OnStartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}
