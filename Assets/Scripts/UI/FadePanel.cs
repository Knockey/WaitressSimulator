using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadePanel : MonoBehaviour
{
    [SerializeField] private float _absoluteFadeValue;
    [SerializeField] private float _animationTime;

    public float AnimationTime => _animationTime;

    private Image _image;
    private void OnValidate()
    {
        _absoluteFadeValue = Mathf.Clamp(_absoluteFadeValue, 0, 1);
        _animationTime = Mathf.Clamp(_animationTime, 0, float.MaxValue);
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void BecomeFullyFaded()
    {
        _image.raycastTarget = true;
        _image.DOFade(_absoluteFadeValue, _animationTime);
    }
}