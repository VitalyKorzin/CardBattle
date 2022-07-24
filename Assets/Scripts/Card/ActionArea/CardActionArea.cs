using System;
using System.Collections.Generic;
using UnityEngine;

public class CardActionArea : MonoBehaviour
{
    private readonly List<Stickman> _stickmen = new List<Stickman>();

    private RaycastHit[] _hits;
    private Camera _camera;
    private Ray _ray;

    public IReadOnlyList<Stickman> Stickmen => _stickmen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            _stickmen.Add(stickman);
            stickman.Select();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Stickman stickman))
        {
            _stickmen.Remove(stickman);
            stickman.Deselect();
        }
    }

    private void Start() => _camera = Camera.main;

    private void Update() => SetPosition();

    public void Initialize(Card card)
    {
        if (card == null)
            throw new ArgumentNullException(nameof(card));

        card.Destroyed += OnCardDestroyed;
    }

    private void SetPosition()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        _hits = Physics.RaycastAll(_ray);

        foreach (var hit in _hits)
        {
            if (hit.collider.TryGetComponent(out Platform _))
                transform.position = hit.point;
        }
    }

    private void OnCardDestroyed(Card card)
    {
        foreach (var stickman in _stickmen)
            stickman.Deselect();

        card.Destroyed -= OnCardDestroyed;
        card.Use(this);
        Destroy(gameObject);
    }
}