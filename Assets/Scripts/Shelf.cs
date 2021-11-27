using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Vector3 dishRotation;
    [SerializeField] private Transform _initialDishPosition;
    [SerializeField] private int _dishAmountInRow;
    [SerializeField] private Vector2 _offset;

    private int _currentDishIndexInRow;
    private Vector3 _currentDishPosition;

    private void OnValidate()
    {
        _dishAmountInRow = Mathf.Clamp(_dishAmountInRow, 0, int.MaxValue);
    }

    private void Awake()
    {
        _currentDishIndexInRow = 0;
        _currentDishPosition = _initialDishPosition.position;
    }

    public void PlaceDish(Dish dish)
    {
        dish.MoveToDishMatrix(transform, _currentDishPosition, dishRotation);
        SetNewDishPosition();
    }

    private void SetNewDishPosition()
    {
        _currentDishIndexInRow++;

        if (_currentDishIndexInRow > _dishAmountInRow - 1)
        {
            _currentDishPosition.x = _initialDishPosition.position.x;
            _currentDishPosition.y -= _offset.y;
            _currentDishIndexInRow = 0;
            return;
        }

        _currentDishPosition.x -= _offset.x;
    }
}
