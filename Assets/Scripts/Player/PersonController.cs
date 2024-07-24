using UnityEngine;

public class PersonController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    private Animator _animator;
    private CharacterController _characterController;
    
    private Vector3 _moveInput;
    private Vector3 _move;
    private Vector2 _currentBleandAnim;
    private Vector2 _animationVelocity;
    [SerializeField] private float _playerSpeed;
    private bool isGround;
    private Vector3 _playerVelocity;
    [SerializeField] private float _jumpHeight;
    private float _gravityValue = -9.81f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _animSmoothTime = 0.2f;
    [SerializeField] private float _animationPlayTransition;
    private int _moveXAnimationParametrId;
    private int _moveYAnimationParametrId;
    private int _runAnimationParamId;
    private int _idleAnimationParamId;
    
    public Vector2 MoveInput
    {
        set
        {
            _moveInput.x = value.x;
            _moveInput.y = value.y;
        }
    }
    public bool isJump;
    public bool isRun;


    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _animator = GetComponent<Animator>();
        _moveXAnimationParametrId = Animator.StringToHash("MovementX");
        _moveYAnimationParametrId = Animator.StringToHash("MovementY");
        _runAnimationParamId = Animator.StringToHash("isRunning");
        _idleAnimationParamId = Animator.StringToHash("isIdle");
        
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheak();
        MovedCharacter();
        RotateToDirection();
    }

  
    private void GroundCheak()
    {
        isGround = _characterController.isGrounded;
        if (isGround && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0;
        }
    }
    private void MovedCharacter()
    {
        _currentBleandAnim = Vector2.SmoothDamp(_currentBleandAnim,_moveInput, ref _animationVelocity, _animSmoothTime);
        _move = new Vector3(_currentBleandAnim.x, 0, _currentBleandAnim.y);
        _move = _cameraTransform.right * _moveInput.x + _cameraTransform.forward * _moveInput.y;
        _move.y = 0;
        float speed = isRun ? _playerSpeed * 4 : _playerSpeed; 
        _characterController.Move(_move * Time.deltaTime * speed);
        
        _animator.SetFloat(_moveXAnimationParametrId,_currentBleandAnim.x);
        _animator.SetFloat(_moveYAnimationParametrId,_currentBleandAnim.y);
        _animator.SetBool("isWalk", _moveInput != Vector3.zero && !isRun); 
        
         if (_moveInput == Vector3.zero && !isRun )
         {
             _animator.SetBool(_idleAnimationParamId, true);
         }
         else
         {
             _animator.SetBool(_idleAnimationParamId, false);
         }
    }

    private void RotateToDirection()
    {
        if (_moveInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.Euler(0f,_cameraTransform.eulerAngles.y,0f);
            transform.rotation = Quaternion.Lerp(transform.rotation,rotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
