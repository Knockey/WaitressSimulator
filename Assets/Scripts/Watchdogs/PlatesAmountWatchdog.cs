using System;
using UnityEngine;

public class PlatesAmountWatchdog : MonoBehaviour
{
    [SerializeField] private int _totalPlatesAmount;
    [SerializeField] private Tray _tray;
    [SerializeField] private Shelf _shelf;
    [SerializeField] private PopState _trayPopState;

    private int _remainToPlacePlatesAmount;
    private int _placedPlatesAmount;

    public event Action<int, int> AllPlatesPlaced;
    public event Action NoPlatesLeft;

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
        _tray.PlatesDropped += OnPlatesDropped;
        _trayPopState.PlatesPlaced += OnPlatesPlaced;
    }

    private void OnDisable()
    {
        _shelf.PlatesAmountChanged -= OnPlatesAmountChanged;
        _tray.PlatesDropped -= OnPlatesDropped;
        _trayPopState.PlatesPlaced -= OnPlatesPlaced;
    }

    private void OnPlatesPlaced()
    {
        CheckPlacedPlatesAmount();
    }

    private void OnPlatesAmountChanged()
    {
        _placedPlatesAmount++;
        _remainToPlacePlatesAmount--;

        CheckPlacedPlatesAmount();
    }

    private void CheckPlacedPlatesAmount()
    {
        if (_remainToPlacePlatesAmount <= 0)
            AllPlatesPlaced?.Invoke(_placedPlatesAmount, _totalPlatesAmount);
    }

    private void OnPlatesDropped(int amount)
    {
        _remainToPlacePlatesAmount -= amount;

        if (_remainToPlacePlatesAmount <= 0)
            NoPlatesLeft?.Invoke();
    }
}
