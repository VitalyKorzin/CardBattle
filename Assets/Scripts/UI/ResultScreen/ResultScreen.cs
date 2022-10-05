using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class ResultScreen<T> : MonoBehaviour
        where T : StickmenSquad
{
    [Min(0)]
    [SerializeField] private float _delayBeforeBackgroundShowing;
    [Min(0)]
    [SerializeField] private float _delayBeforeResultShowing;
    [SerializeField] private T _stickmenSquad;
    [SerializeField] private ResultScreenBackgroundDisplay _backgroundDisplay;
    [SerializeField] private ResultDisplay _resultDisplay;

    public event UnityAction Showed;

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

        _stickmenSquad.Died += OnStickmenSquadDied;
        _backgroundDisplay.gameObject.SetActive(false);
    }

    private void OnDisable()
        => _stickmenSquad.Died -= OnStickmenSquadDied;

    private void OnStickmenSquadDied()
        => StartCoroutine(Show());

    private IEnumerator Show()
    {
        _backgroundDisplay.gameObject.SetActive(true);
        yield return new WaitForSeconds(_delayBeforeBackgroundShowing);
        _backgroundDisplay.Display();
        yield return new WaitForSeconds(_delayBeforeResultShowing);
        _resultDisplay.Display();
        Showed?.Invoke();
    }

    private void Validate()
    {
        if (_stickmenSquad == null)
            throw new InvalidOperationException();

        if (_backgroundDisplay == null)
            throw new InvalidOperationException();

        if (_resultDisplay == null)
            throw new InvalidOperationException();
    }
}