using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private string _levelName;
    [SerializeField] private FadePanel _fadePanel;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => StartCoroutine(LoadNextLevel()));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => StartCoroutine(LoadNextLevel()));
    }

    private IEnumerator LoadNextLevel()
    {
        _fadePanel.BecomeFullyFaded();

        yield return new WaitForSeconds(_fadePanel.AnimationTime);

        SceneManager.LoadScene(_levelName);
    }
}
