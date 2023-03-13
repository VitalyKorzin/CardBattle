using UnityEngine;
using UnityEngine.UI;

public class CardsSelection : LearningStage
{
    [SerializeField] private CardsSelectionSlots _slots;
    [SerializeField] private Button _closeCardsDeckButton;

    private void OnEnable()
    {
        _slots.CardsSelected += OnCardsSelected;
        _closeCardsDeckButton.interactable = false;
    }

    private void OnDisable()
    {
        _slots.CardsSelected -= OnCardsSelected;
        _closeCardsDeckButton.interactable = true;
    }

    private void OnCardsSelected() => Finish();
}