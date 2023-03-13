using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Confetti : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _delay;
    [SerializeField] private ParticleSystem _blast;
    [SerializeField] private ParticleSystem _shower;
    [SerializeField] private EnemiesSquad _enemiesSquad;
    [SerializeField] private Transform[] _blastPositions;

    public event UnityAction Blasted;

    private void OnEnable()
        => _enemiesSquad.Died += OnEnemiesSquadDied;

    private void OnDisable() 
        => _enemiesSquad.Died -= OnEnemiesSquadDied;

    private void OnEnemiesSquadDied() 
        => StartCoroutine(Show());

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(_delay);

        foreach (var blastPosition in _blastPositions)
            Instantiate(_blast, blastPosition);

        Blasted?.Invoke();
        _shower.Play();
    }
}