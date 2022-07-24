using UnityEngine;

[RequireComponent(typeof(TargetSearchState), typeof(Animator))]
public class CelebrationState : MonoBehaviour
{
    private TargetSearchState _targetSearchState;
    private Animator _animator;

    private void OnDisable()
        => _targetSearchState.TargetsDied -= OnTargetsDied;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _targetSearchState = GetComponent<TargetSearchState>();
        _targetSearchState.TargetsDied += OnTargetsDied;
    }

    private void OnTargetsDied() 
        => _animator.SetBool(StickmanAnimator.Params.IsCelebrating, true);
}