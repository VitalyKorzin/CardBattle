using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text), typeof(Button))]
public class NextButton : ResultDisplayElement
{
    [Min(0)]
    [SerializeField] private int _score;
    [SerializeField] private ScoreSaver _saver;

    private Button _button;

    private void OnEnable()
        => _button.onClick.AddListener(OnButtonClick);

    private void OnDisable()
        => _button.onClick.RemoveListener(OnButtonClick);

    private void Awake()
        => _button = GetComponent<Button>();

    public override void Appear(float endValue, float duration)
    {
        if (endValue < 0)
            throw new ArgumentOutOfRangeException(nameof(endValue));

        if (duration < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        GetComponent<TMP_Text>().DOFade(endValue, duration);
    }

    private void OnButtonClick()
    {
        int currentScore = _saver.LoadScore();
        _saver.Save(currentScore + _score);
    }
}