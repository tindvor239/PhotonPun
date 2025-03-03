using SimpleUI;
using Singletons;
using Photon.Pun.UserInterfaces;
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField]
    private List<NetworkPrefab> _networkPrefabs = new();

    private void Start()
    {
        PanelManager.Instance.ShowPanelFromResources<RoomController>(UIName.ROOM_UI);
    }

    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        foreach (NetworkPrefab networkPrefab in Instance._networkPrefabs)
        {
            if (networkPrefab.prefab == obj)
            {
                GameObject result = PhotonNetwork.Instantiate(networkPrefab.path, position, rotation);
                return result;
            }
        }
        return null;
    }

    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    // private static void PopuplateNetworkedPrefabs()
    // {
    //     if (!Application.isEditor)
    //         return;

    //     GameObject[] results = Resources.LoadAll<GameObject>("");
    //     for (int i = 0; i < results.Length; i++)
    //     {
    //         if (results[i].GetComponent<PhotonView>() != null)
    //         {
    //             string path = AssetDatabase.GetAssetPath(results[i]);
    //             Instance._networkPrefabs.Add(new NetworkPrefab(results[i], path));
    //         }
    //     }
    // }
}
