using System;
using UnityEngine;

public class FallState : State
{
    [SerializeField] private Tray _tray;
    [SerializeField] private GameObject _trayModel;

    public event Action WaitressFelt;

    private void OnEnable()
    {
        _tray.DropAllPlates();
        _trayModel.SetActive(false);

        WaitressFelt?.Invoke();
    }
}
