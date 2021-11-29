using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Plate : MonoBehaviour
{
    [SerializeField] private float _flyTime;

    private GameObject _parent;
    private bool _isAbleToMove = true;

    public GameObject Parent => _parent;
    public bool IsAbleToMove => _isAbleToMove;

    private void OnValidate()
    {
        _flyTime = Mathf.Clamp(_flyTime, 0, float.MaxValue);
    }

    public void MoveToPlatesStack(Transform matrix, Vector3 offset)
    {
        transform.SetParent(matrix);
        _parent = matrix.gameObject;

        Vector3 dishPosition = offset + Vector3.zero;
        transform.DOLocalMove(dishPosition, _flyTime);

        TrySetMoveAbility(matrix);
    }
    public void MoveToPlatesStack(Transform matrix, Vector3 offset, Vector3 rotation)
    {
        transform.SetParent(matrix);
        _parent = matrix.gameObject;

        Vector3 dishPosition = offset + Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(dishPosition, _flyTime));

        sequence.Insert(0, transform.DORotate(rotation, _flyTime));

        TrySetMoveAbility(matrix);
    }

    private void TrySetMoveAbility(Transform matrix)
    {
        if (matrix.TryGetComponent(out Shelf shell))
            _isAbleToMove = false;
    }
}
