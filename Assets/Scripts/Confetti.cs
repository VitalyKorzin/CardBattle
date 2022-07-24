using System;
using System.Collections;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _delay;
    [SerializeField] private ParticleSystem _blast;
    [SerializeField] private ParticleSystem _shower;
    [SerializeField] private EnemiesSquad _enemiesSquad;
    [SerializeField] private Transform[] _blastPositions;

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

        _enemiesSquad.Died += OnEnemiesSquadDied;
    }

    private void OnDisable() 
        => _enemiesSquad.Died -= OnEnemiesSquadDied;

    private void OnEnemiesSquadDied() 
        => StartCoroutine(Show());

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(_delay);

        foreach (var blastPosition in _blastPositions)
            Instantiate(_blast, blastPosition);

        _shower.Play();
    }

    private void Validate()
    {
        if (_blast == null)
            throw new InvalidOperationException();

        if (_shower == null)
            throw new InvalidOperationException();

        if (_enemiesSquad == null)
            throw new InvalidOperationException();

        if (_blastPositions == null)
            throw new InvalidOperationException();

        if (_blastPositions.Length == 0)
            throw new InvalidOperationException();
    }
}