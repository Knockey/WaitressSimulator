using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    public event Action<Vector3> StickMoved;

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
