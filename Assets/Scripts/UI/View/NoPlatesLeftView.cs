using TMPro;
using UnityEngine;

public class NoPlatesLeftView : MonoBehaviour
{
    [SerializeField] private PlatesAmountWatchdog _platesAmountWatchdog;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _platesAmountWatchdog.NoPlatesLeft += OnNoPlatesLeft;
        _text.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _platesAmountWatchdog.NoPlatesLeft -= OnNoPlatesLeft;
    }

    private void OnNoPlatesLeft()
    {
        _text.gameObject.SetActive(true);
    }
}
