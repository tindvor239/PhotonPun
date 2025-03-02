using TMPro;
using UnityEngine;
using Photon.Realtime;

namespace Photon.Pun.UserInterfaces
{
    public class JoinedPlayerInterface : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;

        public void Setup(Player player)
        {
            _name.SetText(player.NickName);
        }
    }
}