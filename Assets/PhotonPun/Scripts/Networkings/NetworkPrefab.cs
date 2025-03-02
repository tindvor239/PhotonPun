using System;
using UnityEngine;

[Serializable]
public class NetworkPrefab
{
    public GameObject prefab;
    public string path;

    public NetworkPrefab(GameObject prefab, string path)
    {
        this.prefab = prefab;
        this.path = path;
    }
}