using System;
using UnityEngine;

public class VictoryState : State
{
    [SerializeField] private Tray _tray;

    public event Action VictoryStateEntered;

    private void OnEnable()
    {
        _tray.gameObject.SetActive(false);

        VictoryStateEntered?.Invoke();
    }
}
