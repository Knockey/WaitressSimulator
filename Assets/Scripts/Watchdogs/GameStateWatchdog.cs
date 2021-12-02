using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWatchdog : MonoBehaviour
{
    [SerializeField] private ConfettiEffect _confetties;
    [SerializeField] private EndScreenView _endScreen;
    [SerializeField] private PlatesAmountWatchdog _platesWatchdog;
    [SerializeField] private NoPlatesLeftView _noPlatesLeftView;
    [SerializeField] private List<Star> _stars;
    [SerializeField] private float _oneStarPlatesPercent;
    [SerializeField] private float _twoStarsPlatesPercent;
    [SerializeField] private float _timeBetweenShowStarDelay;
    [SerializeField] private AudioSource _stampStarSound;

    private void OnValidate()
    {
        _oneStarPlatesPercent = Mathf.Clamp(_oneStarPlatesPercent, 0, float.MaxValue);
        _twoStarsPlatesPercent = Mathf.Clamp(_twoStarsPlatesPercent, 0, float.MaxValue);
        _timeBetweenShowStarDelay = Mathf.Clamp(_timeBetweenShowStarDelay, 0, float.MaxValue);
    }

    private void OnEnable()
    {
        _platesWatchdog.AllPlatesPlaced += OnAllPlatesPlaced;
    }

    private void OnDisable()
    {
        _platesWatchdog.AllPlatesPlaced -= OnAllPlatesPlaced;
    }

    private void OnAllPlatesPlaced(int value, int maxValue)
    {
        _noPlatesLeftView.gameObject.SetActive(false);
        _confetties.gameObject.SetActive(true);
        _endScreen.gameObject.SetActive(true);

        int starsCount = GetStarsCount(value, maxValue);

        if (starsCount == 0)
            return;

        for (int i = 0; i < starsCount; i++)
        {
            StartCoroutine(ShowStar(_stars[i], i));
        }
    }

    private int GetStarsCount(int value, int maxValue)
    {
        float percent = (value / (float)maxValue) * 100;

        if (percent > _twoStarsPlatesPercent)
            return _stars.Count;

        if (percent > _oneStarPlatesPercent)
            return _stars.Count - 1;

        if (percent > 0)
            return _stars.Count - 2;

        return 0;
    }

    private IEnumerator ShowStar(Star star, int delay)
    {
        yield return new WaitForSeconds(_timeBetweenShowStarDelay * delay);

        ShowStar(star);
    }

    private void ShowStar(Star star)
    {
        star.gameObject.SetActive(true);
        _stampStarSound.Play();
    }
}
