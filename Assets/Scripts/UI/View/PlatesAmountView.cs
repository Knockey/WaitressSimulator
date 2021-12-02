using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlatesAmountView : MonoBehaviour
{
    [SerializeField] private Tray _tray;
    [SerializeField] private Slider _bar;
    [SerializeField] private Image _fill;
    [SerializeField] private TMP_Text _maxValueText;
    [SerializeField] private float _sliderAnimationTime;

    private Gradient _gradient;

    private void Awake()
    {
        SetGradient();
    }

    private void OnEnable()
    {
        _tray.PlatesAmountChanged += OnDishesAmountChanged;
    }

    private void OnDisable()
    {
        _tray.PlatesAmountChanged -= OnDishesAmountChanged;
    }

    private void OnDishesAmountChanged(int value, int maxValue)
    {
        _maxValueText.text = maxValue.ToString();

        float sliderValue = value / (float)maxValue;
        _bar.DOValue(sliderValue, _sliderAnimationTime);
        _fill.color = _gradient.Evaluate(sliderValue);
    }

    private void SetGradient()
    {
        _gradient = new Gradient();

        var colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.green;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        var alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        _gradient.SetKeys(colorKey, alphaKey);
    }
}
