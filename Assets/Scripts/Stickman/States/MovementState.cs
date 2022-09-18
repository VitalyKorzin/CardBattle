using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Stickman), typeof(TargetSearchState), typeof(AttackState))]
[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class MovementState : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _transitionRange;

    private readonly float _neededDistanceToPlaceInSquad = 0.1f;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private PlaceInSquad _placeInSquad;
    private Stickman _stickman;
    private TargetSearchState _targetSearchState;
    private AttackState _attackState;
    private Coroutine _movementJob;
    private Vector3 _lastPosition;
    private Weapon _currentWeapon;

    public event UnityAction<Stickman> TargetApproached;
    public event UnityAction TargetDied;
    public event UnityAction<Vector3> PositionChanged;
    public event UnityAction<Quaternion> CameToPlace;

    private void OnDisable()
    {
        _stickman.AddedToSquad -= OnStickmanAddedToSquad;
        _stickman.FightStarted -= OnStickmanFightStarted;
        _targetSearchState.Finded -= OnNeedMove;
        _targetSearchState.TargetsDied -= OnNeedMoveToPlaceInSquad;
        _attackState.TargetGone -= OnNeedMove;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _navMeshAgent.updateRotation = false;
        _animator = GetComponent<Animator>();
        _stickman = GetComponent<Stickman>();
        _targetSearchState = GetComponent<TargetSearchState>();
        _attackState = GetComponent<AttackState>();
        _stickman.AddedToSquad += OnStickmanAddedToSquad;
        _stickman.FightStarted += OnStickmanFightStarted;
        _targetSearchState.Finded += OnNeedMove;
        _targetSearchState.TargetsDied += OnNeedMoveToPlaceInSquad;
        _attackState.TargetGone += OnNeedMove;
        _lastPosition = transform.position;
    }

    public void Initialize(Weapon weapon)
    {
        if (weapon == null)
            throw new InvalidOperationException();

        _currentWeapon = weapon;
    }

    private void OnStickmanAddedToSquad(PlaceInSquad placeInSquad)
    {
        _placeInSquad = placeInSquad;

        if (transform.position != _placeInSquad.transform.position)
            OnNeedMoveToPlaceInSquad();
    }

    private void OnStickmanFightStarted(StickmenSquad squad) 
        => _navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;

    private void OnNeedMove(Stickman target) 
        => StartMove(Move(target));

    private void OnNeedMoveToPlaceInSquad() 
        => StartMove(Move(_placeInSquad));

    private void StartMove(IEnumerator movement)
    {
        if (_movementJob != null)
            StopCoroutine(_movementJob);

        _animator.SetBool(StickmanAnimator.Params.IsRunning, true);
        _movementJob = StartCoroutine(movement);
    }

    private IEnumerator Move(Stickman target)
    {
        _navMeshAgent.isStopped = false;

        while (DidNotReachTo(target))
        {
            Move(target.transform.position);
            yield return null;
        }

        _animator.SetBool(StickmanAnimator.Params.IsRunning, false);
        _navMeshAgent.isStopped = true;

        if (target == null)
            TargetDied?.Invoke();
        else
            TargetApproached?.Invoke(target);
    }

    private IEnumerator Move(PlaceInSquad placeInSquad)
    {
        _navMeshAgent.isStopped = false;

        while (DidNotReachTo(placeInSquad))
        {
            Move(placeInSquad.transform.position);
            yield return null;
        }

        _animator.SetBool(StickmanAnimator.Params.IsRunning, false);
        CameToPlace?.Invoke(_placeInSquad.transform.rotation);
    }

    private bool DidNotReachTo(Stickman target)
        => target != null && GetDistanceToTarget(target.transform.position) > _currentWeapon.TransitionRange;

    private bool DidNotReachTo(PlaceInSquad placeInSquad)
        => GetDistanceToTarget(placeInSquad.transform.position) > _neededDistanceToPlaceInSquad;

    private void Move(Vector3 targetPosition)
    {
        if (_navMeshAgent.isActiveAndEnabled)
            _navMeshAgent.SetDestination(targetPosition);

        PositionChanged?.Invoke(transform.position - _lastPosition);
        _lastPosition = transform.position;
    }

    private float GetDistanceToTarget(Vector3 targetPosition)
        => Vector3.Distance(transform.position, targetPosition);
}