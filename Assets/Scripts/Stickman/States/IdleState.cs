using UnityEngine;

[RequireComponent(typeof(Stickman))]
public class IdleState : MonoBehaviour
{
    private Stickman _stickman;

    private void OnDisable()
        => _stickman.FightStarted -= OnFightStarted;

    private void Start()
    {
        _stickman = GetComponent<Stickman>();
        _stickman.FightStarted += OnFightStarted;
    }

    private void OnFightStarted(StickmenSquad squad)
        => enabled = false;
}