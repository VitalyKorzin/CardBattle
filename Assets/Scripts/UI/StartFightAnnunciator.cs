using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StartFightAnnunciator : MonoBehaviour
{
    [SerializeField] private CardsDeck _cardsDeck;
    [SerializeField] private TMP_Text _text;
    [Min(0)]
    [SerializeField] private float _textFadingDuration;
    [Min(0)]
    [SerializeField] private float _secondsBetweenFightStart;

    private readonly float _fadingEndValue = 0f;

    public event UnityAction FightStarted;

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

        _cardsDeck.Ended += OnCardsDeckEnded;
    }

    private void OnDisable()
        => _cardsDeck.Ended -= OnCardsDeckEnded;

    private void Start() => _text.enabled = false;

    private void OnCardsDeckEnded()
        => StartCoroutine(Annunce());

    private IEnumerator Annunce()
    {
        _text.enabled = true;
        yield return new WaitForSeconds(_secondsBetweenFightStart);
        _text.DOFade(_fadingEndValue, _textFadingDuration);
        FightStarted?.Invoke();
    }

    private void Validate()
    {
        if (_cardsDeck == null)
            throw new InvalidOperationException();

        if (_text == null)
            throw new InvalidOperationException();
    }
}