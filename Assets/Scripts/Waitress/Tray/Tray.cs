using System;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    [SerializeField] private float _yOffset;

    private Stack<Dish> _dishes;
    private Vector3 _currentOffset;

    public IReadOnlyCollection<Dish> Dishes => _dishes;

    public event Action DishesPoped;

    private void Awake()
    {
        _dishes = new Stack<Dish>();
        _currentOffset = Vector3.zero;
    }

    public void PushDish(Dish dish)
    {
        dish.MoveToDishMatrix(transform, _currentOffset);
        _dishes.Push(dish);

        _currentOffset.y += _yOffset;
    }

    public void PopDish(Shelf shells)
    {
        Dish dish = _dishes.Pop();
        _currentOffset.y -= _yOffset;

        shells.PlaceDish(dish);

        CheckStackItemsCount();
    }

    private void CheckStackItemsCount()
    {
        if (_dishes.Count <= 0)
            DishesPoped?.Invoke();
    }
}
