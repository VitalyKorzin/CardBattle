using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

[RequireComponent(typeof(Image))]
public class Roulette : ResultDisplayElement
{
    [SerializeField] private SdkYandex _sdkYandex;

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

        GetComponent<Image>().DOFade(endValue, duration);
    }

    private void OnRewarded() => gameObject.SetActive(false);
}