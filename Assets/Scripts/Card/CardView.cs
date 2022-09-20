using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _fadingDuration;
    [Min(0)]
    [SerializeField] private float _colorChangeDuration;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _glow;
    [SerializeField] private Image _background;
    [SerializeField] private Color _selectionBackgroundColor;
    [SerializeField] private Color _selectionFrameColor;

    private readonly float _appearanceEndValue = 1f;
    private readonly float _fadingEndValue = 0f;

    private Color _defaultBackgroundColor;
    private Color _defaultFrameColor;

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

    private void Start()
    {
        _defaultBackgroundColor = _background.color;
        _defaultFrameColor = _frame.color;
    }

    public void ChangeColorOnSelection()
    {
        _glow.DOFade(_appearanceEndValue, _colorChangeDuration);
        _background.DOColor(_selectionBackgroundColor, _colorChangeDuration);
        _frame.DOColor(_selectionFrameColor, _colorChangeDuration);
    }

    public void ChangeColorOnDeselection()
    {
        _glow.DOFade(_fadingEndValue, _colorChangeDuration);
        _background.DOColor(_defaultBackgroundColor, _colorChangeDuration);
        _frame.DOColor(_defaultFrameColor, _colorChangeDuration);
    }

    public void Fade()
    {
        _background.DOFade(_fadingEndValue, _fadingDuration);
        _icon.DOFade(_fadingEndValue, _fadingDuration);
        _frame.DOFade(_fadingEndValue, _fadingDuration);
        _glow.DOFade(_fadingEndValue, _fadingDuration);
    }

    private void Validate()
    {
        if (_icon == null)
            throw new InvalidOperationException();

        if (_frame == null)
            throw new InvalidOperationException();

        if (_glow == null)
            throw new InvalidOperationException();

        if (_background == null)
            throw new InvalidOperationException();

        if (_selectionBackgroundColor == null)
            throw new InvalidOperationException();

        if (_selectionFrameColor == null)
            throw new InvalidOperationException();
    }
}