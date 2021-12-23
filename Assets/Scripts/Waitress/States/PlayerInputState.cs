using System;
using UnityEngine;

public class PlayerInputState : State
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private WaitressAnimation _waitressAnimation;

    public event Action<Vector3> StickMoved;

    private void OnEnable()
    {
        _waitressAnimation.RestoreInitialPositionAndRotation();
    }

    private void Update()
    {
        GetStickDirection();
    }

    private void GetStickDirection()
    {
        Vector3 direction = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;

        StickMoved?.Invoke(direction);
    }
}
