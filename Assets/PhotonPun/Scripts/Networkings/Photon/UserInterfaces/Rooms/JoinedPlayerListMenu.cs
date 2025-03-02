using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Cysharp.Text;

namespace Photon.Pun.UserInterfaces
{
    public class JoinedPlayerListMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private TMP_Text _joinedAmount;
        [SerializeField]
        private Transform _content;
        [SerializeField]
        private JoinedPlayerInterface _joinedPlayerInterfacePrefab;

        private Dictionary<string, JoinedPlayerInterface> _playerMap = new();

        private void Awake()
        {
            Debug.Log(PhotonNetwork.CurrentRoom);
            foreach (KeyValuePair<int, Player> pair in PhotonNetwork.CurrentRoom.Players)
            {
                AddPlayer(pair.Value);
            }
            SetAmount();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            DebugExtend.Log(ZString.Format("Player {0} has join", newPlayer), Color.green);
            AddPlayer(newPlayer);
            SetAmount();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            DebugExtend.Log(ZString.Format("Player {0} has left", otherPlayer), Color.red);

            Destroy(_playerMap[otherPlayer.NickName].gameObject);
            _playerMap.Remove(otherPlayer.NickName);
        }

        private void AddPlayer(Player newPlayer)
        {
            JoinedPlayerInterface joinedPlayerInterface = Instantiate(_joinedPlayerInterfacePrefab, _content);
            joinedPlayerInterface.Setup(newPlayer);
            _playerMap.Add(newPlayer.NickName, joinedPlayerInterface);
        }

        private void SetAmount()
        {
            _joinedAmount.SetText("{0}/ {1}", PhotonNetwork.CurrentRoom.Players.Count, PhotonNetwork.CurrentRoom.MaxPlayers);
        }
    }
}