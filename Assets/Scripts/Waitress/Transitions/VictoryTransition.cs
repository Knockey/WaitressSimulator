using UnityEngine;

public class VictoryTransition : Transition
{
    [SerializeField] private PlatesAmountWatchdog _platesAmountWatchdog;

    private void OnEnable()
    {
        _platesAmountWatchdog.AllPlatesPlaced += OnAllPlatesPlaced;
    }

    private void OnDisable()
    {
        _platesAmountWatchdog.AllPlatesPlaced -= OnAllPlatesPlaced;
    }

    private void OnAllPlatesPlaced(int value, int maxValue)
    {
        NeedTransit = true;
    }
}
