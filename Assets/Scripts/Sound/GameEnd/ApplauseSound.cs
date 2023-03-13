using UnityEngine;

public class ApplauseSound : Sound
{
    [SerializeField] private EnemiesSquad _enemiesSquad;

    private void OnEnable() 
        => _enemiesSquad.Died += OnSquadDied;

    private void OnDisable() 
        => _enemiesSquad.Died -= OnSquadDied;

    private void OnSquadDied() => Play();
}