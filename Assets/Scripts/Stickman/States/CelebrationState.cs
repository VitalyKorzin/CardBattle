using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetSearchState))]
public class CelebrationState : MonoBehaviour
{
    private TargetSearchState _targetSearchState;

    private void OnDisable()
        => _targetSearchState.TargetsDied -= OnTargetsDied;

    private void Start()
    {
        _targetSearchState = GetComponent<TargetSearchState>();
        _targetSearchState.TargetsDied += OnTargetsDied;
    }

    private void OnTargetsDied()
        => StartCoroutine(Celebrate());

    private IEnumerator Celebrate()
    {
        while (enabled)
        {
            Debug.Log("Celebration");
            yield return null;
        }
    }
}