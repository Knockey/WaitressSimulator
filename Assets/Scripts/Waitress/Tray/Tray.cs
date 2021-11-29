using System;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    [SerializeField] private int _maxDishesAmount;
    [SerializeField] private float _yOffset;
    [SerializeField] private AudioSource _stackPlateSound;

    private Stack<Plate> _dishes;
    private Vector3 _currentOffset;
    private int _dishesAmount;

    public IReadOnlyCollection<Plate> Dishes => _dishes;

    public event Action DishesPoped;
    public event Action<int, int> PlatesAmountChanged;

    private void OnValidate()
    {
        _maxDishesAmount = Mathf.Clamp(_maxDishesAmount, 0, int.MaxValue);
    }

    private void Awake()
    {
        _dishes = new Stack<Plate>();
        _currentOffset = Vector3.zero;
        _dishesAmount = 0;
    }

    private void Start()
    {
        PlatesAmountChanged?.Invoke(_dishesAmount, _maxDishesAmount);
    }

    public void PushPlate(Plate dish)
    {
        if (_dishesAmount < _maxDishesAmount)
        {
            dish.MoveToPlatesStack(transform, _currentOffset);
            _dishes.Push(dish);

            _currentOffset.y += _yOffset;

            _stackPlateSound.Play();

            ChangePlatesAmount(1);
        }
    }

    public void TryPopPlate(Shelf shells)
    {
        Plate dish = _dishes.Pop();
        _currentOffset.y -= _yOffset;

        shells.PlaceDish(dish);

        CheckStackItemsCount();

        ChangePlatesAmount(-1);
    }

    private void CheckStackItemsCount()
    {
        if (_dishes.Count <= 0)
            DishesPoped?.Invoke();
    }

    private void ChangePlatesAmount(int value)
    {
        _dishesAmount += value;

        PlatesAmountChanged(_dishesAmount, _maxDishesAmount);
    }
}
