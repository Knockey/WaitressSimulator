using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Tray))]
public class PopState : State
{
    [SerializeField] private Shelf _shells;
    [SerializeField] private float _timeBetweenPop;

    private Tray _matrix;
    private bool _isAbleToPop;

    private void OnValidate()
    {
        _timeBetweenPop = Mathf.Clamp(_timeBetweenPop, 0, float.MaxValue);
    }

    private void Awake()
    {
        _matrix = GetComponent<Tray>();
    }

    private void OnEnable()
    {
        _isAbleToPop = true;
    }

    private void OnDisable()
    {
        StopCoroutine(Timer());
    }

    private void Update()
    {
        if (_isAbleToPop && _matrix.Dishes.Count > 0)
        {
            _matrix.PopDish(_shells);
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        _isAbleToPop = false;

        yield return new WaitForSeconds(_timeBetweenPop);

        _isAbleToPop = true;
    }
}
