using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "PhotonPun/GameSettings", order = 0)]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "0.0.0";
    [SerializeField]
    private string _nickName = "Player";

    public string GameVersion => _gameVersion;
    public string NickName
    {
        get
        {
            int value = Random.Range(0, 9999);
            return _nickName + value;
        }
    }
}
