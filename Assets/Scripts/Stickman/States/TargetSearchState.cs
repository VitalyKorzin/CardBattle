using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Stickman), typeof(MovementState), typeof(AttackState))]
public class TargetSearchState : MonoBehaviour
{
    private StickmenSquad _enemies;
    private MovementState _movementState;
    private AttackState _attackState;
    private Stickman _stickman;

    public event UnityAction<Stickman> Finded;
    public event UnityAction TargetsDied;

    private void OnDisable()
    {
        _movementState.TargetDied -= OnNeedTarget;
        _attackState.TargetDied -= OnNeedTarget;
        _stickman.FightStarted -= OnFightStarted;
    }

    private void Start()
    {
        _movementState = GetComponent<MovementState>();
        _attackState = GetComponent<AttackState>();
        _stickman = GetComponent<Stickman>();
        _movementState.TargetDied += OnNeedTarget;
        _attackState.TargetDied += OnNeedTarget;
        _stickman.FightStarted += OnFightStarted;
    }

    private void OnFightStarted(StickmenSquad enemies)
    {
        if (enemies == null)
            throw new ArgumentNullException(nameof(enemies));

        _enemies = enemies;
        OnNeedTarget();
    }

    private void OnNeedTarget()
    {
        if (TryFindNearestTarget(out Stickman target))
            Finded?.Invoke(target);
        else
            TargetsDied?.Invoke();
    }

    private bool TryFindNearestTarget(out Stickman target)
    {
        target = _enemies.Stickmen.FirstOrDefault();

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            foreach (var stickman in _enemies.Stickmen)
            {
                var newDistance = Vector3.Distance(transform.position, stickman.transform.position);

                if (newDistance < distance)
                {
                    distance = newDistance;
                    target = stickman;
                }
            }
        }

        return target != null;
    }
}