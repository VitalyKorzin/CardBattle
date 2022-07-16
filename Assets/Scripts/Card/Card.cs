using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public abstract class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Min(0)]
    [SerializeField] private float _selectionOffsetY = 250f;
    [Min(0)]
    [SerializeField] private float _colorChangeDuration = 0.7f;
    [Min(0)]
    [SerializeField] private float _movementDuration = 0.35f;
    [Min(0)]
    [SerializeField] private float _fadingDuration = 0.2f;
    [Min(0)]
    [SerializeField] private float _delayBeforeDestroy = 0.2f;
    [Min(0)]
    [SerializeField] private float _rotationDuration = 0.6f;
    [Min(0)]
    [SerializeField] private float _sizeOnSelection = 0.9f;
    [Min(0)]
    [SerializeField] private float _scalingDuration = 0.15f;
    [Min(0)]
    [SerializeField] private float _movementDurationOnSelection = 0.6f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 90f, 0);
    [SerializeField] private Color _selectionColor;
    [SerializeField] private TMP_Text _text;

    private readonly float _fadingEndValue = 0f;
    private readonly float _startSize = 1f;

    private RectTransform _rectTransform;
    private Image _image;

    public event UnityAction<Card> Selected;
    public event UnityAction<Card> Destroyed;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _rectTransform.localRotation = transform.parent.localRotation;
    }

    public void Use(List<Stickman> stickmen)
    {
        if (stickmen == null)
            throw new ArgumentNullException(nameof(stickmen));

        foreach (var stickman in stickmen)
        {
            if (stickman == null)
                throw new ArgumentNullException(nameof(stickman));

            Action(stickman);
        }
    }

    protected abstract void Action(Stickman stickman);

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent = transform.parent.parent.parent;
        _image.DOColor(_selectionColor, _colorChangeDuration);
        ChangeSize();
        MoveOnSelection();
        Rotate();
        Selected?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Fade();
        Destroyed?.Invoke(this);
        StartCoroutine(Destroy());
    }

    public void MoveWithOffset(Vector3 targetPosition)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOAnchorPos(targetPosition + _offset, _movementDuration));
        sequence.Append(_rectTransform.DOAnchorPos(targetPosition, _movementDuration));
    }

    private void Fade()
    {
        _image.DOFade(_fadingEndValue, _fadingDuration);
        _text.DOFade(_fadingEndValue, _fadingDuration);
    }

    private void MoveOnSelection()
    {
        float targetPositionY = _rectTransform.localPosition.y + _selectionOffsetY;
        var targetPosition = new Vector2(_rectTransform.localPosition.x, targetPositionY);
        _rectTransform.DOAnchorPos(targetPosition, _movementDurationOnSelection);
    }

    private void Rotate() 
        => _rectTransform.DOLocalRotate(Vector3.zero, _rotationDuration);

    private void ChangeSize()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(_sizeOnSelection, _scalingDuration));
        sequence.Append(transform.DOScale(_startSize, _scalingDuration));
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delayBeforeDestroy);
        Destroy(gameObject);
    }
}