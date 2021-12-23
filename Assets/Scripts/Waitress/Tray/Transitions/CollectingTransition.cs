using UnityEngine;

[RequireComponent(typeof(Tray))]
public class CollectingTransition : Transition
{
    private Tray _matrix;

    private void Awake()
    {
        _matrix = GetComponent<Tray>();
    }

    private void Update()
    {
        if (_matrix.Dishes.Count <= 0)
            NeedTransit = true;
    }
}
