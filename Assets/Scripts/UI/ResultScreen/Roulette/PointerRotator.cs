using UnityEngine;
using DG.Tweening;

public class PointerRotator : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float _delayBeforeRotation;
    [Min(0)]
    [SerializeField] private float _rotationDuration;
    [Range(-180, 0)]
    [SerializeField] private float _minimumAngle = -70f;
    [Range(0, 180)]
    [SerializeField] private float _maximumAngle = 70f;

    private void OnEnable() 
        => Rotate(GetRandomAngle());

    public void Rotate(Vector3 angle) 
        => GetComponent<RectTransform>().DOLocalRotate(angle, _rotationDuration).SetDelay(_delayBeforeRotation);

    private Vector3 GetRandomAngle()
        => new Vector3(0f, 0f, Random.Range(_minimumAngle, _maximumAngle));
}