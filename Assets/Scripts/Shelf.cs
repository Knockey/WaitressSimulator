using System;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Vector3 _plateRotation;
    [SerializeField] private Transform _initialPlatePosition;
    [SerializeField] private int _platesAmountInRow;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private AudioSource _placePlateSound;

    private int _currentDishIndexInRow;
    private Vector3 _currentPlatePosition;
    private int _platesAmount;

    public event Action PlatesAmountChanged;

    private void OnValidate()
    {
        _platesAmountInRow = Mathf.Clamp(_platesAmountInRow, 0, int.MaxValue);
    }

    private void Awake()
    {
        _currentDishIndexInRow = 0;
        _currentPlatePosition = _initialPlatePosition.position;
        _platesAmount = 0;
    }

    public void PlaceDish(Plate dish)
    {
        dish.MoveToPlatesStack(transform, _currentPlatePosition, _plateRotation);

        _placePlateSound.Play();

        _platesAmount++;
        PlatesAmountChanged?.Invoke();

        SetNewDishPosition();
    }

    private void SetNewDishPosition()
    {
        _currentDishIndexInRow++;

        if (_currentDishIndexInRow > _platesAmountInRow - 1)
        {
            _currentPlatePosition.x = _initialPlatePosition.position.x;
            _currentPlatePosition.y -= _offset.y;
            _currentDishIndexInRow = 0;
            return;
        }

        _currentPlatePosition.x -= _offset.x;
    }
}
