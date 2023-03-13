using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ClaimButton : ResultDisplayElement
{
    [Min(0)]
    [SerializeField] private float _pulsationSpeed;
    [Min(0)]
    [SerializeField] private float _pulsationEndValue;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SdkYandex _sdkYandex;

    private readonly int _loops = -1;

    private void OnEnable()
        => _sdkYandex.Rewarded += OnRewarded;

    private void OnDisable()
        => _sdkYandex.Rewarded -= OnRewarded;

    public override void Appear(float endValue, float duration)
    {
        if (endValue < 0)
            throw new ArgumentOutOfRangeException(nameof(endValue));

        if (duration < 0)
            throw new ArgumentOutOfRangeException(nameof(duration));

        _text.DOFade(endValue, duration);
        GetComponent<Image>().DOFade(endValue, duration);
        Pulsate();
    }

    private void Pulsate()
        => transform.DOScale(_pulsationEndValue, _pulsationSpeed).SetLoops(_loops, LoopType.Yoyo);

    private void OnRewarded() => gameObject.SetActive(false);

}