using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartFightAnnunciator : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _secondsBetweenFightStart;
    [SerializeField] private StartFightTextDisplay _textDisplay;
    [SerializeField] private CardsHand _cardsHand;

    public event UnityAction FightStarted;

    private void OnEnable()
        => _cardsHand.Ended += OnCardsDeckEnded;

    private void OnDisable()
        => _cardsHand.Ended -= OnCardsDeckEnded;

    private void OnCardsDeckEnded()
        => StartCoroutine(Annunce());

    private IEnumerator Annunce()
    {
        _textDisplay.Appear();
        yield return new WaitForSeconds(_secondsBetweenFightStart);
        _textDisplay.Fade();
        FightStarted?.Invoke();
    }
}