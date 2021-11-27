using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class WaitressMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _obstacleLayerMask;

    private PlayerInput _input;

    public event Action<Vector3> WaitressMoved;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0, float.MaxValue);
        _raycastDistance = Mathf.Clamp(_raycastDistance, 0, float.MaxValue);
    }

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _input.StickMoved += OnStickMoved;
    }

    private void OnDisable()
    {
        _input.StickMoved -= OnStickMoved;
    }

    private void OnStickMoved(Vector3 direction)
    {
        Vector3 nextPosition = transform.position + _speed * direction * Time.deltaTime;
        bool isBoxHit = false;

        if (direction != Vector3.zero)
        {
            isBoxHit = Physics.BoxCast(transform.position, transform.localScale / 100f, direction, out RaycastHit hit, transform.rotation, _raycastDistance, _obstacleLayerMask);
        }

        if (isBoxHit == false)
        {
            WaitressMoved?.Invoke(direction);
            transform.position = nextPosition;
        }

        TryLookTowardsDirection(direction);
    }

    private void TryLookTowardsDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction * -1);
    }
}
