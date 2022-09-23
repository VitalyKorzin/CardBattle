using System.Collections;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _damage;
    [Min(0)]
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _explosion;

    private readonly float _delayBeforeDestroing = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.Apply(_damage);

        if (other.TryGetComponent(out Platform _))
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            StartCoroutine(Destroy());
        }
    }

    private void Update()
        => transform.Translate(_speed * Time.deltaTime * Vector3.down);

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delayBeforeDestroing);
        Destroy(gameObject);
    }
}