using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class WaitressMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private PlayerInput _input;

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

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, _speed * Time.deltaTime);

        TryLookTowardsDirection(direction);
    }

    private void TryLookTowardsDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction * -1);
    }
}
