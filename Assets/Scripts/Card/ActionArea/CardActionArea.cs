using System;
using System.Collections.Generic;
using UnityEngine;

public class CardActionArea : MonoBehaviour
{
    private readonly List<Stickman> _stickmens = new List<Stickman>();

    private RaycastHit[] _hits;
    private Ray _ray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickmen))
            _stickmens.Add(stickmen);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickmen))
            _stickmens.Remove(stickmen);
    }

    private void Update() => TrySetPosition();

    public void Initialize(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        card.Destroyed += OnCardDestroyed;
    }

    private void TrySetPosition()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _hits = Physics.RaycastAll(_ray);

        foreach (var hit in _hits)
        {
            if (hit.collider.TryGetComponent(out Platform _))
                transform.position = hit.point;
        }
    }

    private void OnCardDestroyed(Card card)
    {
        card.Destroyed -= OnCardDestroyed;
        card.Use(_stickmens);
        Destroy(gameObject);
    }
}