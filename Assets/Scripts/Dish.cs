using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Dish : MonoBehaviour
{
    private GameObject _parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DishMatrix dish) && _parent != dish.transform.gameObject)
        {
            transform.SetParent(dish.transform);
            _parent = dish.transform.gameObject;
        }
    }
}
