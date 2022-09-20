using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CardMover : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _selectionOffsetY;
    [Min(0)]
    [SerializeField] private float _movementDuration;
    [Min(0)]
    [SerializeField] private float _movementDurationOnSelection;
    [SerializeField] private Vector3 _offset;

    private RectTransform _rectTransform;

    private void Awake() => _rectTransform = GetComponent<RectTransform>();

    public void MoveWithOffset(Vector3 targetPosition)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOAnchorPos(targetPosition + _offset, _movementDuration));
        sequence.Append(_rectTransform.DOAnchorPos(targetPosition, _movementDuration));
    }

    public void MoveOnSelection()
    {
        float targetPositionY = _rectTransform.localPosition.y + _selectionOffsetY;
        var targetPosition = new Vector2(_rectTransform.localPosition.x, targetPositionY);
        _rectTransform.DOAnchorPos(targetPosition, _movementDurationOnSelection);
    }
}