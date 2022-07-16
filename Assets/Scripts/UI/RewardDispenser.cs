using System;
using UnityEngine;

public class RewardDispenser : MonoBehaviour
{
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private RewardReceivingDisplay _rewardReceivingDisplay;

    private readonly int _minimumValue = 15;
    private readonly int _maximumValue = 45;

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

        _enemiesSpawner.Spawned += OnEnemySpawned;
    }

    private void OnDisable()
        => _enemiesSpawner.Spawned -= OnEnemySpawned;

    private void OnEnemySpawned(Stickman stickman)
        => stickman.Died += OnEnemyDied;

    private void OnEnemyDied(Stickman stickman)
    {
        stickman.Died -= OnEnemyDied;
        Instantiate(_rewardReceivingDisplay, stickman.transform.position, Quaternion.identity).Initialize(GetRandomValue());
    }

    private int GetRandomValue()
        => UnityEngine.Random.Range(_minimumValue, _maximumValue);

    private void Validate()
    {
        if (_enemiesSpawner == null)
            throw new InvalidOperationException();

        if (_rewardReceivingDisplay == null)
            throw new InvalidOperationException();
    }
}