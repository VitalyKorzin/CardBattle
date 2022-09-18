using System;
using UnityEngine;

public class PlaceInSquad : MonoBehaviour
{
    [SerializeField] private Stickman _stickman;

    public bool Free => _stickman == null;
    public bool Occupied => _stickman != null;
    public Stickman Stickman => _stickman;

    public void Occupy(Stickman stickman)
    {
        if (stickman == null)
            throw new ArgumentNullException(nameof(stickman));

        _stickman = stickman;
    }
}