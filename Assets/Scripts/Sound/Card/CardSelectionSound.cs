using UnityEngine;

public class CardSelectionSound : Sound
{
    [SerializeField] private Card _card;

    private void OnEnable() 
        => _card.Selected += OnCardSelected;

    private void OnDisable() 
        => _card.Selected -= OnCardSelected;

    private void OnCardSelected(Card card) => Play();
}