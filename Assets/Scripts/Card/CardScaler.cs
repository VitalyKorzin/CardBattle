using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CardScaler : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _sizeOnSelection;
    [Min(0)]
    [SerializeField] private float _targetScaleOnFading;
    [Min(0)]
    [SerializeField] private float _scalingDuration;
    [Min(0)]
    [SerializeField] private float _fadingDuration;

    private readonly float _startSize = 1f;

    private RectTransform _rectTransform;

    private void Awake() 
        => _rectTransform = GetComponent<RectTransform>();

    public void ChangeSize()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(_sizeOnSelection, _scalingDuration));
        sequence.Append(transform.DOScale(_startSize, _scalingDuration));
    }

    public void Fade() 
        => _rectTransform.DOScale(_targetScaleOnFading, _fadingDuration);
}