using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PlaceInSquad : MonoBehaviour
{
    [SerializeField] private Stickman _stickman;

    private MeshRenderer _renderer;

    private void Awake()
        => _renderer = GetComponent<MeshRenderer>();

    public bool Free => _stickman == null;
    public bool Occupied => _stickman != null;
    public Stickman Stickman => _stickman;

    public void Occupy(Stickman stickman)
    {
        if (stickman == null)
            throw new ArgumentNullException(nameof(stickman));

        _stickman = stickman;
    }

    public void EnableDisplay()
        => _renderer.enabled = true;

    public void DisableDisplay()
        => _renderer.enabled = false;
}