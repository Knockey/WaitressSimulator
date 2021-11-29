using System.Collections;
using TMPro;
using UnityEngine;

public class ObjectiveTextView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _timeBeforeDisable;

    private void OnEnable()
    {
        StartCoroutine(StartTimerBeforeDisable());
    }

    private IEnumerator StartTimerBeforeDisable()
    {
        yield return new WaitForSeconds(_timeBeforeDisable);

        _text.gameObject.SetActive(false);
    }
}
