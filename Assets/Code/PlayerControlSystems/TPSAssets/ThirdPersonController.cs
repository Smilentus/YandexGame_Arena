using Cinemachine;
using UnityEngine;

// TODO: redecorate and recook code [1/2]

namespace Dimasyechka.Code.PlayerControlSystems.TPSAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class ThirdPersonController : MonoBehaviour
    {
        private const float _inputThreshold = 0.01f;

        private readonly int _animIDSpeed = Animator.StringToHash("Speed");
        private readonly int _animIDGrounded = Animator.StringToHash("Grounded");
        private readonly int _animIDJump = Animator.StringToHash("Jump");
        private readonly int _animIDFreeFall = Animator.StringToHash("FreeFall");
        private readonly int _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        private readonly int _animIDStrafing = Animator.StringToHash("IsStrafing");
        private readonly int _animIDHorizontal = Animator.StringToHash("HorizontalMovement");
        private readonly int _animIDVertical = Animator.StringToHash("VerticalMovement");


        [Header("Movement Settings")]
        [SerializeField]
        private float _strafingSpeed = 1.0f;
        public float StrafingSpeed => _strafingSpeed;

        [SerializeField]
        private float _walkingSpeed = 2.0f;
        public float WalkingSpeed => _walkingSpeed;

        [SerializeField]
        private float _sprintSpeed = 5.335f;
        public float SprintSpeed => _sprintSpeed;

        [SerializeField]
        private float _jumpHeight = 1.2f;
        public float JumpHeight => _jumpHeight;

        [SerializeField]
        private float _gravity = -9.8f;
        public float Gravity => _gravity;

        [SerializeField]
        private float _jumpTimeout = 0.5f;
        public float JumpTimeout => _jumpTimeout;

        [SerializeField]
        private float _fallTimeOut = 0.15f;
        public float FallTimeout => _fallTimeOut;

        [Tooltip("Скорость поворота в указанном направлении перемещения")]
        [Range(0.0f, 0.3f)]
        private float _rotationSmoothTime = 0.12f;
        public float RotationSmoothTime => _rotationSmoothTime;

        private float _speedChangeRate = 10.0f;
        public float SpeedChangeRate => _speedChangeRate;


        [Header("Footsteps Sounds")]
        [SerializeField]
        private AudioClip _landingAudioClip;
        public AudioClip LandingAudioClip => _landingAudioClip;

        [SerializeField]
        private AudioClip[] _footstepsAudioClips;
        public AudioClip[] FootstepAudioClips => _footstepsAudioClips;

        [Range(0, 1)]
        [SerializeField]
        private float _footstepAudioVolume = 0.5f;
        public float FootstepAudioVolume => _footstepAudioVolume;

        [SerializeField]
        private float _groundedOffset = -0.14f;
        public float GroundedOffset => _groundedOffset;

        [SerializeField]
        private float _groundedRadius = 0.28f;
        public float GroundedRadius => _groundedRadius;

        [SerializeField]
        private LayerMask _groundLayers;
        public LayerMask GroundLayers => _groundLayers;


        [Header("Cinemachine Settings")]
        [SerializeField]
        private CinemachineVirtualCamera _cineCamera;

        [SerializeField]
        private GameObject _cinemachineCameraTarget;

        [SerializeField]
        private float _topClamp = 70.0f;

        [SerializeField]
        private float _bottomClamp = -30.0f;

        [SerializeField]
        private float _cameraVerticalSens = 30f;
        public float CameraVerticalSens { get => _cameraVerticalSens; set => _cameraVerticalSens = value; }

        [SerializeField]
        private float _cameraHorizontalSens = 30f;
        public float CameraHorizontalSens { get => _cameraHorizontalSens; set => _cameraHorizontalSens = value; }

        [SerializeField]
        private bool _lockCameraPosition = false;
        public bool LockCameraPosition { get => _lockCameraPosition; set => _lockCameraPosition = value; }


        [Header("References")]
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private CharacterController _controller;

        [SerializeField]
        private PlayerInputHandler _playerInputHandler;

        [SerializeField]
        private GameObject _mainCamera;


        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private bool _isGrounded = true;
        private float _moveSpeed;
        private float _animationBlend;
        private float _horizontalBlend = 0.0f;
        private float _verticalBlend = 0.0f;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        private bool _hasAnimator;


        private bool _isControllerBlocked = false;


        private void Awake()
        {
            _hasAnimator = _animator != null;
        }


        private void Start()
        {
            _cinemachineTargetYaw = _cinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void FixedUpdate()
        {
            if (_isControllerBlocked)
            {
                if (_hasAnimator)
                {
                    _animator.SetFloat(_animIDMotionSpeed, 0);
                }

                return;
            }

            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void LateUpdate()
        {
            if (_isControllerBlocked) return;

            CameraRotation();

            //if (_cineCameraTransposer != null)
            //{
            //    _cineCameraTransposer.CameraDistance = Mathf.Lerp(_cineCameraTransposer.CameraDistance, 3.0f, StrafingCameraSpeed * Time.deltaTime);
            //}
        }

        public void SetControllerBlockState(bool isBlocked)
        {
            _isControllerBlocked = isBlocked;
        }


        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            _isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            //if (_hasAnimator)
            //{
            //    _animator.SetBool(_animIDGrounded, _isGrounded);
            //}
        }

        private void CameraRotation()
        {
            if (_playerInputHandler.LookVector.sqrMagnitude >= _inputThreshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = Time.deltaTime;

                _cinemachineTargetYaw += _playerInputHandler.LookVector.x * CameraHorizontalSens * deltaTimeMultiplier;
                _cinemachineTargetPitch += _playerInputHandler.LookVector.y * CameraVerticalSens * deltaTimeMultiplier;
            }

            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

            _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            float targetSpeed = _playerInputHandler.IsSprinting ? SprintSpeed : WalkingSpeed;

            if (_playerInputHandler.MovementVector == Vector2.zero) targetSpeed = 0.0f;

            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _playerInputHandler.IsMovingAnalog ? _playerInputHandler.MovementVector.magnitude : 1f;

            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _moveSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.fixedDeltaTime * SpeedChangeRate);

                _moveSpeed = Mathf.Round(_moveSpeed * 1000f) / 1000f;
            }
            else
            {
                _moveSpeed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.fixedDeltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            Vector3 inputDirection = new Vector3(_playerInputHandler.MovementVector.x, 0.0f, _playerInputHandler.MovementVector.y).normalized;

            float rotation = 0.0f;

            if (_playerInputHandler.MovementVector != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            }

            //if (_isStrafing)
            //{
            //    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;

            //    rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _cinemachineTargetYaw, ref _rotationVelocity, RotationSmoothTime);
            //}
            //else
            //{
            //}

            rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            _controller.Move(targetDirection.normalized * (_moveSpeed * Time.fixedDeltaTime) +
                              new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.fixedDeltaTime);

            _verticalBlend = Mathf.Clamp(Mathf.Lerp(_verticalBlend, _playerInputHandler.MovementVector.y, 5f * Time.fixedDeltaTime), -1, 1);
            _horizontalBlend = Mathf.Clamp(Mathf.Lerp(_horizontalBlend, _playerInputHandler.MovementVector.x, 5f * Time.fixedDeltaTime), -1, 1);


            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDMotionSpeed, _animationBlend);
                //_animator.SetFloat(_animIDSpeed, _animationBlend);
                //_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);

                //_animator.SetFloat(_animIDVertical, _verticalBlend);
                //_animator.SetFloat(_animIDHorizontal, _horizontalBlend);
            }
        }

        private void JumpAndGravity()
        {
            if (_isGrounded)
            {
                _fallTimeoutDelta = FallTimeout;

                if (_hasAnimator)
                {
                    //_animator.SetBool(_animIDJump, false);
                    //_animator.SetBool(_animIDFreeFall, false);
                }

                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                if (_playerInputHandler.IsJumping && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    if (_hasAnimator)
                    {
                        //_animator.SetBool(_animIDJump, true);
                    }
                }

                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.fixedDeltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = JumpTimeout;

                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.fixedDeltaTime;
                }
                else
                {
                    if (_hasAnimator)
                    {
                        //_animator.SetBool(_animIDFreeFall, true);
                    }
                }

                _playerInputHandler.IsJumping = false;
            }

            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.fixedDeltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (_isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }
    }
}