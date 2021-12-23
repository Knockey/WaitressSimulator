using System;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    [SerializeField] private int _maxDishesAmount;
    [SerializeField] private float _yOffset;
    [SerializeField] private AudioSource _stackPlateSound;
    [SerializeField] private ParticleSystem _placeEffect;

    private Stack<Plate> _plates;
    private Vector3 _currentOffset;
    private int _platesAmount;

    public IReadOnlyCollection<Plate> Dishes => _plates;

    public event Action DishesPoped;
    public event Action<int, int> PlatesAmountChanged;
    public event Action<int> PlatesDropped;

    private void OnValidate()
    {
        _maxDishesAmount = Mathf.Clamp(_maxDishesAmount, 0, int.MaxValue);
    }

    private void Awake()
    {
        _plates = new Stack<Plate>();
        _currentOffset = Vector3.zero;
        _platesAmount = 0;
    }

    private void Start()
    {
        PlatesAmountChanged?.Invoke(_platesAmount, _maxDishesAmount);
    }

    public void DropAllPlates()
    {
        int count = _plates.Count;

        while(_plates.Count > 0)
        {
            Plate plate = _plates.Pop();

            plate.Drop();

            ChangePlatesAmount(-1);
        }

        _currentOffset = Vector3.zero;
        PlatesDropped?.Invoke(count);
    }

    public void PushPlate(Plate dish)
    {
        if (_platesAmount < _maxDishesAmount)
        {
            dish.MoveToPlatesStack(transform, _currentOffset);
            _plates.Push(dish);

            _currentOffset.y += _yOffset;

            _stackPlateSound.Play();

            SpawnPlaceEffect();

            ChangePlatesAmount(1);
        }
    }

    public void TryPopPlate(Shelf shells)
    {
        Plate dish = _plates.Pop();
        _currentOffset.y -= _yOffset;

        shells.PlaceDish(dish);

        CheckStackItemsCount();

        ChangePlatesAmount(-1);
    }

    private void SpawnPlaceEffect()
    {
        Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y + _currentOffset.y, transform.position.z);

        Instantiate(_placeEffect, effectPosition, transform.rotation, transform);
    }

    private void CheckStackItemsCount()
    {
        if (_plates.Count <= 0)
            DishesPoped?.Invoke();
    }

    private void ChangePlatesAmount(int value)
    {
        _platesAmount += value;

        PlatesAmountChanged?.Invoke(_platesAmount, _maxDishesAmount);
    }
}
