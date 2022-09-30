using UnityEngine;

public class UsingCards : LearningStage
{
    [SerializeField] private CardsHand _cardsHand;

    private void OnEnable() => _cardsHand.Ended += OnCardsEnded;

    private void OnDisable() => _cardsHand.Ended -= OnCardsEnded;

    private void OnCardsEnded() => Finish();
}