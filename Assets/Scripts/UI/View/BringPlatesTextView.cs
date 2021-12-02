using TMPro;
using UnityEngine;

public class BringPlatesTextView : MonoBehaviour
{
    [SerializeField] private Tray _tray;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _platesPercentWhenShow;

    private void OnValidate()
    {
        _platesPercentWhenShow = Mathf.Clamp(_platesPercentWhenShow, 0, 100);
    }

    private void OnEnable()
    {
        _tray.PlatesAmountChanged += OnPlatesAmountChanged;
    }

    private void OnDisable()
    {
        _tray.PlatesAmountChanged -= OnPlatesAmountChanged;
    }

    private void OnPlatesAmountChanged(int value, int maxValue)
    {
        float percent = (value / (float)maxValue) * 100;

        if (percent > _platesPercentWhenShow)
        {
            _text.gameObject.SetActive(true);
            return;
        }

        _text.gameObject.SetActive(false);
    }
}
