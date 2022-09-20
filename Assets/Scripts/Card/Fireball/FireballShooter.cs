using System;
using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _shotAngle;
    [SerializeField] private CardsDeck _cardsDeck;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Fireball _template;

    private readonly int _waypointsCount = 100;
    private readonly float _timeStep = 0.1f;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception exception)
        {
            enabled = false;
            throw exception;
        }

        _cardsDeck.CardAdded += OnCardAdded;
    }

    private void OnDisable()
    {
        _cardsDeck.CardAdded -= OnCardAdded;
    }

    private void OnCardAdded(Card card)
    {
        if (card is FireballCard fireballCard)
            fireballCard.Used += OnFireballCardUsed;
    }

    private void OnFireballCardUsed(FireballCard fireballCard, Vector3 actionPosition)
    {
        fireballCard.Used -= OnFireballCardUsed;
        float velocity = GetVelocity(actionPosition);
        Vector3 vector = _shotPoint.forward * velocity;
        Vector3[] path = GetPath(vector);
        Instantiate(_template, _shotPoint.position, Quaternion.identity).StartMove(path);
    }

    private float GetVelocity(Vector3 actionPosition)
    {
        Vector3 direction = actionPosition - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
        transform.rotation = Quaternion.LookRotation(directionXZ, Vector3.up);
        _shotPoint.localEulerAngles = new Vector3(-_shotAngle, 0f, 0f);
        float x = directionXZ.magnitude;
        float y = direction.y;
        float angleInRadians = _shotAngle * Mathf.Deg2Rad;
        float velocity2 = Physics.gravity.y * Mathf.Pow(x, 2) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float velocity = Mathf.Sqrt(Mathf.Abs(velocity2));
        return velocity;
    }

    private Vector3[] GetPath(Vector3 vector)
    {
        Vector3[] path = new Vector3[_waypointsCount];
        float time;

        for (var i = 0; i < path.Length; i++)
        {
            time = i * _timeStep;
            path[i] = _shotPoint.position + vector * time + Physics.gravity * Mathf.Pow(time, 2) / 2f;
        }

        return path;
    }

    private void Validate()
    {
        if (_cardsDeck == null)
            throw new InvalidOperationException();

        if (_shotPoint == null)
            throw new InvalidOperationException();

        if (_template == null)
            throw new InvalidOperationException();
    }
}