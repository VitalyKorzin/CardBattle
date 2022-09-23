using System.Collections.Generic;
using UnityEngine;

public abstract class ActionArea : MonoBehaviour
{
    private readonly List<Stickman> _stickmen = new List<Stickman>();

    private RaycastHit[] _hits;
    private Camera _camera;
    private Ray _ray;

    public IReadOnlyList<Stickman> Stickmen => _stickmen;

    private void OnDisable()
    {
        foreach (Stickman stickman in _stickmen)
            if (stickman != null)
                stickman.Deselect();
    }

    private void Start() => _camera = Camera.main;

    private void Update() => SetPosition();

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
}