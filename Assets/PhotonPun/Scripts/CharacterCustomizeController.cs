using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Cysharp.Text;

public class CharacterCustomizeController : MonoBehaviourPun
{
    private const byte SET_ACCESSORIES_EVENT = 0;
    private const byte SET_COLOR_EVENT = 1;

    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private GameObject[] _accessories;

    private RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };

    public void SetAccessories(int index)
    {
        // You would have to set the Receivers to All in order to receive this event on the local client as well.
        photonView.RPC(nameof(SetAccessoryByIndex), RpcTarget.All, index);
        // PhotonNetwork.RaiseEvent(SET_ACCESSORIES_EVENT, index, raiseEventOptions, SendOptions.SendReliable);
    }

    public void SetColor(Color color)
    {
        object[] datas = new object[] { color.r, color.g, color.b, color.a };
        _meshRenderer.material.color = color;
        photonView.RPC(nameof(OnChangeColor), RpcTarget.All, datas);
        // PhotonNetwork.RaiseEvent(SET_COLOR_EVENT, datas, raiseEventOptions, SendOptions.SendReliable);
    }

    // private void OnEnable()
    // {
    //     PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    // }

    // private void OnDisable()
    // {
    //     PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    // }

    private void OnEventReceived(EventData obj)
    {
        if (photonView.IsMine)
            return;
        
        if (obj.Code == SET_ACCESSORIES_EVENT)
        {
            int index = (int)obj.CustomData;
            SetAccessoryByIndex(index);
        }

        else if (obj.Code == SET_COLOR_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            Color color = new Color((float)datas[0], (float)datas[1], (float)datas[2], (float)datas[3]);
            _meshRenderer.material.color = color;
        }
    }

    [PunRPC]
    private void OnChangeColor(object[] datas)
    {
        if (photonView.IsMine)
            return;
        
        Color color = new Color((float)datas[0], (float)datas[1], (float)datas[2], (float)datas[3]);
        Debug.Log(ZString.Format("Received {0} ", color));
        _meshRenderer.material.color = color;
    }

    [PunRPC]
    private void SetAccessoryByIndex(int index)
    {
        for (int i = 0; i < _accessories.Length; i++)
        {
            _accessories[i].SetActive(i == index);
        }
    }
}
