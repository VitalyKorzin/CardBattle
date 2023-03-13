using UnityEngine;

[RequireComponent(typeof(CardsDeck))]
public class SoundOfAddingCardToDeck : Sound
{
    private CardsDeck _cardsDeck;

    private void OnEnable() 
        => _cardsDeck.Added += OnCardAdded;

    private void OnDisable() 
        => _cardsDeck.Added -= OnCardAdded;

    protected override void Awake()
    {
        base.Awake();
        _cardsDeck = GetComponent<CardsDeck>();
    }

    private void OnCardAdded() => Play();
}