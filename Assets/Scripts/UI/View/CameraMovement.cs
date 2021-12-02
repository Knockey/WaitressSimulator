using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private WaitressMovement _player;
    [SerializeField] private float _speed;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0, float.MaxValue);
    }

    private void OnEnable()
    {
        _player.WaitressMoved += OnStickMoved;
    }

    private void OnDisable()
    {
        _player.WaitressMoved -= OnStickMoved;
    }

    private void OnStickMoved(Vector3 direction)
    {
        Vector3 nextPosition = transform.position + _speed * direction * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, _speed * Time.deltaTime);
    }
}
