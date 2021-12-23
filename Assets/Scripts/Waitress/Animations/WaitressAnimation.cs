using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RigBuilder))]
public class WaitressAnimation : MonoBehaviour
{
    [SerializeField] private WaitressMovement _movement;
    [SerializeField] private FallState _fall;
    [SerializeField] private VictoryState _victory;

    private RigBuilder _rigBuilder;
    private Animator _animator;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    public event Action FallAnimationCompleted;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigBuilder = GetComponent<RigBuilder>();

        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        _movement.WaitressMovedOnDistance += OnWaitressMovedOnDistance;
        _fall.WaitressFelt += OnWaitressFelt;
        _victory.VictoryStateEntered += OnVictoryStateEntered;
    }

    private void OnDisable()
    {
        _movement.WaitressMovedOnDistance -= OnWaitressMovedOnDistance;
        _fall.WaitressFelt -= OnWaitressFelt;
        _victory.VictoryStateEntered -= OnVictoryStateEntered;
    }

    public void InvokeFallAnimationCompletedEvent()
    {
        _animator.SetFloat(WaitressTypedAnimations.Speed, 0f);
        FallAnimationCompleted?.Invoke();
    }

    public void RestoreInitials()
    {
        transform.localPosition = _initialPosition;
        transform.localRotation = _initialRotation;

        _rigBuilder.enabled = true;
    }

    private void OnWaitressMovedOnDistance(float distance)
    {
        _animator.SetFloat(WaitressTypedAnimations.Speed, distance);
    }

    private void OnVictoryStateEntered()
    {
        _rigBuilder.enabled = false;
        _animator.Play(WaitressTypedAnimations.Dance);
    }

    private void OnWaitressFelt()
    {
        _rigBuilder.enabled = false;
        _animator.Play(WaitressTypedAnimations.Fall);
    }
}
