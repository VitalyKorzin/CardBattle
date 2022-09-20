using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _speed;
    [Min(0)]
    [SerializeField] private int _damage;

    private readonly float _targetDistance = 0.1f;

    public void StartMove(Stickman target)
    {
        if (target == null)
            return;

        StartCoroutine(Move(target));
    }

    protected abstract void DisplayHit();

    private IEnumerator Move(Stickman target)
    {
        while (target != null && Vector3.Distance(transform.position, target.transform.position) > _targetDistance)
        {
            transform.LookAt(target.transform);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speed * Time.deltaTime);
            yield return null;
        }

        if (target != null)
        {
            target.Apply(_damage);
            DisplayHit();
        }

        Destroy(gameObject);
    }
}