using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class PlayerAbilityView : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _viewChangeDuration;
    [SerializeField] private Vector3 _targetSizeOnSelection;
    [SerializeField] private Image _glow;

    private readonly float _appearanceEndValue = 1f;
    private readonly float _fadingEndValue = 0f;

    private Vector3 _defaultSize;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultSize = _rectTransform.localScale;
    }

    public void Select()
    {
        _glow.DOFade(_appearanceEndValue, _viewChangeDuration);
        _rectTransform.DOScale(_targetSizeOnSelection, _viewChangeDuration);
    }

    public void Deselect()
    {
        _glow.DOFade(_fadingEndValue, _viewChangeDuration);
        _rectTransform.DOScale(_defaultSize, _viewChangeDuration);
    }
}