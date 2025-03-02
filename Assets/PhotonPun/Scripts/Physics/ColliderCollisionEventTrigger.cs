using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public sealed class ColliderCollisionEventTrigger : MonoBehaviour
{
    [Space]
    [SerializeField]
    private UnityEvent<Collision> _onCollisionEnter = new();
    [SerializeField]
    private UnityEvent<Collision> _onCollisionExit = new();

    #region PROPERTY
    public UnityEvent<Collision> onCollisionEnter => _onCollisionEnter;
    public UnityEvent<Collision> onCollisionExit => _onCollisionExit;
    #endregion

    private void OnCollisionEnter(Collision other)
    {
        _onCollisionEnter?.Invoke(other);
    }

    private void OnCollisionExit(Collision other)
    {
        onCollisionExit?.Invoke(other);
    }
}
