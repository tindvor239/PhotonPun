using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPun
{
    private enum MovementType
    {
        Linear = 0,
        Instant = 1
    }

    private enum MovementState
    {
        Idle,
        Moving
    }

    private enum SlideDirection
    {
        None,
        Left,
        Right
    }

    [SerializeField]
    private float _velocityMultiplier;

    [SerializeField]
    private float _decelerationMultiplier;

    [SerializeField]
    private Transform _rotateRoot;

    [SerializeField]
    private float _rotationMultiplier;

    [SerializeField]
    private float _jumpForce = 10f;
    [SerializeField]
    private float _stamina = 100f;
    [SerializeField]
    private float _staminaDrainRate = 10f;
    [SerializeField]
    private float _maxStamina = 100f;

    [SerializeField]
    private bool _canMoveWhileMidAir;

    [SerializeField]
    private MovementType _accelerationType;

    [SerializeField]
    private MovementType _decelerationType;

    [SerializeField]
    [RangeVector(0, 0, 0, float.MaxValue, float.MaxValue, float.MaxValue)]
    private Vector3 _maxGroundVelocity;

    [Space]
    [SerializeField]
    [RangeVector(float.MinValue, float.MinValue, float.MinValue, 0, 0, 0)]
    private Vector3 _minGroundVelocity;

    [SerializeField]
    private Rigidbody _rigidBody;

    [SerializeField]
    [Tooltip("To detect When player is landing on something")]
    private ColliderTriggerEventTrigger _feet;

    private bool _isGround;
    private bool _isSprinting;
    private Vector3 _minTempGroundVelocity;
    private Vector3 _maxTempGroundVelocity;
    private MovementState _movementState;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;

    #region PROPERTIES
    public bool IsGround => _isGround;
    #endregion

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _sprintAction = InputSystem.actions.FindAction("Sprint");

        _feet.onTriggerEnter.AddListener(LandingFeetOnSomething);
        _feet.onTriggerExit.AddListener(OnFeetOfTheGround);

        _stamina = _maxStamina;
        _minTempGroundVelocity = _minGroundVelocity;
        _maxTempGroundVelocity = _maxGroundVelocity;
    }

    private void OnDestroy()
    {
        _feet.onTriggerEnter.RemoveListener(LandingFeetOnSomething);
        _feet.onTriggerExit.RemoveListener(OnFeetOfTheGround);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!_isGround)
        {
            if (_canMoveWhileMidAir)
            {
                Move(_minGroundVelocity, _maxGroundVelocity);
            }
            return;
        }

        Move(_minGroundVelocity, _maxGroundVelocity);

        if (_sprintAction.WasPressedThisFrame() && _stamina > 0)
        {
            _minGroundVelocity = _minTempGroundVelocity * 2;
            _maxGroundVelocity = _maxTempGroundVelocity * 2;
            _isSprinting = true;
        }
        else if (_sprintAction.WasReleasedThisFrame())
        {
            _minGroundVelocity = _minTempGroundVelocity;
            _maxGroundVelocity = _maxTempGroundVelocity;
            _isSprinting = false;
        }

        if (!_isSprinting)
        {
            _stamina = Mathf.Clamp(_stamina + Time.deltaTime, 0, _maxStamina);
        }
        else if (_movementState == MovementState.Moving)
        {
            _stamina = Mathf.Clamp(_stamina - Time.deltaTime * _staminaDrainRate, 0, _maxStamina);
            if (_stamina == 0)
            {
                _minGroundVelocity = _minTempGroundVelocity;
                _maxGroundVelocity = _maxTempGroundVelocity;
                _isSprinting = false;
            }
        }

        if (_jumpAction.IsPressed())
        {
            // your jump code here.
            _rigidBody.linearVelocity += Vector3.up * (_jumpForce * Time.fixedDeltaTime);
            _rigidBody.linearVelocity = VectorExtend.Clamp(
            _rigidBody.linearVelocity,
            _minGroundVelocity,
            _maxGroundVelocity);
        }
    }

    private void Move(Vector3 min, Vector3 max)
    {
        if (_moveAction.IsPressed())
        {
            // rigidBody
            Accelerate(min, max);
        }
        else if (_movementState == MovementState.Moving)
        {
            Decelerate();
        }
    }

    private void RotateCharacter()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetDirection = hitInfo.point - _rotateRoot.position;
            targetDirection.y = 0; // Keep the character upright
            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                _rotateRoot.rotation = Quaternion.Slerp(_rotateRoot.rotation, targetRotation, _rotationMultiplier * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        RotateCharacter();
    }

    private void Accelerate(Vector3 min, Vector3 max)
    {
        Vector3 moveValue = _moveAction.ReadValue<Vector2>();
        switch (_accelerationType)
        {
            case MovementType.Linear:
                OnAcceleration(moveValue);
                break;
            case MovementType.Instant:
                _rigidBody.linearVelocity = OnAccelerateInstant(moveValue);
                break;
        }

        //Rotate
        // _rotateRoot.forward = Vector3.Lerp(
        //     _rotateRoot.forward,
        //     VectorExtend.Clamp(new Vector3(moveValue.x, 0, 0), Vector3.left, Vector3.right),
        //     _rotationMultiplier * Time.deltaTime
        // );

        _rigidBody.linearVelocity = ClampVelocity(min, max);
        _movementState = MovementState.Moving;
    }

    private void Decelerate()
    {
        switch (_decelerationType)
        {
            case MovementType.Linear:
                if (Mathf.Approximately(_rigidBody.linearVelocity.x, 0)
                    && Mathf.Approximately(_rigidBody.linearVelocity.z, 0))
                {
                    _movementState = MovementState.Idle;
                }
                else
                {
                    _rigidBody.linearVelocity = Vector3.Lerp(
                        _rigidBody.linearVelocity,
                        new Vector3(0, _rigidBody.linearVelocity.y, 0),
                        _decelerationMultiplier * Time.deltaTime);
                }
                // OnAcceleration(moveValue);
                break;
            case MovementType.Instant:
                // OnLinearVelocity(moveValue);
                _rigidBody.linearVelocity = new Vector3(0, _rigidBody.linearVelocity.y, 0) * Time.deltaTime;
                _movementState = MovementState.Idle;
                break;
        }
        // _rigidBody.linearVelocity = Utilities.Clamp(_rigidBody.linearVelocity, _minVelocity, _maxVelocity);
    }

    private Vector3 ClampVelocity(Vector3 min, Vector3 max)
    {
        return VectorExtend.Clamp(_rigidBody.linearVelocity, min, max);
    }

    private Vector3 OnAcceleration(Vector2 input)
    {
        input *= _velocityMultiplier;
        var accelerationForce =
            new Vector3(input.x, _rigidBody.linearVelocity.y, input.y) * Time.deltaTime;
        _rigidBody.AddForce(accelerationForce, ForceMode.Acceleration);
        return accelerationForce;
    }

    private Vector3 OnAccelerateInstant(Vector2 input)
    {
        input *= _velocityMultiplier;
        Vector3 target = new Vector3(input.x, _rigidBody.linearVelocity.y, input.y);
        return target;
    }

    private void LandingFeetOnSomething(Collider collider)
    {
        _isGround = true;
    }

    private void OnFeetOfTheGround(Collider collider)
    {
        _isGround = false;
    }
}
