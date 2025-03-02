using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public sealed class ColliderTriggerEventTrigger : MonoBehaviour
{
    [SerializeField]
    private LayerMask excludeLayers;
    [SerializeField]
    private UnityEvent<Collider> _onTriggerEnter;
    [SerializeField]
    private UnityEvent<Collider> _onTriggerExit;

    private Collider _collider;
    
    public UnityEvent<Collider> onTriggerEnter => _onTriggerEnter;
    public UnityEvent<Collider> onTriggerExit => _onTriggerExit;

    #region UNITY_METHODS
    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _onTriggerExit?.Invoke(other);
    }
    #endregion

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.excludeLayers = excludeLayers;
    }
}
