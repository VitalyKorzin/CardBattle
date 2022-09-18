using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementState), typeof(Animator))]
public class AttackState : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;

    private MovementState _movementState;
    private Coroutine _attackJob;
    private Animator _animator;
    private Weapon _currentWeapon;

    public event UnityAction TargetDied;
    public event UnityAction<Transform> TargetAttacked;
    public event UnityAction<Stickman> TargetGone;

    private void OnDisable()
        => _movementState.TargetApproached -= OnTargetApproached;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movementState = GetComponent<MovementState>();
        _movementState.TargetApproached += OnTargetApproached;
    }

    public void Initialize(Weapon weapon)
    {
        if (weapon == null)
            throw new InvalidOperationException();

        _currentWeapon = weapon;
    }

    private void OnTargetApproached(Stickman stickman)
    {
        stickman.Died += OnTargetDied;

        if (_attackJob != null)
            StopCoroutine(_attackJob);

        _attackJob = StartCoroutine(Attack(stickman));
    }

    private IEnumerator Attack(Stickman stickman)
    {
        TargetAttacked?.Invoke(stickman.transform);

        while (TargetInAttackZone(stickman))
        {
            yield return _currentWeapon.Attack(stickman, _animator, _shotPoint);
        }

        if (EnemyGone(stickman))
        {
            stickman.Died -= OnTargetDied;
            TargetGone?.Invoke(stickman);
        }
    }

    private void OnTargetDied(Stickman target)
    {
        target.Died -= OnTargetDied;
        TargetDied?.Invoke();
    }

    private bool TargetInAttackZone(Stickman stickman)
        => stickman != null && GetDistanceToTarget(stickman.transform) < _currentWeapon.TransitionRange;

    private bool EnemyGone(Stickman stickman)
        => stickman != null && GetDistanceToTarget(stickman.transform) > _currentWeapon.TransitionRange;

    private float GetDistanceToTarget(Transform target)
        => Vector3.Distance(transform.position, target.position);
}