using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WaitressAnimation : MonoBehaviour
{
    [SerializeField] private WaitressMovement _movement;
    [SerializeField] private VictoryState _victory;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _movement.WaitressMovedOnDistance += OnWaitressMovedOnDistance;
        _victory.VictoryStateEntered += OnVictoryStateEntered;
    }

    private void OnDisable()
    {
        _movement.WaitressMovedOnDistance -= OnWaitressMovedOnDistance;
        _victory.VictoryStateEntered -= OnVictoryStateEntered;
    }

    private void OnWaitressMovedOnDistance(float distance)
    {
        _animator.SetFloat(WaitressTypedAnimations.Speed, distance);
    }

    private void OnVictoryStateEntered()
    {
        _animator.Play(WaitressTypedAnimations.Dance);
    }
}
