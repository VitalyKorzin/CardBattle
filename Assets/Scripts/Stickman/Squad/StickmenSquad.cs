using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class StickmenSquad : MonoBehaviour
{
    [SerializeField] private StartFightAnnunciator _startFightAnnunciator;
    [SerializeField] private StickmenSquad _enemies;
    [SerializeField] private StickmenSpawner _spawner;
    [SerializeField] private List<PlaceInSquad> _places;
    [SerializeField] private Transform _celebrationPlace;

    private readonly List<Stickman> _stickmen = new List<Stickman>();

    public IReadOnlyList<Stickman> Stickmen => _stickmen;

    public event UnityAction Died;
    public event UnityAction<Stickman> StickmanAdded;

    protected virtual void OnEnable()
    {
        _spawner.Spawned += OnStickmanSpawned;
        _startFightAnnunciator.FightStarted += OnFightStarted;
    }

    protected virtual void OnDisable()
    {
        _spawner.Spawned -= OnStickmanSpawned;
        _startFightAnnunciator.FightStarted -= OnFightStarted;
    }

    private void Start()
    {
        _spawner.Initialize(_places);

        foreach (PlaceInSquad place in _places)
        {
            if (place.Occupied)
                OnStickmanSpawned(place.Stickman, place);
        }
    }

    protected void EnablePlaceDisplay()
    {
        foreach (PlaceInSquad place in _places)
            place.EnableDisplay();
    }

    protected void DisablePlaceDisplay()
    {
        foreach (PlaceInSquad place in _places)
            place.DisableDisplay();
    }

    private void OnStickmanSpawned(Stickman stickman, PlaceInSquad place)
    {
        _stickmen.Add(stickman);
        stickman.AddToSquad(place);
        stickman.Died += OnStickmanDied;
        StickmanAdded?.Invoke(stickman);
    }

    private void OnFightStarted()
    {
        foreach (var stickman in _stickmen)
            stickman.StartFight(_enemies);

        transform.SetPositionAndRotation(_celebrationPlace.position, _celebrationPlace.rotation);
    }

    private void OnStickmanDied(Stickman stickman)
    {
        stickman.Died -= OnStickmanDied;
        _stickmen.Remove(stickman);
        Destroy(stickman.gameObject);

        if (_stickmen.Count == 0)
            Died?.Invoke();
    }
}