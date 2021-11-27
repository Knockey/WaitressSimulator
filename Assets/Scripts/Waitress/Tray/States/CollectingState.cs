using UnityEngine;

[RequireComponent(typeof(Tray))]
public class CollectingState : State
{
    private Tray _matrix;

    private void Awake()
    {
        _matrix = GetComponent<Tray>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Dish dish) && dish.Parent != gameObject && dish.IsAbleToMove)
        {
            _matrix.PushDish(dish);
        }
    }
}
