using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _speed;
    [Min(0)]
    [SerializeField] private int _damage;

    private readonly float _targetDistance = 0.1f;

    private Vector3 _direction;

    public void StartMove(Stickman target)
    {
        if (target == null)
            return;

        StartCoroutine(Move(target));
    }

    private IEnumerator Move(Stickman target)
    {
        while (target != null && Vector3.Distance(transform.position, target.transform.position) > _targetDistance)
        {
            _direction = (target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), 100f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
            yield return null;
        }

        if (target != null)
            target.Apply(_damage);

        Destroy(gameObject);
    }
}