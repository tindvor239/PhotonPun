using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    private void Awake()
    {
        GameManager.NetworkInstantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
    }
}
