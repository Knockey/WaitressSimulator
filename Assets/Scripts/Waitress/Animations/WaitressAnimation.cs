using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WaitressAnimation : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
        _animator.SetFloat(WaitressTypedAnimations.Speed, direction.magnitude);
    }
}
