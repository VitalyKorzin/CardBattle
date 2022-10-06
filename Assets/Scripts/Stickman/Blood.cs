using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Blood : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _bloodParticles;

    private readonly float _delay = 8f;

    public event UnityAction Released;

    public void Draw()
    {
        transform.parent = null;

        foreach (ParticleSystem blood in _bloodParticles)
            blood.Play();

        Released?.Invoke();
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }
}