using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputState))]
public class WaitressMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _obstacleLayerMask;

    private PlayerInputState _input;
    private Vector3 _previousPosition;

    public event Action<Vector3> WaitressMoved;
    public event Action<float> WaitressMovedOnDistance;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0, float.MaxValue);
        _raycastDistance = Mathf.Clamp(_raycastDistance, 0, float.MaxValue);
    }

    private void Awake()
    {
        _input = GetComponent<PlayerInputState>();
        _previousPosition = transform.position;
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
            transform.position = nextPosition;
            WaitressMoved?.Invoke(direction);
        }

        TryUpdatePreviousPosition();

        TryLookTowardsDirection(direction);
    }

    private void TryUpdatePreviousPosition()
    {
        float distance = Vector3.Distance(transform.position, _previousPosition);

        WaitressMovedOnDistance?.Invoke(distance);

        _previousPosition = transform.position;
    }

    private void TryLookTowardsDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction * -1);
    }
}
