using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class StickmenSpawner : MonoBehaviour
{
    [SerializeField] private StickmenSquad _squad;
    [SerializeField] private CardsHand _cardsHand;

    private List<PlaceInSquad> _places;

    public IReadOnlyList<PlaceInSquad> FreePlaces => _places.Where(place => place.Free).ToList();

    public event UnityAction<Stickman, PlaceInSquad> Spawned;

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

        _cardsHand.CardAdded += OnCardAdded;
    }

    private void OnDisable()
        => _cardsHand.CardAdded -= OnCardAdded;

    public void Initialize(List<PlaceInSquad> places)
    {
        if (places == null)
            throw new ArgumentNullException(nameof(places));

        if (places.Count == 0)
            throw new ArgumentOutOfRangeException(nameof(places));

        _places = places;
    }

    private void Spawn(Stickman template, Vector3 spawnPoint, PlaceInSquad place)
    {
        var stickman = Instantiate(template, spawnPoint, place.transform.rotation);
        place.Occupy(stickman);
        Spawned?.Invoke(stickman, place);
    }

    private void OnCardAdded(Card card)
    {
        if (card is MultiplierCard multiplierCard)
            multiplierCard.Used += OnMultiplierCardUsed;
    }

    private void OnMultiplierCardUsed(MultiplierCard card, List<Stickman> stickmen)
    {
        card.Used -= OnMultiplierCardUsed;

        foreach (var stickman in stickmen)
        {
            if (_squad.Stickmen.Contains(stickman))
            {
                var count = GetSickmenCount(card);

                for (var i = 0; i < count; i++)
                    SpawnInNearestPlace(stickman);
            }
        }
    }

    private void SpawnInNearestPlace(Stickman stickman)
    {
        PlaceInSquad freePlace = GetNearestFreePlace(stickman.transform.position);
        Spawn(stickman, stickman.transform.position, freePlace);
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

        if (_cardsHand == null)
            throw new InvalidOperationException();
    }
}