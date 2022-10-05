using UnityEngine;

[RequireComponent(typeof(SelectedCardSlot))]
public class SoundOfAddingCardToSlot : Sound
{
    private SelectedCardSlot _cardSlot;

    private void OnEnable() 
        => _cardSlot.Selected += OnCardSelected;

    private void OnDisable() 
        => _cardSlot.Selected -= OnCardSelected;

    protected override void Awake()
    {
        base.Awake();
        _cardSlot = GetComponent<SelectedCardSlot>();
    }

    private void OnCardSelected() => Play();
}