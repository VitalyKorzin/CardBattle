using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CoinsIcon : ResultDisplayElement
{
    [Min(0)]
    [SerializeField] private float _pulsationSpeed;
    [Min(0)]
    [SerializeField] private float _pulsationEndValue;

    public override void Appear(float endValue, float duration)
    {
        GetComponent<Image>().DOFade(endValue, duration);
        Pulsate();
    }

    private void Pulsate()
        => transform.DOScale(_pulsationEndValue, _pulsationSpeed).SetLoops(-1, LoopType.Yoyo);
}