using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        public Vector2 SurfaceNormal { get; private set; }
        
        [Header("Run")]
        [SerializeField] private float initialSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private Vector2 raycastVector;
        [SerializeField] private LayerMask raycastLayer;
        
        public UnityEvent onRun;
        
        [Header("Jump")] 
        [SerializeField] private float jumpHeight = 5;
        [SerializeField] private float gravityScale = 5;
        public UnityEvent onJump;
        

        private Transform _transform;
        private Vector2 _moveVector;
        private float _currentSpeed;
        private float _gravityVelocity;
        private bool _isGrounded;
        
        private void Awake()
        {
            _currentSpeed = initialSpeed;
            _transform = transform;
        }

        private void Start()
        {
            StartCoroutine(IncreaseSpeedOverTime());
        }

        private void Update()
        {
            Move();
            Jump();
        }
        
        private void FixedUpdate()
        {
            AdjustGravity();
        }
        
        private Vector2 GetSurfacePosition()
        {
            var surfaceHit = Physics2D.Raycast(
                transform.position
                , Vector2.down
                , 4f
                , raycastLayer
            );
            
            return surfaceHit.point + Vector2.up * _transform.localScale.y / 2f;
        }

        private void MoveToSurface(Vector2 surfacePosition)
        {
            var position = _transform.position;
            position.y = surfacePosition.y;
            _transform.position = position;
        }

        private void AdjustGravity()
        {
            if (_isGrounded) { return; }

            _gravityVelocity += Physics2D.gravity.y * gravityScale * Time.deltaTime;
            transform.Translate(new Vector2(0, _gravityVelocity) * Time.deltaTime);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.layer == 3)
            {
                _isGrounded = false;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == 3)
            {
                _isGrounded = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 3)
            {
                onRun.Invoke();
                _gravityVelocity = 0;
                MoveToSurface(GetSurfacePosition());
            }
        }

        private void Jump()
        {
            if (!Input.GetMouseButtonDown(0) && !Input.GetButtonDown("Jump")) return;

            if (!_isGrounded) return;
            // TODO: make an input controller
            var position = _transform.position;
                
            position = new Vector3(position.x, position.y + 1, position.z);
            _transform.position = position;
            _gravityVelocity = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * gravityScale));

            onJump.Invoke();

        }

        private void Move()
        {
            SurfaceNormal = _isGrounded ? GetSurfaceNormal(raycastVector, raycastLayer) : Vector2.up;
            _moveVector = _isGrounded ? GetMoveVector(SurfaceNormal) : Vector2.right;
            
            _transform.Translate(_moveVector * (_currentSpeed * Time.deltaTime));
        }

        private Vector2 GetSurfaceNormal(Vector2 vector, LayerMask layer)
        {
            var hit = Physics2D.Raycast(
                transform.position
                , vector
                , _transform.localScale.y * 2f
                , layer
                );
            
            return !hit ? Vector2.up : hit.normal;
        }
        
        private static Vector2 GetMoveVector(Vector2 normal)
        {
            return new Vector2(normal.y, -normal.x);
        }
        
        private IEnumerator IncreaseSpeedOverTime()
        {
            while (_currentSpeed < maxSpeed)
            {
                _currentSpeed += 0.2f * Time.deltaTime;
                yield return null; 
            }
        }
    }
}
