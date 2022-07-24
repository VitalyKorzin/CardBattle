using System;
using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _damage;
    [Min(0)]
    [SerializeField] private float _speed;

    private int _currentWaypointIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.Apply(_damage);
    }

    public void StartMove(Vector3[] path)
    {
        if (path == null)
            throw new ArgumentNullException(nameof(path));

        if (path.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(path));

        StartCoroutine(Move(path));
    }

    private IEnumerator Move(Vector3[] path)
    {
        while (_currentWaypointIndex < path.Length)
        {
            Vector3 targetPosition = path[_currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            transform.LookAt(targetPosition);

            if (transform.position == targetPosition)
                _currentWaypointIndex++;

            yield return null;
        }

        Destroy(gameObject);
    }
}