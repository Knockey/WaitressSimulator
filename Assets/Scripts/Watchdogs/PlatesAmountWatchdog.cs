using System;
using UnityEngine;

public class PlatesAmountWatchdog : MonoBehaviour
{
    [SerializeField] private int _totalPlatesAmount;
    [SerializeField] private Shelf _shelf;

    private int _remainToPlacePlatesAmount;
    private int _placedPlatesAmount;


    public event Action<int, int> AllPlatesPlaced;

    private void Awake()
    {
        _remainToPlacePlatesAmount = _totalPlatesAmount;
        _placedPlatesAmount = 0;
    }

    private void OnValidate()
    {
        _totalPlatesAmount = Mathf.Clamp(_totalPlatesAmount, 0, int.MaxValue);
    }

    private void OnEnable()
    {
        _shelf.PlatesAmountChanged += OnPlatesAmountChanged;
    }

    private void OnDisable()
    {
        _shelf.PlatesAmountChanged -= OnPlatesAmountChanged;
    }

    private void OnPlatesAmountChanged(int value)
    {
        _placedPlatesAmount 
            = value;

        CheckPlacedPlatesAmount();
    }

    private void CheckPlacedPlatesAmount()
    {
        if (_placedPlatesAmount >= _remainToPlacePlatesAmount)
            AllPlatesPlaced?.Invoke(_placedPlatesAmount, _totalPlatesAmount);
    }
}
