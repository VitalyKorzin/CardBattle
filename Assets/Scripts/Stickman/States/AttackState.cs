using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementState), typeof(Stickman))]
public class AttackState : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _secondsBetweenAttack;
    [Min(0)]
    [SerializeField] private float _transitionRange;

    private MovementState _movementState;
    private Stickman _stickman;
    private Stickman _target;
    private Coroutine _attackJob;

    public event UnityAction TargetDied;
    public event UnityAction<Stickman> TargetGone;

    private void OnDisable()
        => _movementState.TargetApproached -= OnTargetApproached;

    private void Start()
    {
        _movementState = GetComponent<MovementState>();
        _stickman = GetComponent<Stickman>();
        _movementState.TargetApproached += OnTargetApproached;
    }

    private void Update()
    {
        if (_target != null)
            transform.LookAt(_target.transform);
    }

    private void OnTargetApproached(Stickman stickman)
    {
        _target = stickman;
        _target.Died += OnTargetDied;

        if (_attackJob != null)
            StopCoroutine(_attackJob);

        _attackJob = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        var delay = new WaitForSeconds(_secondsBetweenAttack);

        while (TargetInAttackZone())
        {
            _target.Apply(_stickman.Damage);
            yield return delay;
        }

        if (EnemyGone())
        {
            _target.Died -= OnTargetDied;
            TargetGone?.Invoke(_target);
            _target = null;
        }
    }

    private void OnTargetDied(Stickman target)
    {
        _target.Died -= OnTargetDied;

        if (_attackJob != null)
            StopCoroutine(_attackJob);

        TargetDied?.Invoke();
    }

    private bool TargetInAttackZone()
        => _target != null && GetDistanceToTarget(_target.transform) < _transitionRange;

    private bool EnemyGone()
        => _target != null && GetDistanceToTarget(_target.transform) > _transitionRange;

    private float GetDistanceToTarget(Transform target)
        => Vector3.Distance(transform.position, target.position);
}