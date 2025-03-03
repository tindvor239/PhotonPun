using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed = 10;

    private void Start()
    {
        foreach (PlayerController playerController in FindObjectsByType<PlayerController>(FindObjectsSortMode.InstanceID))
        {
            if (playerController.photonView.IsMine)
            {
                _target = playerController.transform;
                return;
            }
        }
    }

    private void Update()
    {
        Vector3 target = _target.position;
        target.y = transform.position.y;
        
        transform.position = target;
    }
}
