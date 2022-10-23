using System;
using Controllers;
using UnityEngine;

namespace Player
{
    public class CatMovement : MonoBehaviour
    {
        private ControllerManager _cm;

        [Header("Movement")]
        public float moveSpeed;
        public float groundDrag;
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        private bool _readyToJump;

        [Header("Keybindings")]
        public KeyCode jumpKey = KeyCode.Space;

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask groundObjects;
        private bool _grounded;
        private float _lastY;

        [Header("Wall Climbing")]
        public LayerMask wallObjects;
        public float climbSpeed;
        private bool _isClimbing;
        public float wallDetectionLength;
        public float wallSphereCastRad;
        public float maxWallLookAngle;
        private float _currWallLookAngle;
        private RaycastHit _wallHit;
        private bool _wallInFront;

        [Header("General")]
        [SerializeField] private Transform catOrientation;
        [SerializeField] private Rigidbody rb;

        private float _horizontalInput;
        private float _verticalInput;
        private Vector3 _moveDirection;

        private void Start()
        {
            _cm = FindObjectOfType<ControllerManager>();
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;

            _readyToJump = true;
        }

        private void Update()
        {
            if (!_cm.GameController.GameRunning()) return;

            // Check if on the ground or not
            _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundObjects);

            GetInputs();
            SpeedControl();

            // Handle physics drag
            if (_grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        private void FixedUpdate()
        {
            if (!_cm.GameController.GameRunning()) return;

            CheckForWall();
            ClimbingStateMachine();

            if (_isClimbing)
            {
                ClimbingMovement();
            }
            else
            {
                MovePlayer();
            }
        }

        private void ClimbingStateMachine()
        {
            if (_wallInFront && Input.GetKey(KeyCode.W) && _currWallLookAngle <= maxWallLookAngle)
            {
                if (!_isClimbing)
                {
                    StartClimbing();
                }
            }
            else
            {
                StopClimbing();
            }
        }

        private void MovePlayer()
        {
            // Calculate direction
            _moveDirection = catOrientation.forward * _verticalInput + catOrientation.right * _horizontalInput;

            // Move on ground
            if (_grounded)
                rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);

            // Move in the air
            else if (!_grounded)
                rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
        }

        private void CheckForWall()
        {
            Vector3 forwardVec = catOrientation.forward;
            _wallInFront = Physics.SphereCast(transform.position, wallSphereCastRad, forwardVec,
                out _wallHit, wallDetectionLength, layerMask: wallObjects);
            _currWallLookAngle = Vector3.Angle(forwardVec, -_wallHit.normal);

            if (_cm.GameController.debugMode) Debug.Log("Wall in front: " + _wallInFront);
        }

        private void SpeedControl()
        {
            Vector3 velocity = rb.velocity;
            Vector3 flatVelocity = new(velocity.x, 0f, velocity.z);

            // Limit velocity 
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis (not expensive as only called on input)
        private void GetInputs()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");

            // Jump, if possible
            if (Input.GetKey(jumpKey) && _readyToJump && _grounded)
            {
                _readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void Jump()
        {
            // reset Y velocity
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(velocity.x, 0f, velocity.z);
            rb.velocity = velocity;

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            _readyToJump = true;
        }

        private void StartClimbing()
        {
            _isClimbing = true;
            _lastY = transform.position.y;
        }

        private void ClimbingMovement()
        {
            Vector3 velocity = rb.velocity;
            velocity = new Vector3(velocity.x, climbSpeed, velocity.z);
            rb.velocity = velocity;
        }

        private void StopClimbing()
        {
            if (_isClimbing)
            {
                var climbDistance = (int) Math.Abs(_lastY - transform.position.y) * 10;
                _cm.Achievements.ClimbDistance(climbDistance);
                if (_cm.GameController.debugMode)
                {
                    Debug.Log("Stop climbing");
                    Debug.Log("You climbed " + climbDistance + "cm");
                }
            }

            _isClimbing = false;
        }
    }
}