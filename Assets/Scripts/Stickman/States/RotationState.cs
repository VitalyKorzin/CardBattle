using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovementState), typeof(AttackState))]
public class RotationState : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _speed = 0.2f;

    private MovementState _movementState;
    private AttackState _attackState;
    private Coroutine _rotationJob;

    private void OnDisable()
    {
        _movementState.PositionChanged -= OnPositionChanged;
        _movementState.CameToPlace -= OnCameToPlace;
        _attackState.TargetAttacked -= OnTargetAttacked;
    }

    private void Start()
    {
        _movementState = GetComponent<MovementState>();
        _attackState = GetComponent<AttackState>();
        _movementState.PositionChanged += OnPositionChanged;
        _movementState.CameToPlace += OnCameToPlace;
        _attackState.TargetAttacked += OnTargetAttacked;
    }

    private void OnTargetAttacked(Transform target) 
        => StartRotate(RotateTo(target));

    private void OnCameToPlace(Quaternion rotation) 
        => StartRotate(RotateTo(rotation));

    private void OnPositionChanged(Vector3 direction) 
        => StartRotate(RotateTo(direction));

    private void StartRotate(IEnumerator rotation)
    {
        if (_rotationJob != null)
            StopCoroutine(_rotationJob);

        _rotationJob = StartCoroutine(rotation);
    }

    private IEnumerator RotateTo(Transform target)
    {
        while (target != null && transform.rotation != GetTargetRotation(target.position))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, GetTargetRotation(target.position), _speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator RotateTo(Quaternion targetRotation)
    {
        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator RotateTo(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private Quaternion GetTargetRotation(Vector3 targetPosition) 
        => Quaternion.LookRotation(targetPosition - transform.position);
}