using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.UserInterfaces
{
    public class RoomListMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Transform _content;
        [SerializeField]
        private RoomUserInterface _roomUserInterfacePrefab;

        private Dictionary<string, RoomUserInterface> _roomMap = new();

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            foreach (RoomInfo info in roomList)
            {
                if (info.RemovedFromList)
                {
                    Destroy(_roomMap[info.Name].gameObject);
                    _roomMap.Remove(info.Name);
                }
                else
                {
                    RoomUserInterface roomUserInterface = Instantiate(_roomUserInterfacePrefab, _content);
                    roomUserInterface.Setup(info);
                    _roomMap.Add(info.Name, roomUserInterface);
                }
            }
        }
    }
}