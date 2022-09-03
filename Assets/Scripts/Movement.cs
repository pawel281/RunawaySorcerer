using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnHandSpeed;
    [SerializeField] private Transform _handRoot;
    [SerializeField] private SpriteRenderer _bodySprite;
    private Rigidbody _rigidbody;
    private Vector3 movingDirection;

    public float Speed => _speed;
    public void ChangeDirection(Vector3 dir) => movingDirection = dir.normalized;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0, Mathf.Infinity);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + movingDirection.normalized *
            _speed
            * Time.fixedDeltaTime);
    }
    
    public void HandTurn(Vector3 viewDirection)
    {
        if (viewDirection.sqrMagnitude > 0.3f)
        {
            var step = _turnHandSpeed * Time.deltaTime;
            _handRoot.transform.rotation = Quaternion.RotateTowards(_handRoot.transform.rotation,
                Quaternion.LookRotation(viewDirection.normalized) * Quaternion.Euler(90, 0, 0), step);
            _bodySprite.flipX = viewDirection.x < 0;
        }
    }
}