using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartFightAnnunciator : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _secondsBetweenFightStart;
    [SerializeField] private StartFightTextDisplay _textDisplay;
    [SerializeField] private CardsDeck _cardsDeck;

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

    private void OnCardsDeckEnded()
        => StartCoroutine(Annunce());

    private IEnumerator Annunce()
    {
        _textDisplay.Appear();
        yield return new WaitForSeconds(_secondsBetweenFightStart);
        _textDisplay.Fade();
        FightStarted?.Invoke();
    }

    private void Validate()
    {
        if (_cardsDeck == null)
            throw new InvalidOperationException();

        if (_textDisplay == null)
            throw new InvalidOperationException();
    }
}