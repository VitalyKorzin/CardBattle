using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class Pointer : ResultDisplayElement
{
    public event UnityAction<int> RewardMultiplied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RewardMultiplier multiplier))
            RewardMultiplied?.Invoke(multiplier.Value);
    }

    public override void Appear(float endValue, float duration) 
        => GetComponent<Image>().DOFade(endValue, duration);
}