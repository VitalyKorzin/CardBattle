using System.Collections;
using UnityEngine;

public class CardsDispenser : MonoBehaviour
{
    [SerializeField] private CardView[] _templates;
    [SerializeField] private CardsDeckView _cardsDeck;
    [SerializeField] private float _delayBetweenDispensing;

    public void Start()
    {
        StartCoroutine(Dispense());
    }

    private IEnumerator Dispense()
    {
        var _delay = new WaitForSeconds(_delayBetweenDispensing);

        for (var i = 0; i < _cardsDeck.MaximumCount; i++)
        {
            var card = Instantiate(_templates[Random.Range(0, _templates.Length)], transform.position, Quaternion.identity, _cardsDeck.transform);
            _cardsDeck.Add(card);
            yield return _delay;
        }
    }
}