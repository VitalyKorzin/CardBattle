using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public abstract class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Min(0)]
    [SerializeField] private float _selectionOffsetY;
    [Min(0)]
    [SerializeField] private float _colorChangeDuration;
    [Min(0)]
    [SerializeField] private float _movementDuration;
    [Min(0)]
    [SerializeField] private float _fadingDuration;
    [Min(0)]
    [SerializeField] private float _delayBeforeDestroy;
    [Min(0)]
    [SerializeField] private float _rotationDuration;
    [Min(0)]
    [SerializeField] private float _sizeOnSelection;
    [Min(0)]
    [SerializeField] private float _targetScaleOnFading;
    [Min(0)]
    [SerializeField] private float _scalingDuration;
    [Min(0)]
    [SerializeField] private float _movementDurationOnSelection;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Color _selectionBackgroundColor;
    [SerializeField] private Color _selectionFrameColor;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _glow;

    private readonly float _appearanceEndValue = 1f;
    private readonly float _fadingEndValue = 0f;
    private readonly float _startSize = 1f;

    private RectTransform _rectTransform;
    private Image _background;

    public event UnityAction<Card> Selected;
    public event UnityAction<Card> Destroyed;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception exception)
        {
            enabled = false;
            throw exception;
        }
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _background = GetComponent<Image>();
        _rectTransform.localRotation = transform.parent.localRotation;
    }

    public abstract void Use<T>(List<T> stickmen, Vector3 actionPosition) where T: Stickman;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (transform.parent.parent != null)
            transform.parent = transform.parent.parent;

        _glow.DOFade(_appearanceEndValue, _colorChangeDuration);
        ChangeColor();
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
        _background.DOFade(_fadingEndValue, _fadingDuration);
        _icon.DOFade(_fadingEndValue, _fadingDuration);
        _frame.DOFade(_fadingEndValue, _fadingDuration);
        _glow.DOFade(_fadingEndValue, _fadingDuration);
        _rectTransform.DOScale(_targetScaleOnFading, _fadingDuration);
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

    private void ChangeColor()
    {
        _background.DOColor(_selectionBackgroundColor, _colorChangeDuration);
        _frame.DOColor(_selectionFrameColor, _colorChangeDuration);
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delayBeforeDestroy);
        Destroy(gameObject);
    }

    private void Validate()
    {
        if (_icon == null)
            throw new InvalidOperationException();

        if (_frame == null)
            throw new InvalidOperationException();
    }
}