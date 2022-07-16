using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class StickmenSpawner : MonoBehaviour
{
    [SerializeField] private StickmenSquad _squad;
    [SerializeField] private CardsDeck _cardsDeck;
    [SerializeField] private Stickman _template;

    private List<PlaceInSquad> _places;

    public IReadOnlyList<PlaceInSquad> FreePlaces => _places.Where(place => place.Free).ToList();

    public event UnityAction<Stickman> Spawned;

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

        _cardsDeck.CardAdded += OnCardAdded;
    }

    private void OnDisable()
        => _cardsDeck.CardAdded -= OnCardAdded;

    public void Initialize(List<PlaceInSquad> places)
    {
        _places = places;

        foreach (var place in places)
        {
            if (place.Occupied)
                Spawn(place.transform.position, place);
        }
    }

    private void Spawn(Vector3 spawnPoint, PlaceInSquad place)
    {
        var stickman = Instantiate(_template, spawnPoint, place.transform.rotation);
        stickman.AddToSquad(place);
        Spawned?.Invoke(stickman);
    }

    private void OnCardAdded(Card card)
    {
        if (card is MultiplierCard multiplierCard)
            multiplierCard.Used += OnMultiplierCardUsed;
    }

    private void OnMultiplierCardUsed(MultiplierCard card, Stickman stickman)
    {
        card.Used -= OnMultiplierCardUsed;

        if (_squad.Stickmen.Contains(stickman))
            for (var i = 0; i < GetSickmenCount(card); i++)
                SpawnInNearestPlace(stickman);
    }

    private void SpawnInNearestPlace(Stickman stickman)
    {
        PlaceInSquad freePlace = GetNearestFreePlace(stickman.transform.position);
        freePlace.Occupy();
        Spawn(stickman.transform.position, freePlace);
    }

    private int GetSickmenCount(MultiplierCard card)
    {
        int stickmenCount = card.SitckmenCount;

        if (FreePlaces.Count < stickmenCount)
            stickmenCount = FreePlaces.Count;

        return stickmenCount;
    }

    private PlaceInSquad GetNearestFreePlace(Vector3 currentPosition)
    {
        PlaceInSquad nearestFreePlace = _places.FirstOrDefault(place => place.Free);
        float distance = Vector3.Distance(currentPosition, nearestFreePlace.transform.position);

        foreach (var freePlace in FreePlaces)
        {
            float newDistance = Vector3.Distance(currentPosition, freePlace.transform.position);

            if (newDistance < distance)
            {
                distance = newDistance;
                nearestFreePlace = freePlace;
            }
        }

        return nearestFreePlace;
    }

    private void Validate()
    {
        if (_squad == null)
            throw new InvalidOperationException();

        if (_cardsDeck == null)
            throw new InvalidOperationException();

        if (_template == null)
            throw new InvalidOperationException();
    }
}