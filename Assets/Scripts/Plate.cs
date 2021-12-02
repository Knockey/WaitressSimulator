using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Plate : MonoBehaviour
{
    [SerializeField] private float _flyTime;
    [SerializeField] private float _dropOffset;
    [SerializeField] private float _dropTime;
    [SerializeField] private float _dropSoundDelay;
    [SerializeField] AudioSource _dropSound;
    [SerializeField] private ParticleSystem _poofEffect;

    private GameObject _parent;
    private bool _isAbleToMove = true;

    public GameObject Parent => _parent;
    public bool IsAbleToMove => _isAbleToMove;

    private void OnValidate()
    {
        _flyTime = Mathf.Clamp(_flyTime, 0, float.MaxValue);
        _dropTime = Mathf.Clamp(_dropTime, 0, float.MaxValue);
        _dropSoundDelay = Mathf.Clamp(_dropSoundDelay, 0, _dropTime - 0.1f);
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

    public void Drop()
    {
        var fallPosition = new Vector3(Random.Range(transform.position.x - _dropOffset, 
            transform.position.x + _dropOffset), 0f, 
            Random.Range(transform.position.z - _dropOffset, transform.position.x + _dropOffset));

        transform.DOMove(fallPosition, _dropTime);

        StartCoroutine(PlayDropEffects());

        Destroy(gameObject, _dropTime);
    }

    private void TrySetMoveAbility(Transform matrix)
    {
        if (matrix.TryGetComponent(out Shelf shell))
            _isAbleToMove = false;
    }

    private IEnumerator PlayDropEffects()
    {
        yield return new WaitForSeconds(_dropSoundDelay);

        Instantiate(_poofEffect, transform.position, transform.rotation);
        _dropSound.Play();
    }
}
