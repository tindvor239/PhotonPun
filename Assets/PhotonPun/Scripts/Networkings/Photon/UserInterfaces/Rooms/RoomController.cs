using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Cysharp.Text;
using SimpleUI;

namespace Photon.Pun.UserInterfaces
{
    public class RoomController : BasePunUserInterface
    {
        [SerializeField]
        private TMP_InputField _roomNameInput;
        [SerializeField]
        private Button _createRoom;

        private void JoinOrCreateRoom()
        {
            if (!PhotonNetwork.IsConnected)
            {
                return;
            }
            RoomOptions roomOptions = new();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.JoinOrCreateRoom(_roomNameInput.text, roomOptions, TypedLobby.Default);
        }

        public override void OnCreatedRoom()
        {
            DebugExtend.Log("Created Room Successully", Color.green);
            PanelManager.Instance.ShowPanelFromResources<JoinedRoomController>(UIName.JOINED_ROOM_UI);
            Hide();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            DebugExtend.Log(ZString.Format("Created Room Failed: {0}", message), Color.green);
        }

        protected override void SetData()
        {
            _createRoom.onClick.AddListener(JoinOrCreateRoom);
        }
    }

}