using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementState), typeof(Stickman), typeof(Animator))]
public class AttackState : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _secondsBetweenAttack;
    [Min(0)]
    [SerializeField] private float _transitionRange;

    private MovementState _movementState;
    private Stickman _stickman;
    private Coroutine _attackJob;
    private Animator _animator;

    public event UnityAction TargetDied;
    public event UnityAction<Transform> TargetAttacked;
    public event UnityAction<Stickman> TargetGone;

    private void OnDisable()
        => _movementState.TargetApproached -= OnTargetApproached;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movementState = GetComponent<MovementState>();
        _stickman = GetComponent<Stickman>();
        _movementState.TargetApproached += OnTargetApproached;
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
        var delay = new WaitForSeconds(_secondsBetweenAttack);
        TargetAttacked?.Invoke(stickman.transform);

        while (TargetInAttackZone(stickman))
        {
            _animator.SetTrigger(StickmanAnimator.Params.IsAttacking);
            yield return delay;
            stickman.Apply(_stickman.Damage);
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
        => stickman != null && GetDistanceToTarget(stickman.transform) < _transitionRange;

    private bool EnemyGone(Stickman stickman)
        => stickman != null && GetDistanceToTarget(stickman.transform) > _transitionRange;

    private float GetDistanceToTarget(Transform target)
        => Vector3.Distance(transform.position, target.position);
}