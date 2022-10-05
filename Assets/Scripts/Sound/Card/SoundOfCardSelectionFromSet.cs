using UnityEngine;

[RequireComponent(typeof(CardsSet))]
public class SoundOfCardSelectionFromSet : Sound
{
    private CardsSet _cardsSet;

    private void OnEnable() 
        => _cardsSet.CardSelected += OnCardSelected;

    private void OnDisable() 
        => _cardsSet.CardSelected -= OnCardSelected;

    protected override void Awake()
    {
        base.Awake();
        _cardsSet = GetComponent<CardsSet>();
    }

    private void OnCardSelected() => Play();
}