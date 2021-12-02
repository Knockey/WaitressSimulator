using UnityEngine;

public class PlayerInputTransition : Transition
{
    [SerializeField] private GameObject _trayModel;
    [SerializeField] private WaitressAnimation _animations;

    private void OnEnable()
    {
        NeedTransit = false;

        _animations.FallAnimationCompleted += OnFallAnimationCompleted;
    }

    private void OnDisable()
    {
        _animations.FallAnimationCompleted -= OnFallAnimationCompleted;
    }

    private void OnFallAnimationCompleted()
    {
        _trayModel.SetActive(true);
        NeedTransit = true;
    }
}
