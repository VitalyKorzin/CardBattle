using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class PointerRotator : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _rotationDuration;
    [SerializeField] private Vector3 _targetRotation;
    [SerializeField] private SdkYandex _sdkYandex;

    private RectTransform _rectTransform;
    private Vector3 _startRotation;
    private Sequence _sequence;

    private void OnEnable()
    {
        _sdkYandex.VideoAdOpened += OnVideoAdOpend;
        Rotate();
    }

    private void OnDisable() 
        => _sdkYandex.VideoAdOpened -= OnVideoAdOpend;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startRotation = _rectTransform.localEulerAngles;
    }

    public void Rotate()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(_rectTransform.DOLocalRotate(_targetRotation, _rotationDuration).SetEase(Ease.Linear));
        _sequence.Append(_rectTransform.DOLocalRotate(_startRotation, _rotationDuration).SetEase(Ease.Linear));
        _sequence.SetLoops(-1);

    }

    private void OnVideoAdOpend() => _sequence.Kill();
}