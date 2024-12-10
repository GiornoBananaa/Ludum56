using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartCutscene : MonoBehaviour
{
    private const string SHOWED_COMICS_PROPERTY = "ShowedComics";
    [SerializeField] private RectTransform _panel;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _timeForSprite;
    
    private CancellationTokenSource _cancellation;
    public UnityEvent OnCutsceneEnd;

    private void Start()
    {
        if(PlayerPrefs.GetInt(SHOWED_COMICS_PROPERTY, 0) == 0)
            LaunchCutscene();
    }

    private void OnDestroy()
    {
        _cancellation?.Cancel();
    }
    
    public void LaunchCutscene()
    {
        _cancellation?.Dispose();
        _cancellation = new CancellationTokenSource();
        Cutscene(_cancellation.Token);
        PlayerPrefs.SetInt(SHOWED_COMICS_PROPERTY, 1);
        PlayerPrefs.Save();
    }
    
    private async UniTask Cutscene(CancellationToken cancellationToken)
    {
        Time.timeScale = 0;
        _panel.gameObject.SetActive(true);
        foreach (var sprite in _sprites)
        {
            _image.sprite = sprite;
            await UniTask.WaitForSeconds(_timeForSprite, true, cancellationToken: cancellationToken);
        }
        _panel.gameObject.SetActive(false);
        OnCutsceneEnd?.Invoke();
        Time.timeScale = 1f;
    }
}